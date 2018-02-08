using Akka.Actor;
using AkkaTraining.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaTraining.Actors
{
    public class AdHocJob : ReceiveActor
    {
        private IActorRef _self;

        public AdHocJob()
        {
            _self = this.Self;
            ready();

        }

        private void ready()
        {
            Receive<StartJob>(
                startJob => 
                {
                    Console.WriteLine($"Starting AdHoc job Id=[{startJob.JobId}] at {_self.Path}");
                });
        }


        protected override void PreStart()
        {
            Console.WriteLine($"Starting ad_hoc_job {Self.Path}");
            base.PreStart();
        }
    }
}
