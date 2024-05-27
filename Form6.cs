using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBMS_PROJECT_DRAFT1
{
    public partial class Form6 : Form
    {

        OracleConnection cn2;
        public void ConnectDB()
        {
            cn2 = new OracleConnection("Data Source=DESKTOP-5SKP087;User ID=system;Password=tarapore");
            cn2.Open();
        }

        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                string query1 = "INSERT INTO CONTACT VALUES ('" + textBox6.Text + "', '" + textBox5.Text + "')";
                string query2 = "INSERT INTO MENTORS " +
                    "VALUES ("+textBox1.Text+", '"+textBox2.Text+"', 'Unspecified', 'Unspecified', 'Mentor', TO_DATE('"+textBox4.Text+ "', 'dd/MM/yyyy'))";
                string query3 = "INSERT INTO MENTORED_BY " +
                    "VALUES ("+textBox1.Text+", "+textBox3.Text+", '"+textBox5.Text+"', '"+textBox6.Text+"')";
                string query4 = "INSERT INTO LOGIN VALUES ('"+textBox6.Text+"', '"+textBox4.Text+ "', '"+textBox6.Text+"', '"+textBox5.Text+"', 2)";

                using (OracleCommand cmd1 = cn2.CreateCommand())
                {
                    cmd1.CommandText = query1;
                    cmd1.ExecuteNonQuery();
                }

                using (OracleCommand cmd2 = cn2.CreateCommand())
                {
                    cmd2.CommandText = query2;
                    cmd2.ExecuteNonQuery();
                }

                using (OracleCommand cmd3 =  cn2.CreateCommand())
                {
                    cmd3.CommandText = query3;
                    cmd3.ExecuteNonQuery();
                }

                using (OracleCommand cmd4 = cn2.CreateCommand())
                {
                    cmd4.CommandText = query4;
                    int rowsAffected = cmd4.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {

                        MessageBox.Show("Registration Successful!\n" +
                            "Login Details: \n" +
                            "USERNAME: " + textBox6.Text +
                            "\nPASSWORD: " + textBox4.Text);

                    }
                    else
                    {

                        MessageBox.Show("Error inserting Contact data.");

                    }

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                cn2.Close();
            }
        }

        private void Form6_Load(object sender, EventArgs e)
        {

        }
    }
}
