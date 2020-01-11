using MySql.Data.MySqlClient;
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

namespace ServiceSystem
{
    public partial class medicalRecords : Form
    {
        ArrayList patientList;
        public medicalRecords()
        {
            InitializeComponent();
            patientList = Patient.getPatientList();
            listBox1.Items.Clear();
            for (int i = 0; i < patientList.Count; i++)
            {
                Patient temp = (Patient)patientList[i];
                listBox1.Items.Add(temp.getID()+" "+temp.getName());
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            doctorMainMenu docMain = new doctorMainMenu();
            docMain.ShowDialog();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            String[] tokens = text.Split(' ');
            int pid = Int32.Parse(tokens[0]);
            listBox2.Items.Clear();
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Date, Record_ID FROM fjs_medical_record WHERE Patient_ID='"+pid+"'";
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
                listBox2.Items.Add(row["Record_ID"].ToString()+" "+row["Date"].ToString());
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string text = listBox2.GetItemText(listBox2.SelectedItem);
            String[] tokens = text.Split(' ');
            int rID = Int32.Parse(tokens[0]);
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Doctor_Notes FROM fjs_medical_record WHERE Record_ID ='"+rID+"'";
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
                textBox1.Text = row["Doctor_Notes"].ToString();
            }
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            string text = listBox2.GetItemText(listBox2.SelectedItem);
            String[] tokens = text.Split(' ');
            int Rid = Int32.Parse(tokens[0]);
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "UPDATE fjs_medical_record SET Doctor_Notes = '" + textBox1.Text + "' WHERE Record_ID='" + Rid + "'";
                MySqlCommand com = new MySqlCommand(insert, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = listBox1.GetItemText(listBox1.SelectedItem);
            String[] tokens = text.Split(' ');
            int pid = Int32.Parse(tokens[0]);
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "Insert into fjs_medical_record (Patient_ID, Doctor_Notes, Date) Values('" + pid + "', '" + textBox2.Text + "', '" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "')";
                MySqlCommand com = new MySqlCommand(insert, conn);
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            panel1.Visible = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
