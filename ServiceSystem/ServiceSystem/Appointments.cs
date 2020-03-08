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
    public partial class Appointments : Form
    {
        int APid;
        public Appointments()
        {
            InitializeComponent();
            DataTable myTable = new DataTable();
            //connect to database
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                string n = "New"; //variable to hold appointment status
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Name, Appointment_ID, Reason FROM fjs_patient p JOIN fjs_Appointments a ON p.Patient_ID = a.Patient_ID WHERE Status='"+n+ "'ORDER BY Appointment_ID";
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
            //display name and appointment id in listbox
            foreach (DataRow row in myTable.Rows)
            {
                listBox1.Items.Add(row["Appointment_ID"] + " "+row["Name"]);
            }
            dateTimePicker2.Format = DateTimePickerFormat.Time;
            dateTimePicker2.CustomFormat = "HH:mm tt";
            dateTimePicker2.ShowUpDown = true;
        }

        //code for back button
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            doctorMainMenu docMain = new doctorMainMenu();
            docMain.ShowDialog();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //gets appointment id from listbox
            if (listBox1.SelectedItem != null){
                string text = listBox1.GetItemText(listBox1.SelectedItem);
                String[] tokens = text.Split(' ');
                APid = Int32.Parse(tokens[0]);
            }
            DataTable myTable = new DataTable();
            //display reason for selected appointment
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("1Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT Reason FROM fjs_Appointments WHERE Appointment_ID='" + APid +"'";
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
                textBox1.Text = row["Reason"].ToString();
            }
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)

        { 
            string status = "Confirmed";//variable to hold the status value
            //insert date, time, status, and reason into database
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "UPDATE fjs_appointments SET Date ='" + dateTimePicker1.Value.ToString("yyyy-MM-dd") + "', Time ='" + dateTimePicker2.Value.TimeOfDay.ToString()+ "', Status = '"+status+"', Reason = @reason WHERE Appointment_ID = '"+APid+"'";
                MySqlCommand com = new MySqlCommand(insert, conn);
                com.Parameters.AddWithValue("@reason", textBox1.Text);
                com.Prepare();
                com.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            listBox1.Items.Remove(listBox1.SelectedItem);
            textBox1.Text = " ";
        }

    }
}
