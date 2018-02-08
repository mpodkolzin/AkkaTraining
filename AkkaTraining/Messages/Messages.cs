using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AkkaTraining.Messages
{
    public class CommonMessage
    {
        public string JobId { get; private set; }

        public CommonMessage(string jobId) 
        {

            if(string.IsNullOrWhiteSpace(jobId))
            {
                throw new ArgumentNullException(nameof(jobId));
            }

            JobId = jobId;
        }
    }


    public class StartJob : CommonMessage
    {
        public StartJob(string jobId) : base(jobId)
        {

        }
    }
    public class StopJob : CommonMessage
    {
        public StopJob(string jobId) : base(jobId)
        {

        }
    }


    public class GetStatus
    {

    }

    public class StatusResponse : CommonMessage
    {
        public string Text { get; private set; }

        public StatusResponse(string jobId, string text) : base(jobId)
        {
            Text = text;
        }
    }

}
