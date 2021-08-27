using System.Web;
using System.Web.Mvc;

namespace Monitoring_First_Version
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
