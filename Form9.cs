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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DBMS_PROJECT_DRAFT1
{

    public partial class Form9 : Form
    {

        OracleConnection conn;

        public static string userID;
        public static string passID;
        public static string email;
        public static string phoneNo;
        public static string mentorID;
        public static string startupID;

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

        public void IdentifyUser()
        {
            try
            {
                ConnectDB();

                string query1 = "SELECT email, phoneNo FROM LOGIN WHERE username = '" + userID + "' AND password = '" + passID + "'";

                using (OracleCommand cmd1 =  new OracleCommand(query1, conn))
                    using (OracleDataReader reader = cmd1.ExecuteReader()) 
                {
                    if (reader.Read())
                    {
                        email = reader.GetString(0);
                        phoneNo = reader.GetString(1);

                        string query2 = "SELECT mentorID, mentorName, expertise, commitment, role, dateofbirth, startupID " +
                                        "FROM MENTORS NATURAL JOIN MENTORED_BY " +
                                        "WHERE email = '"+email+"' AND phoneNo = '"+phoneNo+"'";

                        using (OracleCommand cmd2 =  new OracleCommand(query2, conn))
                            using (OracleDataReader reader1 = cmd2.ExecuteReader())
                        {
                            if (reader1.Read())
                            {
                                mentorID = reader1.GetString(0);
                                label7.Text = reader1.GetString(0);
                                label8.Text = reader1.GetString(1);
                                startupID = reader1.GetString(6);
                                label9.Text = reader1.GetString(6);
                                label10.Text = reader1.GetString(2);
                                label11.Text = reader1.GetString(4);
                                label12.Text = reader1.GetString(5);
                                label14.Text = reader1.GetString(3);

                            }
                        }
                    }
                }

                string query3 = "SELECT startupName, numberOfEmployees, foundingDate, industry, numberOfProjects, status, location " +
                                "FROM STARTUPS WHERE startupID = "+startupID+"";

                using (OracleCommand cmd3 =  new OracleCommand(query3, conn))
                    using (OracleDataReader reader3 = cmd3.ExecuteReader())
                {
                    if (reader3.Read())
                    {
                        label25.Text = reader3.GetString(0);
                        label24.Text = reader3.GetInt32(1).ToString();
                        label23.Text = reader3.GetString(2);
                        label22.Text = reader3.GetString(3);
                        label21.Text = reader3.GetInt32(4).ToString();
                        label19.Text = reader3.GetString(5);
                        label17.Text = reader3.GetString(6);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conn.Close(); }
        }

        public Form9()
        {
            InitializeComponent();
        }

        private void Form9_Load(object sender, EventArgs e)
        {
            userID = Form1.userID; passID = Form1.passID;
            IdentifyUser();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DateTime currentDate = DateTime.Now;

            string month = currentDate.Month.ToString();
            string year = currentDate.Year.ToString();

            try
            {
                ConnectDB();

                string query1 = @"SELECT 
                            SUM(CASE WHEN transactionType = 'Expenditure' THEN amount ELSE 0 END) AS totalExpenditure, 
                            SUM(CASE WHEN transactionType = 'Income' THEN amount ELSE 0 END) AS totalEarnings 
                        FROM 
                            TRANSACTIONS t
                        JOIN 
                            PROJECT_TRANSACTIONS pt ON t.transactionID = pt.transactionID
                        JOIN 
                            STARTUP_HAS sh ON pt.projectID = sh.projectID
                        WHERE 
                            EXTRACT(MONTH FROM t.dateOfTransaction) = :selectedMonth
                            AND EXTRACT(YEAR FROM t.dateOfTransaction) = :selectedYear
                            AND sh.startupID = :startupID";

                string query2 = @"SELECT 
                            pt.projectID,
                            p.projectTitle,
                            SUM(CASE WHEN t.transactionType = 'Expenditure' THEN t.amount ELSE 0 END) AS totalExpenditure,
                            SUM(CASE WHEN t.transactionType = 'Income' THEN t.amount ELSE 0 END) AS totalEarnings
                        FROM 
                            PROJECT_TRANSACTIONS pt
                        JOIN 
                            TRANSACTIONS t ON pt.transactionID = t.transactionID
                        JOIN 
                            PROJECTS p ON pt.projectID = p.projectID
                        JOIN 
                            STARTUP_HAS sh ON pt.projectID = sh.projectID
                        WHERE 
                            EXTRACT(MONTH FROM t.dateOfTransaction) = :selectedMonth
                            AND EXTRACT(YEAR FROM t.dateOfTransaction) = :selectedYear
                            AND sh.startupID = :startupID
                        GROUP BY 
                            pt.projectID, p.projectTitle";

                string message = "";

                using (OracleCommand cmd = new OracleCommand(query1, conn))
                {
                    cmd.Parameters.Add(":selectedMonth", OracleDbType.Int32).Value = month;
                    cmd.Parameters.Add(":selectedYear", OracleDbType.Int32).Value = year;
                    cmd.Parameters.Add(":startupID", OracleDbType.Int32).Value = startupID;

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string[] resultParts = result.ToString().Split(',');
                        if (resultParts.Length == 2)
                        {
                            message += $"Total Expenditure: {resultParts[0].Trim()}, Total Earnings: {resultParts[1].Trim()}\n";
                        }
                    }
                }

                using (OracleCommand cmd = new OracleCommand(query2, conn))
                {
                    cmd.Parameters.Add(":selectedMonth", OracleDbType.Int32).Value = month;
                    cmd.Parameters.Add(":selectedYear", OracleDbType.Int32).Value = year;
                    cmd.Parameters.Add(":startupID", OracleDbType.Int32).Value = startupID;

                    OracleDataAdapter adapter = new OracleDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    message += "Project Expenditure and Earnings:\n";
                    foreach (DataRow row in dataTable.Rows)
                    {
                        message += $"Project ID: {row["projectID"]}, Project Name: {row["projectTitle"]}, Total Expenditure: {row["totalExpenditure"]}, Total Earnings: {row["totalEarnings"]}\n";
                    }
                }

                MessageBox.Show(message, "Monthly Budget Report");

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
