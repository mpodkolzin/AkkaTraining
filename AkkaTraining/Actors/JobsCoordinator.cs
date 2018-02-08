using Akka.Actor;
using Akka.Routing;
using AkkaTraining.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaTraining.Actors
{
    public class JobsCoordinator : ReceiveActor
    {
        private IActorRef _self;

        private Dictionary<string, IActorRef> _jobs;

        private IActorRef _jobsBroadcaster;
        private IActorRef _adHocJobsRouter;

        public JobsCoordinator()
        {
            _self = Self;
            _jobs = new Dictionary<string, IActorRef>();
            _jobsBroadcaster = Context.ActorOf(Props.Empty.WithRouter(FromConfig.Instance), "JobsBroadcaster");
            _adHocJobsRouter = Context.ActorOf(Props.Create(() => new AdHocJob()).WithRouter(FromConfig.Instance), "AdHocJobsRouter");
            //_adHocJobsRouter = Context.ActorOf(Props.Create(() => new AdHocJob()).WithRouter(new RoundRobinPool(5)), "AdHocJob");

            ready();
        }

        private void ready()
        {
            Receive<StartJob>(
                startJob =>
                {
                    if(startJob.JobId.StartsWith("ah"))
                    {
                        _adHocJobsRouter.Tell(new ConsistentHashableEnvelope(startJob, startJob.JobId));

                    }
                    else
                    {
                        var jobActor = createJobIfNotExist(startJob.JobId);
                        jobActor.Tell(startJob);
                    }

                });
            Receive<GetStatus>(
                getStatus =>
                {
                    _jobsBroadcaster.Tell(getStatus);
                });
            Receive<StatusResponse>(
                status =>
                {
                    Console.WriteLine($"Job id={status.JobId}, repored {status.Text}");
                });
        }

        private void publishing()
        {

        }

        private IActorRef createJobIfNotExist(string jobId)
        {
            IActorRef res;
            if (_jobs.ContainsKey(jobId))
            {
                res = _jobs[jobId];
            }
            else
            {
                res = Context.ActorOf(Props.Create(() => new JobActor(jobId, _self)), $"JobActor_{jobId}");
                _jobs.Add(jobId, res);
            }

            return res;

        }


        #region LifeCycle

        protected override void PreStart()
        {
            Console.WriteLine($"Starting {nameof(JobsCoordinator)} actor");
            base.PreStart();
        }
        protected override void PostStop()
        {
            Console.WriteLine($"{nameof(JobsCoordinator)} actor has been stopped");
            base.PreStart();
        }


        #endregion

        protected override SupervisorStrategy SupervisorStrategy()
        {
            var supervisionStrategy = new OneForOneStrategy(10, TimeSpan.FromMinutes(5), 
            e =>
            {
                if(e is TypeInitializationException)
                {
                    return Directive.Stop;
                }
                else if  (e is Exception)
                {
                    return Directive.Restart;
                }
                else
                {
                    return Directive.Resume;
                }

            });

            return supervisionStrategy;
        }


    }
}
