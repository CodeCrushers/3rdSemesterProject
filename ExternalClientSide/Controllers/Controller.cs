using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace ExternalClientSide.Controllers
{
    public class Controller
    {
        [ScriptMethod, WebMethod]
        public static Boolean emailPassworldCheck(String email, string password)
        {
            Boolean ReturnCheck;
            ReturnCheck = false;
            if (email=="Email"&&password=="password")
            {
                ReturnCheck = true;
            }





            return ReturnCheck;
        }
    }
}