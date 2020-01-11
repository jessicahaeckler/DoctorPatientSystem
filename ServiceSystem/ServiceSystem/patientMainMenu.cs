using System;
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
    public partial class patientMainMenu : Form
    {
        public patientMainMenu()
        {
            InitializeComponent();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            patientDoctorRequests patReq = new patientDoctorRequests();
            patReq.ShowDialog();
            this.Close();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            requestCall patCall = new requestCall();
            patCall.ShowDialog();
            this.Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            patientSelfRecords patRec = new patientSelfRecords();
            patRec.ShowDialog();
            this.Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            patientRefills patRefill = new patientRefills();
            patRefill.ShowDialog();
            this.Close();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            requestAppointment patApp = new requestAppointment();
            patApp.ShowDialog();
            this.Close();
        }
    }
}
