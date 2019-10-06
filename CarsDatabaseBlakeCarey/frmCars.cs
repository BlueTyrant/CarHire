using CarsDatabaseBlakeCarey.DataClass;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarsDatabaseBlakeCarey
{
    public partial class frmCars : Form
    {
        static MainClass mc = new MainClass();

        public static frmCars Current;

        public frmCars()
        {
            InitializeComponent();
            Current = this;
        }

        DataTable dt = mc.Select();
        int count = 0;        
        
        private void frmCars_Load(object sender, EventArgs e)
        {
            try
            {
                btnFirst.FlatStyle = FlatStyle.Flat;
                btnPrevious.FlatStyle = FlatStyle.Flat;
                btnNext.FlatStyle = FlatStyle.Flat;
                btnLast.FlatStyle = FlatStyle.Flat;
                btnUpdate.FlatStyle = FlatStyle.Flat;
                btnAdd.FlatStyle = FlatStyle.Flat;
                btnDelete.FlatStyle = FlatStyle.Flat;
                btnSearch.FlatStyle = FlatStyle.Flat;
                btnCancel.FlatStyle = FlatStyle.Flat;
                btnExit.FlatStyle = FlatStyle.Flat;
                btnClear.FlatStyle = FlatStyle.Flat;
                Paginate(count);
                toolTip1.SetToolTip(txtVehicleRegNo, "Enter the registration/number plate of the vehicle e.g 'GH376DRS'");
                toolTip2.SetToolTip(txtMake, "Enter the Make/Brand of the vehicle e.g 'Mazda'");
                toolTip3.SetToolTip(txtEngineSize, "Enter the Engine Size of the vehicle e.g '1.6L'");
                toolTip4.SetToolTip(txtDate, "Enter the date of registration for the vehicle in format YYYY/MM/DD e.g '2009/10/04'");
                toolTip5.SetToolTip(txtRental, "Enter the Rental price per day for the vehicle e.g 'R 90.00'");
                toolTip6.SetToolTip(cbxAvailable, "Check the Avalibilty of the vehicle");
            }
            catch (Exception)
            {
                MessageBox.Show("Error Getting Load data", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Clear()
        {
            txtVehicleRegNo.Text = "";
            txtMake.Text = "";
            txtEngineSize.Text = "";
            txtDate.Text = "";
            txtRental.Text = "";
            cbxAvailable.Checked = false;
        }

        public void Paginate(int count, [Optional] string page)
        {
            try
            {
                txtVehicleRegNo.Text = dt.Rows[count]["VehicleRegNo"].ToString();
                txtMake.Text = dt.Rows[count]["Make"].ToString();
                txtEngineSize.Text = dt.Rows[count]["EngineSize"].ToString();            
                txtDate.Text = dt.Rows[count]["DateRegistered"].ToString();
                int dateLength = txtDate.Text.Length;
                if (dateLength>20)
                {
                    txtDate.Text = (dt.Rows[count]["DateRegistered"].ToString()).Substring(0, 12);
                }
                else
                {
                    txtDate.Text = (dt.Rows[count]["DateRegistered"].ToString()).Substring(0, 10);
                }
                txtRental.Text = "R " + dt.Rows[count]["RentalPerDay"].ToString();
                cbxAvailable.Checked = (bool)dt.Rows[count]["Available"];
                if (page == "Last")
                {
                    txtPaginate.Text = dt.Rows.Count + " of " + dt.Rows.Count;
                }
                else
                {
                    txtPaginate.Text = (count + 1) + " of " + dt.Rows.Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DataCheck()
        {
            mc.DataTableChange(dt);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            const string message = "Do you want to Exit?";
            const string caption = "EXIT";
            var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Paginate(count);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSearch frmSearch = new frmSearch();
            frmSearch.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                //get the value from the input fields
                mc.VehicleRegNo = txtVehicleRegNo.Text;
                mc.Make = txtMake.Text;
                mc.EngineSize = txtEngineSize.Text;
                mc.DateRegistered = txtDate.Text;
                mc.RentalPerDay = Convert.ToDouble(txtRental.Text.Replace("R ", ""));
                mc.Available = cbxAvailable.Checked;

                bool success = mc.Insert(mc);
                if (success)
                {
                    dt = mc.Select();
                    MessageBox.Show("New Vehicle Sucessfully Inserted.");
                }
                else
                {
                    MessageBox.Show("Failed to Insert New Vehicle");
                }
                txtPaginate.Text = (count + 1) + " of " + dt.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex+"","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
}

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            { 
                mc.VehicleRegNo = txtVehicleRegNo.Text;

                bool success = mc.Delete(mc);
                if (success)
                {
                    dt = mc.Select();
                    MessageBox.Show("Vehicle Sucessfully Deleted.");
                }
                else
                {
                    MessageBox.Show("Failed to Delete New Vehicle");
                }

                if (count < (dt.Rows.Count - 1))
                {
                    btnNext_Click(sender, e);
                }
                else
                {
                    btnPrevious_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //get the value from the input fields
                mc.VehicleRegNo = txtVehicleRegNo.Text;
                mc.Make = txtMake.Text;
                mc.EngineSize = txtEngineSize.Text;
                mc.DateRegistered = txtDate.Text;
                mc.RentalPerDay = Convert.ToDouble(txtRental.Text.Replace("R ", ""));
                mc.Available = cbxAvailable.Checked;
                bool success = mc.Update(mc);
                if (success)
                {
                    dt = mc.Select();
                    MessageBox.Show("Vehicle Sucessfully Updated.");
                }
                else
                {
                    MessageBox.Show("Failed to Update Vehicle");
                }
                txtPaginate.Text = (count + 1) + " of " + dt.Rows.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex+"","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            count = 0;
            Paginate(count);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            count = dt.Rows.Count - 1;
            Paginate(count,"Last");
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (count > 0)
            {
                count--;
                Paginate(count);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (count<(dt.Rows.Count-1)) {
                count++;
                Paginate(count);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
