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
    public partial class Form2 : Form
    {

        public static string userID;
        public static string passID;

        public static string email;
        public static string phoneNo;

        public static int adminID;
        public static string adminName;
        public static string dob;

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

        public void IdentifyUser()
        {
            try
            {
                ConnectDB();

                string query1 = "SELECT email, phoneNo FROM LOGIN WHERE username = '"+userID+"' AND password = '"+passID+"'";

                using (OracleCommand  cmd = new OracleCommand(query1, conn))
                    using (OracleDataReader rdr = cmd.ExecuteReader())
                {
                    if (rdr.Read())
                    {
                        email = rdr.GetString(0);
                        phoneNo = rdr.GetString(1);

                        string query2 = "SELECT adminID, adminName, dateOfBirth " +
                                        "FROM ADMINISTRATORS " +
                                        "WHERE email = '"+email+"' AND phoneNo = '"+phoneNo+"'";

                        using (OracleCommand cmd2 = new OracleCommand(query2, conn))
                            using (OracleDataReader rdr1 = cmd2.ExecuteReader())
                        {
                            if (rdr1.Read())
                            {
                                adminID = rdr1.GetInt32(0);
                                adminName = rdr1.GetString(1);
                                dob = rdr1.GetString(2);
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            userID = Form1.userID;
            passID = Form1.passID;

            IdentifyUser();

            label7.Text = adminID.ToString();
            label8.Text = adminName;
            label9.Text = dob;
            label10.Text = email;
            label11.Text = phoneNo;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                string query1 = "SELECT * " +
                                "FROM STARTUPS " +
                                "WHERE startupID = "+textBox2.Text+"";

                using (OracleCommand cmd1 = new OracleCommand(query1, conn))
                    using (OracleDataReader reader = cmd1.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBox5.Text = reader.GetInt32(0).ToString();
                        textBox6.Text = reader.GetString(1);
                        textBox7.Text = reader.GetInt32(2).ToString();
                        textBox8.Text = reader.GetString(3);
                        textBox9.Text = reader.GetString(4);
                        textBox10.Text = reader.GetInt32(5).ToString();
                        textBox11.Text = reader.GetString(6);
                        textBox12.Text = reader.GetString(7);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                string query1 = "SELECT * FROM INVESTORS WHERE investorID = "+textBox1.Text+"";

                using (OracleCommand cmd1 = new OracleCommand(query1, conn))
                    using (OracleDataReader reader1 = cmd1.ExecuteReader())
                {
                    if (reader1.Read())
                    {
                        textBox15.Text = reader1.GetInt32(0).ToString();
                        textBox14.Text = reader1.GetString(1);
                        textBox13.Text = reader1.GetString(2);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                string query1 = "SELECT * FROM MENTORED_BY WHERE mentorID = "+textBox4.Text+"";

                using (OracleCommand cmd1 = new OracleCommand(query1, conn))
                    using (OracleDataReader reader1 = cmd1.ExecuteReader())
                {
                    if (reader1.Read())
                    {
                        textBox19.Text = reader1.GetInt32(0).ToString();
                        textBox17.Text = reader1.GetInt32(1).ToString();
                        textBox18.Text = reader1.GetString(2); 
                        textBox16.Text = reader1.GetString(3);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                string query1 = "DELETE FROM STARTUPS WHERE startupID = "+textBox3.Text+"";

                OracleCommand cmd1 = new OracleCommand(query1 , conn);

                cmd1.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conn.Close(); }
        }
    }
}
