using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;
using MySql.Data.MySqlClient;

namespace ServiceSystem
{
    public partial class patientRefills : Form
    {
        
        public patientRefills()
        {
            InitializeComponent();
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Doctor_ID, First_Name, Last_Name FROM fjs_doctors";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("@myDate", dateString);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable);
                Console.WriteLine("Table is ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            foreach (DataRow row in myTable.Rows)
            {
                listBox1.Items.Add(Int32.Parse(row["Doctor_ID"].ToString())+" "+row["First_Name"].ToString()+" "+row["Last_Name"].ToString());
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            patientMainMenu patMain = new patientMainMenu();
            patMain.ShowDialog();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Medicine FROM fjs_prescriptions";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                //cmd.Parameters.AddWithValue("@myDate", dateString);
                MySqlDataAdapter myAdapter = new MySqlDataAdapter(cmd);
                myAdapter.Fill(myTable);
                Console.WriteLine("Table is ready.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            conn.Close();
            foreach (DataRow row in myTable.Rows)
            {
                listBox2.Items.Add(row["Medicine"]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            string status = "Pending";
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "Insert into fjs_requests (Patient_ID, Medicine, Status, Message) Values('" + 1 + "', '" + text + "', '" + status + "', '" + textBox1.Text + "')";
                MySqlCommand com = new MySqlCommand(insert, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
