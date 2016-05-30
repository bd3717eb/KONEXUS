
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Web;
using System.Data;
using IntegraData;

namespace Integra_Develoment
{
    public static class DropTool
    {
        public static void FillDrop(System.Web.UI.WebControls.DropDownList ddl, DataTable table, string textfield, string valuefield)
        {
            ddl.Items.Clear();
            ddl.DataSource = table;
            ddl.DataTextField = textfield;
            ddl.DataValueField = valuefield;
            ddl.DataBind();

        }

        public static void ClearDrop(System.Web.UI.WebControls.DropDownList ddl)
        {
            ddl.Items.Clear();
        }

        public static void CompleteDrop(DataTable table, int id, string description)
        {
            DataRow row = table.NewRow();

            row["Numero"] = id;
            row["Descripcion"] = description;

            table.Rows.InsertAt(row, 0);
        }

        public static void gfClearDrop(ref DropDownList drop, bool bInhabilitar = true)
        {
            if (!bInhabilitar)
                drop.Enabled = false;
            drop.Items.Clear();
        }
        public static void gfFillDrop(ref System.Web.UI.WebControls.DropDownList ddl, DataTable table, string textfield, string valuefield)
        {
            ddl.Items.Clear();
            ddl.DataSource = table;
            ddl.DataTextField = textfield;
            ddl.DataValueField = valuefield;
            ddl.DataBind();
        }

        public static void gfFill_DropdownList(string firts_data, ref DropDownList drp, DataTable table)
        {
            int index = 0;

            drp.Items.Clear();

            if (firts_data != null)
                drp.Items.Add(firts_data);

            foreach (DataRow row in table.Rows)
                drp.Items.Add(table.Rows[index++][1].ToString());
        }

        /// <summary>
        /// Función que llena el dropdownlist por referencia con los parametros requeridos para  llenarlo ( Query , DataTextField , DataValueFiel)
        /// </summary>
        /// <param name="listaInfoDrop">listaInfoDrop <c>Posicion 0 va el query </c> <br/><c>Posicion 1 va DataTextField perteneciente al dropdownlist</c><br/><c>Posicion 2 va DataValueField perteneciente al dropdownlist</c> </param>
        /// <param name="drop"></param>
        public static void gfDropFill(ref DropDownList drop, List<string> listaInfoDrop = null, DataTable table = null)
        {
            drop.Items.Clear();
            if (table == null)
            {
                SQLConection context = new SQLConection();
                DataTable dt;
                dt = context.ExecuteQuery(listaInfoDrop[0]);
                if (dt.Rows.Count > Globales.Default)
                {
                    drop.DataSource = dt;
                    drop.DataTextField = listaInfoDrop[1];
                    drop.DataValueField = listaInfoDrop[2];
                    drop.DataBind();
                    drop.Items.Insert(Globales.Default, " Seleccione ");
                    drop.SelectedIndex = Globales.Default;
                }
                else
                {
                    drop.Items.Insert(Globales.Default, " Ningun registro ");
                    drop.SelectedIndex = Globales.Default;
                }
            }
            else
            {
                int index = 0;
                drop.Items.Insert(Globales.Default, " Seleccione ");
                drop.SelectedIndex = Globales.Default;

                foreach (DataRow row in table.Rows)
                    drop.Items.Add(table.Rows[index++][1].ToString());

            }

        }
        public static void gfDropFill(ref DropDownList drop, List<string> listaInfoDrop, string sItemsHorizonte)
        {
            drop.Items.Clear();
            SQLConection context = new SQLConection();
            DataTable dt;
            dt = context.ExecuteQuery(listaInfoDrop[0]);
            if (dt.Rows.Count > Globales.Default)
            {
                string sConcatenaResultado = string.Empty;
                Dictionary<int, string> diccionarioHorizontal = new Dictionary<int, string>();
                for (int iC = 0; iC < dt.Rows[0].Table.Columns.Count; iC++)
                {
                    if (dt.Rows[0][iC].ToString() == "|")
                    {
                        int iTemporal = iC + 1;
                        int iTempora2 = iTemporal + 1;
                        diccionarioHorizontal.Add(int.Parse(dt.Rows[0][iTemporal].ToString()), dt.Rows[0][iTempora2].ToString());
                    }
                }

                drop.DataSource = diccionarioHorizontal;
                drop.DataTextField = "Value"; ;
                drop.DataValueField = "Key";
                drop.DataBind();
                drop.SelectedIndex = Globales.Default;
            }
            else
            {
                drop.Items.Insert(Globales.Default, " Ningun registro ");
                drop.SelectedIndex = Globales.Default;
            }
        }

        public static void gfDropFillBal(ref DropDownList drop, DataTable dt, string sItemsHorizonte)
        {
            drop.Items.Clear();
            // SQLConection context = new SQLConection();
            //  DataTable dt;
            //  dt = context.ExecuteQuery(listaInfoDrop[0]);
            if (dt.Rows.Count > Globales.Default)
            {
                string sConcatenaResultado = string.Empty;
                Dictionary<int, string> diccionarioHorizontal = new Dictionary<int, string>();
                for (int iC = 0; iC < dt.Rows[0].Table.Columns.Count; iC++)
                {
                    if (dt.Rows[0][iC].ToString() == "|")
                    {
                        int iTemporal = iC + 1;
                        int iTempora2 = iTemporal + 1;
                        diccionarioHorizontal.Add(int.Parse(dt.Rows[0][iTemporal].ToString()), dt.Rows[0][iTempora2].ToString());
                    }
                }

                drop.DataSource = diccionarioHorizontal;
                drop.DataTextField = "Value"; ;
                drop.DataValueField = "Key";
                drop.DataBind();
                drop.SelectedIndex = Globales.Default;
            }
            else
            {
                drop.Items.Insert(Globales.Default, " Ningun registro ");
                drop.SelectedIndex = Globales.Default;
            }
        }

        public static void FillDropSO(System.Web.UI.WebControls.DropDownList ddl, DataTable table, string textfield, string valuefield)
        {


            DataView dvOptions = new DataView(table);


            ddl.Items.Clear();
            ddl.DataSource = dvOptions;//table;
            ddl.DataTextField = textfield;
            ddl.DataValueField = valuefield;
            ddl.DataBind();

        }
    }
}