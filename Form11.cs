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
    public partial class Form11 : Form
    {

        OracleConnection conn;

        public void ConnectDB()
        {
            conn = new OracleConnection("Data Source=DESKTOP-5SKP087;User ID=system;Password=tarapore");
            try
            {
                conn.Open();
                //MessageBox.Show("Connection to Database Successful!");
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public Form11()
        {
            InitializeComponent();
        }

        private void Form11_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                string query1 = "INSERT INTO TRANSACTIONS VALUES ("+textBox1.Text+", TO_DATE('"+textBox2.Text+"', 'dd/MM/yyyy'), '"+textBox3.Text+"', "+textBox4.Text+")";

                OracleCommand cmd1 = new OracleCommand(query1, conn);

                int rowsAffected = cmd1.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    MessageBox.Show("Transaction Logged successfully!");
                }
                else
                {
                    MessageBox.Show("Transaction Logging failed!");
                }

                string query2 = "INSERT INTO PROJECT_TRANSACTIONS VALUES ("+textBox1.Text+", "+textBox5.Text+")";

                OracleCommand cmd2 = new OracleCommand( query2, conn);

                cmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
