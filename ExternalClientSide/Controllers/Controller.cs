using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Script.Services;
using System.Web.Services;

namespace ExternalClientSide.Controllers
{
    public partial class Controller : System.Web.UI.Page
    {
        [WebMethod]
        public static Boolean EmailPassworldCheck(String email, string password)
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