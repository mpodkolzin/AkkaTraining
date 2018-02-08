using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaTrainingUnitTests
{
    public static class TestConfig
    {
        public static string GetConfigString()
        {
             return @"akka {
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
                           router = round-robing-pool
                           nr-of-instances = 
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

        }
    }
}
