using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace ServiceSystem
{
    public partial class requestedCalls : Form
    {
        public requestedCalls()
        {
            string status = "New";
            InitializeComponent();
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Name, Request_ID, Phone_Number FROM fjs_patient p JOIN fjs_call_request r ON p.Patient_ID=r.Patient_ID WHERE Status = '"+status+ "' ORDER BY Request_ID";
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
            //convert the retrieved data to events and save them to the list
            foreach (DataRow row in myTable.Rows)
            {
                listBox1.Items.Add(row["Request_ID"].ToString()+" "+ row["Name"].ToString()+" "+row["Phone_Number"].ToString());
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            doctorMainMenu docMain = new doctorMainMenu();
            docMain.ShowDialog();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            String[] tokens = text.Split(' ');
            int rID = Int32.Parse(tokens[0]);
            string status = "Taken";
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "UPDATE fjs_call_request SET Status ='" + status + "' WHERE Request_ID = " + rID;
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
