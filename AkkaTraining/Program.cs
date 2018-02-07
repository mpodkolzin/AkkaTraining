using AkkaTraining.CLI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace AkkaTraining
{
    class Program
    {
        static void Main(string[] args)
        {

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
            return false;

        }
        public static bool StopJob(string jobId)
        {
            return false;

        }
        public static bool GetJobStatus(string jobId)
        {
            return false;

        }
    }
}
