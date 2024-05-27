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
    public partial class Form7 : Form
    {

        OracleConnection conn;
        public void ConnectDB()
        {
            conn = new OracleConnection("Data Source=DESKTOP-5SKP087;User ID=system;Password=tarapore");
            try
            {
                conn.Open();
                MessageBox.Show("Connection to Database Successful!");
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        public Form7()
        {
            InitializeComponent();
        }

        private void Form7_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string query1 = "INSERT INTO CONTACT VALUES ('"+textBox5.Text+"', '"+textBox6.Text+"')";
            string query3 = "INSERT INTO WORKS_FOR " +
                "VALUES ("+textBox1.Text+", "+textBox3.Text+", '"+textBox6.Text+ "', '"+textBox5.Text+"')";
            string query2 = "INSERT INTO STARTUP_MEMBER " +
                "VALUES ("+textBox1.Text+", '"+textBox2.Text+"', "+textBox3.Text+", TO_DATE('"+textBox4.Text+"', 'dd/MM/yyyy'), 'Unspecified', 0, 2024)";
            string query4 = "INSERT INTO LOGIN " +
                "VALUES ('"+textBox5.Text+"', '"+textBox4.Text+ "', '"+textBox5.Text+"', '"+textBox6.Text+"', 5)";

            try
            {
                ConnectDB();

                using (OracleCommand cmd1 = conn.CreateCommand())
                {
                    cmd1.CommandText = query1;
                    cmd1.ExecuteNonQuery();
                }

                using (OracleCommand cmd2 = conn.CreateCommand())
                {
                    cmd2.CommandText = query2;
                    cmd2.ExecuteNonQuery();
                }

                using (OracleCommand cmd3  = conn.CreateCommand())
                {
                    cmd3.CommandText = query3;
                    cmd3.ExecuteNonQuery();
                }

                using (OracleCommand cmd4 = conn.CreateCommand())
                {
                    cmd4.CommandText = query4;
                    int rowsAffected = cmd4.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Registration Successful!\n" +
                            "Login Details: \n" +
                            "USERNAME: " + textBox5.Text +
                            "PASSWORD: " + textBox4.Text);
                    }
                    else
                    {
                        MessageBox.Show("Registration Unsuccessful! Try again.");
                    }
                }

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
