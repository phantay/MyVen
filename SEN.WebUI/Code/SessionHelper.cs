using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace SEN.WebUI.Code
{
    public class SessionHelper
    {
            public static void SetEmailSession(string email)
            {
                HttpContext.Current.Session["UserEmail"] = email;
            }

            public static string GetEmailSession()
            {
                var userEmail = HttpContext.Current.Session["UserEmail"];

                return userEmail == null ? null : userEmail.ToString();
            }

            public static string GetSession()
            {
                var session = HttpContext.Current.Session["loginSession"];
                if (session == null)
                    return null;
                else
                {
                    return session.ToString();
                }
            }
    }
}