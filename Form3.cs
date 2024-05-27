using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DBMS_PROJECT_DRAFT1
{
    public partial class Form3 : Form
    {
        OracleConnection cn;

        public void ConnectDB()
        {
            cn = new OracleConnection("Data Source=DESKTOP-5SKP087;User ID=system;Password=tarapore");
            cn.Open();
            MessageBox.Show("CONNECTED");
        }

        public Form3()
        {
            InitializeComponent();
            //ConnectDB();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                using (OracleCommand cmd1 = cn.CreateCommand())
                {
                    string query1 = "INSERT INTO STARTUPS(StartupID, StartupName, numberOfEmployees, foundingDate, industry, numberOfProjects, status, location) " +
                        "VALUES(" + textBox1.Text + ", '" + textBox2.Text + "', " + textBox3.Text + ", TO_DATE('" + textBox4.Text + "', 'dd/MM/yyyy'), '" + textBox5.Text + "', 0, 'New', '" + textBox6.Text + "')";

                    cmd1.CommandText = query1;

                    int rowsAffected = cmd1.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Data inserted successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Error inserting data.");
                    }
                    cmd1.Dispose();
                }   
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);

            }
            finally
            {
                cn.Close();
            }
       
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
