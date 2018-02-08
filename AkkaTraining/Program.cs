using AkkaTraining.CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using Akka.Actor;
using AkkaTraining.Config;
using AkkaTraining.Actors;
using AkkaTraining.Messages;

namespace AkkaTraining
{
    class Program
    {

        static ActorSystem actorSystem;
        static IActorRef coordinator;
        const string actorSystemName = "CastActorSystem";


        static void Main(string[] args)
        {

            actorSystem = ActorSystem.Create(actorSystemName, ActorSystemConfig.GetActorSystemConfig());

            coordinator = actorSystem.ActorOf(Props.Create(() => new JobsCoordinator()), "JobCoordinator");

            while(true)
            {

                var input = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Input cannot be empty;");
                    continue;
                }

                parseCommandLineArgs(input.Split(' '));

            }


        }

        private static void parseCommandLineArgs(IEnumerable<string> args)
        {
            if(args == null || !args.Any())
            {
            return;
            }
            var res = Parser.Default.ParseArguments<StartOptions, StopOptions, StatusOptions>(args)
                .MapResult(
                    (StartOptions opts) => StartJob(opts.JobId),
                    (StopOptions opts) => StopJob(opts.JobId),
                    (StatusOptions opts) => GetJobStatus(opts.JobId),
                    e => false);
        }


        public static bool StartJob(string jobId)
        {
            coordinator.Tell(new StartJob(jobId));
            return true;

        }
        public static bool StopJob(string jobId)
        {
            coordinator.Tell(new StopJob(jobId));
            return false;
        }
        public static bool GetJobStatus(string jobId)
        {
            coordinator.Tell(new GetStatus());
            return true;

        }
    }
}
