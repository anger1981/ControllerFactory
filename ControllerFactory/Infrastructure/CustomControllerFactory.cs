using ControllerFactory.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.SessionState;

namespace ControllerFactory.Infrastructure
{
    public class CustomControllerFactory : IControllerFactory
    {

        public IController CreateController(RequestContext requestContext,
            string controllerName)
        {

            //Type targetType = null;
            //switch (controllerName)
            //{
            //    case "Product":
            //        targetType = typeof(ProductController);
            //        break;
            //    case "Customer":
            //        targetType = typeof(CustomerController);
            //        break;
            //    default:
            //        requestContext.RouteData.Values["controller"] = "Product";
            //        targetType = typeof(ProductController);
            //        break;
            //}

            var q = from t in Assembly.GetExecutingAssembly().GetTypes()
                    where t.IsClass && t.Namespace == "ControllerFactory.Controllers"
                    select t;

            int ctrl_cnt = Assembly.GetExecutingAssembly().GetTypes()
                .Where(p => p.IsClass && p.Namespace == "ControllerFactory.Controllers")
                .Where(t => t.Name.Contains(controllerName + "Controller"))
                .Where(t => typeof(IController).IsAssignableFrom(t)).Count();  // .Where(t => t.GetInterfaces().Contains(typeof(IController))).Count(); //.Where(i => i.Name == "IController").Count() >= 1).Count(); // t.IsAssignableFrom(typeof(IController))).Count(); //// && t.IsAssignableFrom(typeof(IController))).Count();

            var type_ctrl = ctrl_cnt != 1 ? null : Assembly.GetExecutingAssembly().GetTypes()
                .Where(p => p.IsClass && p.Namespace == "ControllerFactory.Controllers")
                .Where(t => t.Name.Contains(controllerName + "Controller"))
                .Where(t => typeof(IController).IsAssignableFrom(t)).First(); ///// && t.IsAssignableFrom(typeof(IController))).First();


            return type_ctrl == null ? (IController)DependencyResolver.Current.GetService(typeof(ProductController)) :
                (IController)DependencyResolver.Current.GetService(type_ctrl);

            //targetType = typeof((Type)type_controller);

            //return targetType == null ? null :
            //    (IController)DependencyResolver.Current.GetService(targetType);
        }

        public SessionStateBehavior GetControllerSessionBehavior(RequestContext
            requestContext, string controllerName)
        {
            return SessionStateBehavior.Default;
        }

        public void ReleaseController(IController controller)
        {
            IDisposable disposable = controller as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}