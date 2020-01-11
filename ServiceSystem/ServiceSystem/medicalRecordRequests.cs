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
    public partial class medicalRecordRequests : Form
    {

        string pID;
        ArrayList patientList;
        public medicalRecordRequests()
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

        private void button2_Click(object sender, EventArgs e)
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
            pID = tokens[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string status = "Pending";
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                conn.Open();
                string insert = "INSERT INTO fjs_medical_record_acess (Aproval_Status, Doctor_ID, Patient_ID) VALUES('"+status+"','"+1+"','"+pID+"')"; //"UPDATE fjs_medical_record_access SET Aproval_Status ='" + "Pending" + "', Doctor_ID ='"+"1"+"' WHERE Appointment_ID = '"+APid+"'";
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
