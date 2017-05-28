using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace ControllerFactory.Models
{
    public class RemoteService
    {
        public string GetRemoteData()
        {
            Thread.Sleep(2000);
            return "Hello world";
        }
    }
}