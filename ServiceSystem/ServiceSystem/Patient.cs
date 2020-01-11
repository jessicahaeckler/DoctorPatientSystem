using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceSystem
{
    class Patient
    {
        int patientID;
        String name;
        String street;
        String city;
        String state;
        String country;
        String zip;
        int familyDoctorID;
        String DOB;
        public String getName()
        {
            return name;
        }
        public int getID()
        {
            return patientID;
        }
        public static ArrayList getPatientList()
        {
            ArrayList patientList = new ArrayList();  //a list to save the patient's data
            //prepare an SQL query to retrieve all the patients 
            DataTable myTable = new DataTable();
            string connStr = "server=csdatabase.eku.edu;user=stu_csc340;database=csc340_db;port=3306;password=Colonels18;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                Console.WriteLine("Connecting to MySQL...");
                conn.Open();
                string sql = "SELECT * FROM fjs_patient ORDER BY name";
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
                Patient newPatient = new Patient();
                newPatient.patientID = Int32.Parse(row["Patient_ID"].ToString());
                newPatient.name = row["Name"].ToString();
                newPatient.street = row["Street"].ToString();
                newPatient.city = row["City"].ToString();
                newPatient.state = row["State"].ToString();
                newPatient.country = row["Country"].ToString();
                newPatient.zip = row["Zip"].ToString();
                newPatient.familyDoctorID = Int32.Parse(row["Family_Doctor_ID"].ToString());
                newPatient.DOB = row["DOB"].ToString();
                patientList.Add(newPatient);
            }
            return patientList;  //return the event list
        }

    }
}
