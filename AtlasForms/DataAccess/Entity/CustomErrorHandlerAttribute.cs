using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AtlasForms.DataAccess.Entity
{
    public class CustomErrorHandlerAttribute : ActionFilterAttribute, IExceptionFilter

    {

        public void OnException(ExceptionContext filterContext)

        {

            Exception exception = filterContext.Exception;
            var controller = filterContext.Controller as Controller;
            controller.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            controller.Response.TrySkipIisCustomErrors = true;
            filterContext.ExceptionHandled = true;

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            filterContext.ExceptionHandled = true;
            var model = new HandleErrorInfo(exception, controllerName, actionName);
            Logger.SaveErr(exception);
            filterContext.Result = new ViewResult()
            {
                ViewName = "error",
                ViewData = new ViewDataDictionary
                {
                    Model = model
                }
            };
        }

    }
}