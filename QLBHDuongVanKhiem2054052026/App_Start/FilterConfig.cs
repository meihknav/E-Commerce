using System.Web;
using System.Web.Mvc;

namespace QLBHDuongVanKhiem2054052026
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
