using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarsDatabaseBlakeCarey.DataClass
{
    class MainClass
    {
        //getter and setter
        public string VehicleRegNo { get; set; }
        public string Make { get; set; }
        public string EngineSize { get; set; }
        public string DateRegistered { get; set; }
        public double RentalPerDay { get; set; }
        public bool Available { get; set; }

        static string myConString = ConfigurationManager.ConnectionStrings["HireDB"].ConnectionString;

        SqlDataAdapter sda;

        public void DataTableChange(DataTable dataTable)
        {
            // Create temporary DataSet variable and
            // GetChanges for modified rows only.            
                DataTable tempDataSet =
                    dataTable.GetChanges(DataRowState.Modified);

            if (tempDataSet != null)
            {
                sda.Update(tempDataSet);
            }
        }

        public DataTable Empty()
        {
            //connect to the database
            SqlConnection sqlCon = new SqlConnection(myConString);
            DataTable dt = new DataTable();
            try
            {
                //write the sql query
                string sql = "SELECT * FROM Car WHERE 1=2";
                //creating a command using 'sql' and 'sqlCon'
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //Creating a sql adapter using 'cmd'
                sda = new SqlDataAdapter(cmd);

                sqlCon.Open();
                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlCon.Close();
            }
            return dt;
        }

        public DataTable Select()
        {
            //connect to the database
            SqlConnection sqlCon = new SqlConnection(myConString);
            DataTable dt = new DataTable();
            try
            {
                //write the sql query
                string sql = "SELECT * FROM Car";
                //creating a command using 'sql' and 'sqlCon'
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //Creating a sql adapter using 'cmd'
                sda = new SqlDataAdapter(cmd);

                sqlCon.Open();
                sda.Fill(dt);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlCon.Close();
            }
            return dt;
        }

        public bool Insert(MainClass mc)
        {
            //setting the default return type and setting its value to false
            bool isSuccess = false;

            //connect to the Database
            SqlConnection sqlCon = new SqlConnection(myConString);
            try
            {
                //create a sql query to Insert Data
                string sql = "INSERT into Car(VehicleRegNo, Make, EngineSize, DateRegistered, RentalPerDay, Available) VALUES (@VehicleRegNo, @Make, @EngineSize, @DateRegistered, @RentalPerDay, @Available)";
                //creating a sql command using sql and sqlCon
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //create the Parameters to add data
                cmd.Parameters.AddWithValue("@VehicleRegNo", mc.VehicleRegNo);
                cmd.Parameters.AddWithValue("@Make", mc.Make);
                cmd.Parameters.AddWithValue("@EngineSize", mc.EngineSize);
                cmd.Parameters.AddWithValue("@DateRegistered", mc.DateRegistered);
                cmd.Parameters.AddWithValue("@RentalPerDay", mc.RentalPerDay);
                cmd.Parameters.AddWithValue("@Available", mc.Available);

                //open the connection
                sqlCon.Open();
                //If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Loading data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlCon.Close();
            }

            return isSuccess;
        }

        public bool Update(MainClass mc)
        {
            //setting the default return type and setting its value to false
            bool isSuccess = false;

            //connect to the Database
            SqlConnection sqlCon = new SqlConnection(myConString);
            try
            {
                //create a sql query to Insert Data
                string sql = "UPDATE Car SET Make=@Make, EngineSize=@EngineSize, DateRegistered=@DateRegistered, RentalPerDay=@RentalPerDay, Available=@Available WHERE VehicleRegNo=@VehicleRegNo";
                //creating a sql command using sql and sqlCon
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //create the Parameters to add data
                cmd.Parameters.AddWithValue("@VehicleRegNo", mc.VehicleRegNo);
                cmd.Parameters.AddWithValue("@Make", mc.Make);
                cmd.Parameters.AddWithValue("@EngineSize", mc.EngineSize);
                cmd.Parameters.AddWithValue("@DateRegistered", mc.DateRegistered);
                cmd.Parameters.AddWithValue("@RentalPerDay", mc.RentalPerDay);
                cmd.Parameters.AddWithValue("@Available", mc.Available);

                //open the connection
                sqlCon.Open();
                //If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Updating the data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlCon.Close();
            }

            return isSuccess;
        }

        public bool Delete(MainClass mc)
        {
            //setting the default return type and setting its value to false
            bool isSuccess = false;

            //connect to the Database
            SqlConnection sqlCon = new SqlConnection(myConString);
            try
            {
                //create a sql query to Insert Data
                string sql = "Delete Car WHERE VehicleRegNo=@VehicleRegNo";
                //creating a sql command using sql and sqlCon
                SqlCommand cmd = new SqlCommand(sql, sqlCon);
                //create the Parameters to add data
                cmd.Parameters.AddWithValue("@VehicleRegNo", mc.VehicleRegNo);

                //open the connection
                sqlCon.Open();
                //If the query runs successfully then the value of rows will be greater than zero else its value will be 0
                int rows = cmd.ExecuteNonQuery();
                if (rows > 0)
                {
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error Deleting the data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                sqlCon.Close();
            }

            return isSuccess;
        }
    }
}
