using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Configuration;

namespace AkkaTraining.Config
{
    public static class ActorSystemConfig
    {
        public static Akka.Configuration.Config GetActorSystemConfig()
        {

            string configString = @"
                     akka {
                        loglevel = DEBUG
                        #loggers = [""Akka.Logger.NLog.NLogLogger, Akka.Logger.NLog""]
                        log-remote-lifecycle-events = INFO
                     }";

            return ConfigurationFactory.ParseString(configString);
        }
    }
}
