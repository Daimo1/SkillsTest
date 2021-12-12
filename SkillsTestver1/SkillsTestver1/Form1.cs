using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SkillsTestver1
{
    public partial class Form1 : Form
    {
        SqlConnection con = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\franz\\source\\repos\\SkillsTestver1\\SkillsTestver1\\Database1.mdf;Integrated Security=True;");
        SqlCommand cmd;
        SqlDataAdapter adapt;
        DataTable dt;

        int ID = 0;
        public Form1()
        {
            InitializeComponent();
            displayData();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("insert into sampleDB(First_Name,Last_Name) values(@fname,@lname)",con);
            con.Open();
            cmd.Parameters.AddWithValue("@fname", firstname.Text);
            cmd.Parameters.AddWithValue("@lname", lastname.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Saved Successfully");
            displayData();
            clearData();
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("update sampleDB set First_Name=@fname,Last_Name=@lname where Id=@id", con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", ID);
            cmd.Parameters.AddWithValue("@fname", firstname.Text);
            cmd.Parameters.AddWithValue("@lname", lastname.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Updated Successfully");
            displayData();
            clearData();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand("delete sampleDB where Id=@id", con);
            con.Open();
            cmd.Parameters.AddWithValue("@id", ID);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Data Deleted Successfully");
            displayData();
            clearData();
        }
        private void displayData()
        {
            con.Open();
            adapt = new SqlDataAdapter("select * from sampleDB", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void search_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            adapt = new SqlDataAdapter("select * from sampleDB where First_Name like '"+search.Text+"%'", con);
            dt = new DataTable();
            adapt.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            firstname.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            lastname.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
        }
        private void clearData()
        {
            ID = 0;
            firstname.Text = "";
            lastname.Text = "";
        }
    }
}
