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
    public partial class Form1 : Form
    {

        public static string userID;
        public static string passID;

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

        public void ChooseDashboard()
        {
            try
            {
                ConnectDB();

                string dashquery = "SELECT usertype FROM LOGIN WHERE username = '"+userID+"' AND password = '"+passID+"'";

                using (OracleCommand cmd2 = new OracleCommand(dashquery, conn))
                {
                    using (OracleDataReader reader = cmd2.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int usertype = reader.GetInt32(0);

                            switch(usertype)
                            {
                                case 1:
                                    //MessageBox.Show("Welcome Admin.");
                                    Form2 adminDash = new Form2();
                                    adminDash.Show();
                                    break;
                                case 2:
                                    //MessageBox.Show("Welcome Mentor.");
                                    Form9 mentorDash = new Form9();
                                    mentorDash.Show();
                                    break;
                                case 5:
                                    //MessageBox.Show("Welcome Member.");
                                    Form10 startupDash = new Form10();
                                    startupDash.Show();
                                    break;
                                default:
                                    MessageBox.Show("User Category not recognised! Please contact Administrators.");
                                    break;
                            }

                        }
                    }
                }
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Error: " + ex1.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form8 forgotPassword = new Form8();
            //conn.Close();

            forgotPassword.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form7 memberReg = new Form7();
            //conn.Close();

            memberReg.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form3 startupReg = new Form3();
            //conn.Close();

            startupReg.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form6 mentorReg = new Form6(); 
            //conn.Close();

            mentorReg.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                string userName = textBox1.Text.Trim();
                string passWord = textBox2.Text.Trim();

                string str = "SELECT * FROM LOGIN WHERE username=:user_name AND password=:pass_word";

                using (OracleCommand cmd1 = new OracleCommand(str, conn))
                {
                    cmd1.Parameters.Add("user_name", OracleDbType.Varchar2).Value = userName;
                    cmd1.Parameters.Add("pass_word", OracleDbType.Varchar2).Value = passWord;

                    using (OracleDataReader reader = cmd1.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            userID = userName;
                            passID = passWord;

                            MessageBox.Show("Login successful!");

                            ChooseDashboard();
                        }
                        else
                        {
                            MessageBox.Show("Invalid username or password.");
                        }
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
