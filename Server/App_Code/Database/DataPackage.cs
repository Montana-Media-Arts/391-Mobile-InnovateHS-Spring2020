using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InnovateServer.App_Code.Database
{
    public class DataPackage
    {
        private bool wasSuccessful = true;  //Successful unless told otherwise.
        private Object data;
        private string message;

        public bool WasSuccessful { get => wasSuccessful; set => wasSuccessful = value; }
        public object Data { get => data; set => data = value; }
        public string Message { get => message; set => message = value; }

    }
}