using WERC.Filters.ActionFilterAttributes;
using WERC.Filters.ResultFilters;
using System.Web.Mvc;

namespace WERC
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LocalizationAttribute(), 0);
            filters.Add(new BaseModelDataProviderResultFilter(), 1);
            filters.Add(new PreLoadDataActionFilter(), 3);
            filters.Add(new HandleErrorAttribute());           
        }
    }
}
