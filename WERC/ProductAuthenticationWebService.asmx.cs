using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StoreLocator
{
    /// <summary>
    /// Summary description for ProductAuthenticationWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class ProductAuthenticationWebService : System.Web.Services.WebService
    {

        static string path;
        static string authenticatePath;
        static ProductAuthenticationWebService()
        {
            path = HttpContext.Current.Server.MapPath("~/App_Data/URL.txt");
            authenticatePath = HttpContext.Current.Server.MapPath("~/App_Data/authenticate.txt");
        }

        [WebMethod]
        public bool Authenticate(string Url, List<string> ip)
        {
            try
            {

                var streamReader = new StreamReader(authenticatePath);
                var data = streamReader.ReadToEnd().Split(',').ToArray();

                Url = Url.ToLower();
                var ipExists = data.Any(x => ip.Contains(x));


                if (data.Contains(Url) || ipExists == true)
                {
                    streamReader.Close();
                    return true;
                }

                streamReader.Close();

                TextWriter tw = new StreamWriter(path, true);
                tw.Write(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ": ");
                tw.WriteLine(Url);
                tw.Close();

            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
