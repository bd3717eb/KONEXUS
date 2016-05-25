using System.Data;
using System.Web;

namespace IntegraBussines
{
    public class SecurityController
    {

        public bool lfAccess(string sPageName)
        {
            DataTable dtaccess = HttpContext.Current.Session["sauthorization_page"] as DataTable;
            DataRow[] dresult = dtaccess.Select("Observaciones like '%" + sPageName + "%'");
            if (dresult.Length >= 1)
                return true;
            else
                return false;

        }
    }
}
