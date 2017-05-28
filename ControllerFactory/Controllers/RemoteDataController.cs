using ControllerFactory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ControllerFactory.Controllers
{
    public class RemoteDataController : Controller
    {
        // GET: RemoteData
        public async Task<ActionResult> Data()
        {
            string data = await Task<string>.Factory.StartNew(() =>
            {
                return new RemoteService().GetRemoteData();
            });

            return View((object)data);
        }
    }
}