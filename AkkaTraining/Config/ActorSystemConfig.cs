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
                        provider = ""Akka.Cluster.ClusterActorRefProvider, Akka.Cluster""
                        #loggers = [""Akka.Logger.NLog.NLogLogger, Akka.Logger.NLog""]
                        actor {
                            deployment {
                                /JobCoordinator/JobsBroadcaster {
                                   router = broadcast-group
                                   routees.paths = [""/user/JobCoordinator/JobActor*""]
                                   cluster {
                                      enabled = on
                                      allow-local-routees = on
                                      use-role = worker
                                   }
                                }
                                /JobCoordinator/AdHocJobsRouter {
                                   router = consistent-hashing-pool
                                   nr-of-instances = 5
                                   cluster {
                                      enabled = on
                                      allow-local-routees = on
                                      use-role = worker
                                   }
                                }
                            }
                        }
                      remote {
                        dot-netty.tcp {
                          transport-protocol = tcp
                          port = 0
                          hostname = ""mpolenovo""
                        }
                      }
                      cluster {
                        seed-nodes = [""mpodlenovo""]
                        roles = [worker]
                      }

                    }";

            return ConfigurationFactory.ParseString(configString);
        }
    }
}
