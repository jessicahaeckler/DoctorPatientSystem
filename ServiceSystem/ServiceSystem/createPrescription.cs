using MySql.Data.MySqlClient;
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

namespace ServiceSystem
{
    public partial class createPrescription : Form
    {
        int pid;
        ArrayList patientList;
        public createPrescription()
        {
            InitializeComponent();
            patientList = Patient.getPatientList();
            listBox1.Items.Clear();
            for (int i = 0; i < patientList.Count; i++)
            {
                Patient temp = (Patient)patientList[i];
                listBox1.Items.Add(temp.getName());
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            doctorMainMenu docMain = new doctorMainMenu();
            docMain.ShowDialog();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curItem = listBox1.SelectedItem.ToString();
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string sql = "SELECT Patient_ID FROM fjs_patient WHERE Name ='" + curItem +"'";
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
                pid = Int32.Parse(row["Patient_ID"].ToString());
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "Insert into fjs_prescriptions (Patient_ID, Medicine, Dosage, Refills, Status, Instructions) Values('" + pid + "', '" + textBox1.Text + "', '" + Int32.Parse(textBox2.Text) + "', '" + Int32.Parse(textBox4.Text) + "', '" + 1 + "', '" + textBox3.Text + "')";
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

        }
    }
}
