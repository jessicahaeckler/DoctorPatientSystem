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
    public partial class doctorMainMenu : Form
    {
        public doctorMainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Appointments app = new Appointments();
            app.ShowDialog();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            medicalRecords medRec = new medicalRecords();
            medRec.ShowDialog();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            medicalRecordRequests MedReq = new medicalRecordRequests();
            MedReq.ShowDialog();
            this.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            refillRequests refillReq = new refillRequests();
            refillReq.ShowDialog();
            this.Close();
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            requestedCalls callReq = new requestedCalls();
            callReq.ShowDialog();
            this.Close();
        }

        private void Button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            createPrescription docPresc = new createPrescription();
            docPresc.ShowDialog();
            this.Close();
        }
    }
}
