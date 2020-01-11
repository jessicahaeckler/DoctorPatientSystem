using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections;

namespace ServiceSystem
{
    public partial class refillRequests : Form
    {
        int rID;
        string status;
        public refillRequests()
        {
            InitializeComponent();
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                status = "Pending";
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Name, Request_ID, Date FROM fjs_patient p JOIN fjs_requests ON p.Patient_ID = fjs_requests.Patient_ID WHERE Status = '"+status+"' ORDER BY Date";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
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
                listBox1.Items.Add(row["Request_ID"]+" " + row["Name"] + " " + row["Date"]);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            doctorMainMenu docMain = new doctorMainMenu();
            docMain.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            String[] tokens = text.Split(' ');
            rID = Int32.Parse(tokens[0]);
            status = "Accepted";
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "UPDATE fjs_requests SET Status ='" + status + "' WHERE Request_ID = "+rID;
                MySqlCommand com = new MySqlCommand(insert, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            String[] tokens = text.Split(' ');
            rID = Int32.Parse(tokens[0]);
            status = "Denied";
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "UPDATE fjs_requests SET Status ='" + status + "' WHERE Request_ID = " + rID;
                MySqlCommand com = new MySqlCommand(insert, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Name, Request_ID, Medicine, Message, r.Patient_ID FROM fjs_patient p JOIN fjs_requests r ON p.Patient_ID = r.Patient_ID ORDER BY Date";
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
            //convert the retrieved data to events and save them to the list
            foreach (DataRow row in myTable.Rows)
            {
                label6.Text = row["Patient_ID"].ToString();
                label7.Text = row["Name"].ToString();
                label8.Text = row["Medicine"].ToString();
                label2.Text = row["Message"].ToString();
            }
        }
    }
}
