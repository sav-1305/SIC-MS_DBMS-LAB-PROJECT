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
    public partial class Form10 : Form
    {

        public static string username;
        public static string password;
        public static string email;
        public static string phoneNo;

        public static int memberID;
        public static string memberName;
        public static string dob;
        public static string role;
        public static int yearofjoining;

        public static int startupID;
        public static string foundingDate;
        public static string industry;
        public static int numberofprojects;
        public static string status;

        public static string projectTitle;
        public static string category;
        public static string proj_foundingDate;

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

        public void IdentifyUser()
        {
            try
            {

                ConnectDB();

                string query1 = "SELECT email, phoneNo FROM LOGIN WHERE username = '"+username+"' AND password = '"+password+"'";

                using (OracleCommand cmd1 = new OracleCommand(query1, conn))
                    using (OracleDataReader reader1 = cmd1.ExecuteReader())
                {
                    if (reader1.Read())
                    {
                        email = reader1.GetString(0);
                        phoneNo = reader1.GetString(1);

                        string query2 = "SELECT memberID, startupID FROM WORKS_FOR WHERE email = '" + email + "' AND phoneNo = '" + phoneNo + "'";

                        using (OracleCommand cmd2 = new OracleCommand(query2, conn))
                            using (OracleDataReader reader2  = cmd2.ExecuteReader())
                        {
                            if (reader2.Read())
                            {
                                memberID = reader2.GetInt32(0);
                                startupID = reader2.GetInt32(1);
                                //MessageBox.Show("ASSOCIATED STARTUP: " + startupID);

                                string query3 = "SELECT startupName FROM STARTUPS WHERE startupID = "+startupID+"";

                                using (OracleCommand cmd3 =  new OracleCommand(query3, conn))
                                    using (OracleDataReader reader3 = cmd3.ExecuteReader())
                                {
                                    if (reader3.Read())
                                    {
                                        label1.Text = reader3.GetString(0);

                                        //Get all possible data

                                        string query4 = "SELECT memberName, dateofbirth, role, yearofjoining FROM STARTUP_MEMBER WHERE memberID = "+memberID+"";

                                        using (OracleCommand cmd4 =  new OracleCommand(query4, conn))
                                            using (OracleDataReader reader4 = cmd4.ExecuteReader())
                                        {
                                            if (reader4.Read())
                                            {
                                                memberName = reader4.GetString(0);
                                                dob = reader4.GetString(1);
                                                role = reader4.GetString(2);
                                                yearofjoining = reader4.GetInt32(3);

                                                //MessageBox.Show("Member Data retrieved successfully!");
                                            }
                                        }

                                        string query5 = "SELECT foundingDate, industry, numberofprojects, status FROM STARTUPS WHERE startupID = "+startupID+"";

                                        using (OracleCommand cmd5 = new OracleCommand(query5, conn))
                                            using (OracleDataReader reader5 = cmd5.ExecuteReader())
                                        {
                                            if (reader5.Read())
                                            {
                                                foundingDate = reader5.GetString(0);
                                                industry = reader5.GetString(1);
                                                numberofprojects = reader5.GetInt32(2);
                                                status = reader5.GetString(3);

                                                //MessageBox.Show("Startup Data retrieved successfully!");
                                            }
                                        }

                                    }
                                }

                            }
                            else
                            {
                                MessageBox.Show("No startupID found for this user.");
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

        public Form10()
        {
            InitializeComponent();
        }

        private void Form10_Load(object sender, EventArgs e)
        {
            username = Form1.userID;
            password = Form1.passID;

            IdentifyUser();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                label2.Text = "STARTUP ID:";
                label3.Text = "FOUNDING DATE:";
                label4.Text = "INDUSTRY:";
                label5.Text = "NUMBER OF PROJECTS:";
                label6.Text = "STATUS:";

                label7.Text = startupID.ToString();
                label8.Text = foundingDate;
                label9.Text = industry;
                label10.Text = numberofprojects.ToString();
                label11.Text = status.ToString();
            }
            else if (comboBox1.SelectedIndex == 1)
            {
                label2.Text = "MEMBER ID:";
                label3.Text = "MEMBER NAME:";
                label4.Text = "D.O.B (dd/mm/yy):";
                label5.Text = "ROLE:";
                label6.Text = "YEAR OF JOINING:";

                label7.Text = memberID.ToString();
                label8.Text = memberName;
                label9.Text = dob;
                label10.Text = role;
                label11.Text = yearofjoining.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                string query1 = "SELECT projectTitle, category, foundingDate FROM PROJECTS WHERE projectID = "+textBox1.Text+"";

                using (OracleCommand cmd1 = new OracleCommand(query1, conn))
                    using (OracleDataReader reader1 = cmd1.ExecuteReader())
                {
                    if (reader1.Read())
                    {
                        projectTitle = reader1.GetString(0);
                        category = reader1.GetString(1);
                        proj_foundingDate = reader1.GetString(2);

                        MessageBox.Show("\t\t" +projectTitle + "\nProject ID: " + textBox1.Text + "\nCategory: " + category + "\nFounding Date: " + proj_foundingDate);
                    }
                    else
                    {
                        MessageBox.Show("Error: Project does not exist with ID: " + textBox1.Text);
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

        private void button5_Click(object sender, EventArgs e)
        {
            string month = textBox3.Text.Substring(0, 2);
            string year = textBox3.Text.Substring(3, 4);

            try
            {
                ConnectDB();

                string query1 = "SELECT " +
                                    "SUM(CASE WHEN transactionType = 'Expenditure' THEN amount ELSE 0 END) AS totalExpenditure, " +
                                    "SUM(CASE WHEN transactionType = 'Income' THEN amount ELSE 0 END) AS totalEarnings " +
                                "FROM TRANSACTIONS t " +
                                "JOIN PROJECT_TRANSACTIONS pt ON t.transactionID = pt.transactionID " +
                                "JOIN STARTUP_HAS sh ON pt.projectID = sh.projectID " +
                                "WHERE EXTRACT(MONTH FROM t.dateOfTransaction) = '"+month+"' " +
                                "AND EXTRACT(YEAR FROM t.dateOfTransaction) = '"+year+"' " +
                                "AND sh.startupID = "+startupID+"";

                using (OracleCommand cmd1 = new OracleCommand(query1, conn))
                    using (OracleDataReader reader1 = cmd1.ExecuteReader())
                {
                    if (reader1.Read())
                    {
                        MessageBox.Show("Total Expenditure: " + reader1.GetString(0) + "\nTotal Earnings: " + reader1.GetString(1));
                    }
                    else
                    {
                        MessageBox.Show("No Data Found!");
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

        private void button3_Click(object sender, EventArgs e)
        {
            Form11 logtransaction = new Form11();
            logtransaction.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();

                string query1 = "SELECT t.transactionID, t.dateOfTransaction, t.transactionType, t.amount " +
                                "FROM TRANSACTIONS t " +
                                "JOIN PROJECT_TRANSACTIONS pt ON t.transactionID = pt.transactionID " +
                                "JOIN STARTUP_HAS sh ON pt.projectID = sh.projectID " +
                                "WHERE t.dateOfTransaction = TO_DATE('"+textBox2.Text+"', 'DD/MM/YYYY') " +
                                "AND sh.startupID = "+startupID+"";

                using (OracleCommand cmd1 = new OracleCommand(query1, conn))
                    using (OracleDataAdapter adp1 =  new OracleDataAdapter(cmd1))
                {
                    DataSet ds = new DataSet();

                    adp1.Fill(ds);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            sb.AppendLine($"Transaction ID: {row["transactionID"]}, Date: {row["dateOfTransaction"]}, Type: {row["transactionType"]}, Amount: {row["amount"]}");
                        }

                        MessageBox.Show(sb.ToString(), "Transactions for Selected Date");
                    }
                    else
                    {
                        MessageBox.Show("No transactions found for the selected date.", "No Transactions");
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form12 createProject = new Form12();
            createProject.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string query1 = @"SELECT i.investorName
                             FROM INVESTORS i
                             JOIN INVESTED_IN ii ON i.investorID = ii.investorID
                             WHERE ii.startupID = "+startupID+"";

            try
            {
                StringBuilder result = new StringBuilder();
                ConnectDB();

                using (OracleCommand cmd1 = new OracleCommand(query1, conn))
                    using (OracleDataReader reader = cmd1.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        result.AppendLine(reader.GetString(0));
                    }
                }

                MessageBox.Show(result.ToString(), "Investors for Startup");

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

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {
                ConnectDB();
                StringBuilder result = new StringBuilder();

                string query1 = "SELECT mentorName, phoneNo, email " +
                                "FROM MENTORS NATURAL JOIN MENTORED_BY " +
                                "WHERE mentorID = "+textBox4.Text+"";

                using (OracleCommand cmd1 = new OracleCommand( query1, conn))
                    using (OracleDataReader reader = cmd1.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        MessageBox.Show("NAME: " + reader.GetString(0) + "\nPHONE NO: " + reader.GetString(1) + "\nEMAIL: " + reader.GetString(2), "Mentor Information");
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally { conn.Close(); }
        }
    }

}