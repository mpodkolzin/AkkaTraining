
using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace AkkaTraining.CLI
{
    class CommandLineOptions
    {

        [Option('t', "topic", Required = true,
            HelpText = "Kafka topic")]
        public string Topic { get; set; }

    }

    [Verb("start", HelpText = "Add file contents to the index.")]
    class StartOptions
    {
        [Option("id", Default =false, Required = true,
            HelpText = "Job Id")]
        public string JobId { get; set; }
    }

    [Verb("stop", HelpText = "Add file contents to the index.")]
    class StopOptions
    {
        [Option("id", Default =false, Required = true,
            HelpText = "Job Id")]
        public string JobId { get; set; }
    }

    [Verb("status", HelpText = "Add file contents to the index.")]
    class StatusOptions
    {
        [Option("id", Default =false, Required = true,
            HelpText = "Job Id")]
        public string JobId { get; set; }
    }
}