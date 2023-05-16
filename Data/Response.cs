using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Adstra_task
{
    public class Response
    {


            public bool IsSuccess { get; set; }
            public string Message { get; set; }
            public dynamic result { get; set; }

            public string Controller { get; set; }
            public string Action { get; set; }

            public Response()
            {
                IsSuccess = false;
                Message = string.Empty;
                result = new ExpandoObject();
                Controller = "Home";
                Action = "Login";

            }
        }
}
