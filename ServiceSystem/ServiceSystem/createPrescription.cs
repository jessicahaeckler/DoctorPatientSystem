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
            //fill listbox1 with patient names
            patientList = Patient.getPatientList();
            listBox1.Items.Clear();
            for (int i = 0; i < patientList.Count; i++)
            {
                Patient temp = (Patient)patientList[i];
                listBox1.Items.Add(temp.getID()+" "+temp.getName());
            }
        }

        //back button
        private void Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            doctorMainMenu docMain = new doctorMainMenu();
            docMain.ShowDialog();
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string text = listBox1.GetItemText(listBox1.SelectedItem);
                String[] tokens = text.Split(' ');
                pid = Int32.Parse(tokens[0]);//gets appointment id from listbox
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                //insert prescription data into database
                conn.Open();
                string insert = "Insert into fjs_prescriptions (Patient_ID, Medicine, Dosage, Refills, Status, Instructions) Values(@val1, @val2, @val3,@val4, @val5, @val6)";
                MySqlCommand com = new MySqlCommand(insert, conn);
                com.Parameters.AddWithValue("@val1", pid);
                com.Parameters.AddWithValue("@val2", textBox1.Text);
                com.Parameters.AddWithValue("@val3", Int32.Parse(textBox2.Text));
                com.Parameters.AddWithValue("@val4", Int32.Parse(textBox4.Text));
                com.Parameters.AddWithValue("@val5", "Waiting");
                com.Parameters.AddWithValue("@val6", textBox3.Text);
                com.Prepare();
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
