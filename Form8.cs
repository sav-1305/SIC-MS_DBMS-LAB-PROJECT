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
    public partial class Form8 : Form
    {
        OracleConnection conn;
        public void ConnectDB()
        {
            conn = new OracleConnection("Data Source=DESKTOP-5SKP087;User ID=system;Password=tarapore");
            conn.Open();
        }

        public Form8()
        {
            InitializeComponent();
            ConnectDB();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void Form8_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string email = textBox1.Text;
                string contact = textBox2.Text;
                string new_pw = textBox3.Text;

                string str = "UPDATE login SET password =:new_password WHERE email =:email_address AND phoneNo =:contact_no";

                OracleCommand cmd1 = new OracleCommand(str, conn);

                cmd1.Parameters.Add("new_password", OracleDbType.Varchar2).Value = new_pw;
                cmd1.Parameters.Add("email_address", OracleDbType.Varchar2).Value = email;
                cmd1.Parameters.Add("contact_no", OracleDbType.Varchar2).Value = contact;

                if (textBox4.Text == new_pw)
                {
                    int rowsAffected = cmd1.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Password updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("No rows updated. Email or contact number not found.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: "+ ex.Message);
            }
        }
    }
}
