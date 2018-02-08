using Akka.Actor;
using AkkaTraining.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaTraining.Actors
{
    public class JobActor : ReceiveActor
    {

        private string _jobId;
        private IActorRef _coordinator;
        public JobActor(string jobId, IActorRef coordinator)
        {
            _jobId = jobId;
            _coordinator = coordinator;
            stopped();
        }


        public void stopped()
        {
            handleServiceRequests();
            Receive<StartJob>(
                startJob =>
                {
                    Console.WriteLine("Starting some very intensive job");
                    if(startJob.JobId == "die")
                    {
                        throw new TypeInitializationException(nameof(JobActor), new Exception("die"));
                    }
                    else if (startJob.JobId == "fail")
                    {
                        throw new Exception("fail");
                    }
                    Become(publishing);
                });
            Receive<StopJob>(
                stopJob =>
                {
                    Console.WriteLine($"JobActo Id={_jobId} is already stopped");
                });
        }

        private void publishing()
        {
            handleServiceRequests();
            Receive<StartJob>(
                startJob =>
                {
                    Console.WriteLine($"JobId = {_jobId} has been already started");
                });
            Receive<StopJob>(
                stopJob =>
                {
                    Console.WriteLine($"Stopping JobActor Id={_jobId}");
                    Become(stopped);
                });
        }

        private void handleServiceRequests()
        {
            Receive<GetStatus>(getStatus =>
            {
                _coordinator.Tell(new StatusResponse(_jobId, "I'm fine"));
            });
        }

        #region LifeCycle 
        protected override void PreStart()
        {
            Console.WriteLine($"Starting JobActor, Id = {_jobId}");
            base.PreStart();
        }

        protected override void PostStop()
        {
            Console.WriteLine($"JobActor, Id = {_jobId} has been stopped");
            base.PostStop();
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine($"JobActor, Id = {_jobId} has been restarted, reason {reason.Message}");
            base.PreRestart(reason, message);
        }
        #endregion


    }
}
