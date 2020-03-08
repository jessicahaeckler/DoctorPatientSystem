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
        int rid;
        int pid;
        ArrayList patientList;
        public medicalRecords()
        {
            InitializeComponent();
            //display patient names and ids in listbox
            patientList = Patient.getPatientList();
            listBox1.Items.Clear();
            for (int i = 0; i < patientList.Count; i++)
            {
                Patient temp = (Patient)patientList[i];
                listBox1.Items.Add(temp.getID()+" "+temp.getName());
            }
        }

        //back button
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            doctorMainMenu docMain = new doctorMainMenu();
            docMain.ShowDialog();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //get selected patient id
            if (listBox1.SelectedItem != null)
            {
                string text = listBox1.GetItemText(listBox1.SelectedItem);
                String[] tokens = text.Split(' ');
                pid = Int32.Parse(tokens[0]);
            }
            listBox2.Items.Clear();
            //display data for selected patient in listbox2
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
            //get selected record id
            if (listBox2.SelectedItem != null)
            {
                string text = listBox2.GetItemText(listBox2.SelectedItem);
                String[] tokens = text.Split(' ');
                rid = Int32.Parse(tokens[0]);
            }
            //display data for selected record id
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Doctor_Notes FROM fjs_medical_record WHERE Record_ID ='"+rid+"'";
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
            //get selected record id
            if (listBox2.SelectedItem != null)
            {
                string text = listBox2.GetItemText(listBox2.SelectedItem);
                String[] tokens = text.Split(' ');
                rid = Int32.Parse(tokens[0]);
            }
            //update selected medical record
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "UPDATE fjs_medical_record SET Doctor_Notes = @notes WHERE Record_ID= @id";
                MySqlCommand com = new MySqlCommand(insert, conn);
                com.Parameters.AddWithValue("@id", rid);
                com.Parameters.AddWithValue("@notes", textBox1.Text);
                com.Prepare();
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        //create medical record button
        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //get selected patient id
            if (listBox1.SelectedItem != null)
            {
                string text = listBox1.GetItemText(listBox1.SelectedItem);
                String[] tokens = text.Split(' ');
                int pid = Int32.Parse(tokens[0]);
            }
            //create new medical record
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "Insert into fjs_medical_record (Patient_ID, Doctor_Notes, Date) Values(@id, @notes, @date)";
                MySqlCommand com = new MySqlCommand(insert, conn);
                com.Parameters.AddWithValue("@id", pid);
                com.Parameters.AddWithValue("@notes", textBox2.Text);
                com.Parameters.AddWithValue("@date", dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                com.Prepare();
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            panel1.Visible = false;
        }

        //cancel button
        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
    }
}
