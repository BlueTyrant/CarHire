using CarsDatabaseBlakeCarey.DataClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarsDatabaseBlakeCarey
{
    public static class StringExtensions
    {
        public static bool CaseInsensitiveContains(this string text, string value,
        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
        {
            return text.IndexOf(value, stringComparison) >= 0;
        }
    }
    public partial class frmSearch : Form
    {
        public static frmSearch Current;

        MainClass mc = new MainClass();

        public frmSearch()
        {
            InitializeComponent();
            Current = this;
        }

        private void frmSearch_Load(object sender, EventArgs e)
        {
            cboField.DropDownStyle = ComboBoxStyle.DropDownList;
            cboOperator.DropDownStyle = ComboBoxStyle.DropDownList;

            DataTable dtsf = new DataTable();
            dtsf.Columns.Add("id", typeof(int));
            dtsf.Columns.Add("name", typeof(string));

            dtsf.Rows.Add(1, "Make");
            dtsf.Rows.Add(2, "EngineSize");
            dtsf.Rows.Add(3, "RentalPerDay");
            dtsf.Rows.Add(4, "Available");
            cboField.DataSource = dtsf;
            cboField.DisplayMember = "name";
            cboField.ValueMember = "id";

            DataTable dtso = new DataTable();
            dtso.Columns.Add("id", typeof(int));
            dtso.Columns.Add("name", typeof(string));

            dtso.Rows.Add(1, "=");
            dtso.Rows.Add(2, "<");
            dtso.Rows.Add(3, ">");
            dtso.Rows.Add(4, "<=");
            dtso.Rows.Add(5, ">=");
            cboOperator.DataSource = dtso;
            cboOperator.DisplayMember = "name";
            cboOperator.ValueMember = "id";

            //cboField.SelectedValueChanged += cboField_SelectedValueChanged
            DataTable dt = mc.Select();
            dgvSearch.DataSource = dt;
            this.dgvSearch.Columns["ID"].Visible = false;
            //int count = 0;
            //if (count < (dt.Rows.Count - 1))
            //{
            //    count++;
            //    //double test = this.dgvSearch.Rows[count].Cells["RentalPerDay"].Value;
            //    this.dgvSearch.Rows[count].Cells["Make"].Value = "R"+90;//'R' + this.dgvSearch.Rows[count].Cells["RentalPerDay"].Value;
            //}
            //this.dgvSearch.Columns["RentalPerDay"].Value = 'R'; 
            btnRun.FlatStyle = FlatStyle.Flat;
            btnClose.FlatStyle = FlatStyle.Flat;
        }        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCars.Current.Show();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            try
            {
                double parsedValue;
                string Field = cboField.Text;
                string Operator = cboOperator.Text;
                DataTable dt = mc.Select();
                DataView dv = dt.DefaultView;
                //Filter the DatagridView using the txtBox and comboBoxes -- (txtValue, cboField , cboOperator)
                DataTable dte = mc.Empty();
                DataView dve = dte.DefaultView;

                if (Field == "Available" && Operator != "=" || Field == "RentalPerDay" && !double.TryParse(txtValue.Text, out parsedValue))
                {
                    
                    dgvSearch.DataSource = dve.ToTable();
                }
                else
                {
                    if (Field == "Available" && txtValue.Text.Equals("Yes", StringComparison.CurrentCultureIgnoreCase) || Field == "Available" && txtValue.Text == "1")
                    {
                        dv.RowFilter = string.Format(Field + Operator + "'{0}'", "True");
                        dgvSearch.DataSource = dv.ToTable();
                    }
                    else if (Field == "Available" && txtValue.Text.Equals("No", StringComparison.CurrentCultureIgnoreCase) || Field == "Available" && txtValue.Text == "0")
                    {
                        dv.RowFilter = string.Format(Field + Operator + "'{0}'", "False");
                        dgvSearch.DataSource = dv.ToTable();
                    }
                    else if (Field == "Available" && (!txtValue.Text.CaseInsensitiveContains("True") || !txtValue.Text.CaseInsensitiveContains("Yes")))
                    {
                        dgvSearch.DataSource = dve.ToTable();
                    }
                    else
                    {
                        dv.RowFilter = string.Format(Field + Operator + "'{0}'", txtValue.Text);
                        dgvSearch.DataSource = dv.ToTable();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Incorrect values supplied, Please input correct values and formats");
            }
            
        }
    }
}
