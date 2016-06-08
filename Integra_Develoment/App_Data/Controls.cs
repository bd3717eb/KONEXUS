using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;

namespace Integra_Develoment
{
    public class Controls
    {
        public void FillDrp(DropDownList drp, DataTable table, string textfield, string valuefield)
        {

            //if (textfield != null)

            drp.DataSource = table;
            drp.DataTextField = textfield;
            drp.DataValueField = valuefield;
            drp.DataBind();
        }

        public void ClearDrp(DropDownList drp)
        {
            drp.Items.Clear();
        }
    }
}