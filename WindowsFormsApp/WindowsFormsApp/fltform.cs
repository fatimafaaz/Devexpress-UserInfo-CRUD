using DevExpress.XtraBars;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class fltform : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm

    {
     
        SqlConnection con = new SqlConnection("Data Source=CN-140;Initial Catalog=UserDatabase;Integrated Security=true;");
        SqlCommand cmd;
        SqlDataAdapter adapt;


        


        public fltform()
        {
            InitializeComponent();
        }

    

        private void btnsave_Click(object sender, EventArgs e)
        {

            if (txtName.Text != "" && txtpass.Text != "")
            {
                cmd = new SqlCommand("insert into UserTable(Username,Password) values(@name,@pass)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@pass", txtpass.Text);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Inserted Successfully");
                DisplayData();
                
            }
            else
            {
                MessageBox.Show("Please Provide Details!");
            }


        }



        private void DisplayData()
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from UserTable", con);
            adapt.Fill(dt);
            showdatagrid.DataSource = dt;
            con.Close();
        }

    

        private void btndel_Click(object sender, EventArgs e)
        {
            int i = this.showdatagrid.SelectedRows[0].Index;
            var ID = Convert.ToInt32(showdatagrid.Rows[i].Cells[0].Value.ToString());

            if (ID != 0)
            {
                cmd = new SqlCommand("delete UserTable where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Deleted Successfully!");
                DisplayData();
               
            }
            else
            {
                MessageBox.Show("Please Select Record to Delete");
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var ID = Convert.ToInt32(showdatagrid.Rows[e.RowIndex].Cells[0].Value.ToString());
            txtName.Text = showdatagrid.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtpass.Text = showdatagrid.Rows[e.RowIndex].Cells[2].Value.ToString();
        }


        private void btnupdate_Click(object sender, EventArgs e)
        {


            if (txtName.Text != "" && txtpass.Text != "")
            {

                int i = this.showdatagrid.SelectedRows[0].Index;
                var ID = Convert.ToInt32(showdatagrid.Rows[i].Cells[0].Value.ToString());
                cmd = new SqlCommand("update UserTable set Username=@name,Password=@pass where id=@id", con);
                con.Open();
                cmd.Parameters.AddWithValue("@id", ID);
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@pass", txtpass.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record Updated Successfully");
                con.Close();
                DisplayData();
                //ClearData();
            }
            else
            {
                MessageBox.Show("Please Select Record to Update");
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void lbname_Click(object sender, EventArgs e)
        {

        }

        private void btnshow_Click(object sender, EventArgs e)
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from UserTable", con);
            adapt.Fill(dt);
            showdatagrid.DataSource = dt;
            con.Close();
        }

        private void showdatagrid_SelectionChanged(object sender, EventArgs e)
        {


            if (showdatagrid.SelectedRows.Count > 0)
            {
                btnupdate.Visible = true;
                btndel.Visible = true;
            }


        }

        private void showdatagrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void fltform_Load(object sender, EventArgs e)
        {
            con.Open();
            DataTable dt = new DataTable();
            adapt = new SqlDataAdapter("select * from UserTable", con);
            adapt.Fill(dt);
            showdatagrid.DataSource = dt;
            con.Close();
        }
    }
}
