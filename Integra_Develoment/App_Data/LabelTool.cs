using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Integra_Develoment
{
    public static class LabelTool
    {
        public static void ShowLabel(System.Web.UI.WebControls.Label lbl, string msg, System.Drawing.Color colour)
        {
            lbl.Text = msg;
            lbl.ForeColor = colour;
            lbl.Visible = true;
        }

        public static void HideLabel(System.Web.UI.WebControls.Label lbl)
        {
            lbl.Text = "";
            lbl.Visible = false;
        }
    }
}