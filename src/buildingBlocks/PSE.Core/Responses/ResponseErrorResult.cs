using System;
using System.Collections.Generic;

namespace PSE.Core.Responses
{
    public class ResponseErrorResult
    {
        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Errors { get; set; }

        public ResponseErrorResult()
        {
            Errors = new ResponseErrorMessages();
        }
    }

    public class ResponseErrorMessages
    {
        public List<string> Messages { get; set; }

        public ResponseErrorMessages()
        {
            Messages = new List<string>();
        }
    }
}
