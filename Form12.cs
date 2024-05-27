using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DBMS_PROJECT_DRAFT1
{
    public partial class Form12 : Form
    {

        OracleConnection conn;

        public void ConnectDB()
        {
            conn = new OracleConnection("Data Source=DESKTOP-5SKP087;User ID=system;Password=tarapore");
            try
            {
                conn.Open();
                //MessageBox.Show("Connected to Database Successful!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to connect to database. Error: " + ex.Message);
            }
        }

        public Form12()
        {
            InitializeComponent();
        }

        private void Form12_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                string query1 = "INSERT INTO PROJECTS VALUES (" + textBox1.Text + ", '" + textBox2.Text + "', '" + textBox3.Text + "', TO_DATE('" + textBox4.Text + "', 'dd/MM/yyyy'))";

                OracleCommand cmd1 = new OracleCommand(query1, conn);

                cmd1.ExecuteNonQuery();

                MessageBox.Show("Project details Entered!");

                string query2 = "INSERT INTO STARTUP_HAS VALUES ("+textBox1.Text+", "+Form10.startupID+")";

                OracleCommand cmd2 = new OracleCommand(query2, conn);

                cmd2.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
