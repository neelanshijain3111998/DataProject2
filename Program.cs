using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using CsvHelper;
using System.Globalization;
using ConsoleTables;
namespace project2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (string.Equals("load", args[0]))
                {
                    bool DatabaseExist = false;

                    string connectionString = "Server=LAPTOP-AS3FK0E2; Initial catalog=master;integrated security=True";
                    SqlConnection conn = new SqlConnection(connectionString);

                    string command1 = "SELECT * FROM master.dbo.sysdatabases where name ='neel2'";
                    SqlCommand sqlCmd1 = new SqlCommand(command1, conn);
                    conn.Open();

                    SqlDataReader dr = sqlCmd1.ExecuteReader();
                    DatabaseExist = dr.HasRows;
                    conn.Close();
                    if (DatabaseExist)
                    {
                        Console.WriteLine("DataBase Already exist");
                    }
                    else
                    {
                        Console.WriteLine("Initially DATABASE is not exist");
                        string sqll = "CREATE DATABASE neel2";
                        SqlCommand sqlCm = new SqlCommand(sqll, conn);
                        conn.Open();
                        sqlCm.ExecuteNonQuery();
                        conn.Close();
                        Console.WriteLine("Now database is created");
                    }
                    
                    string command2 = "SELECT count(*) from Information_schema.Tables where TABLE_name ='Rajasthan'";
                    SqlCommand sqlCmd2 = new SqlCommand(command2, conn);
                    conn.Open();

                    SqlDataReader drr = sqlCmd2.ExecuteReader();

                        string command3 = "Use neel2";
                        string command4 = "Create table Rajasthan("
                        + "CORPORATE_IDENTIFICATION_NUMBER varchar(200),"
                        + "Company_Name varchar(200),"
                        + "Company_status varchar(200),"
                        + "Company_class varchar(200),"
                        + "Company_Category varchar(200),"
                        + "Company_sub_category varchar(200),"
                        + "DATE_OF_REGISTRATION DateTime,"
                        + "REGISTERED_STATE varchar(200),"
                        + "AUTHORIZED_CAP BigInt,"
                        + "PAIDUP_CAPITAL varchar(200),"
                        + "Industrial_Class varchar(200),"
                        + "PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN varchar(200),"
                        + "Registered_Office_Address varchar(200),"
                        + "REGISTRAR_OF_COMPANIES varchar(200),"
                        + "EMAIL_ADDR varchar(200),"
                        + "Latest_Year_AR varchar(200),"
                        + "Latest_Year_BS varchar(200))";
                        conn.Close();

                        SqlCommand sqlCmd3 = new SqlCommand(command3, conn);
                        conn.Open();
                        sqlCmd3.ExecuteNonQuery();

                        SqlCommand sqlCmd4 = new SqlCommand(command4, conn);
                        sqlCmd4.ExecuteNonQuery();
                        conn.Close();
                        Console.WriteLine("table created");

                        var reader = new StreamReader(@"C:\Users\yogen\source\repos\project2\Rajasthan.csv");
                        var rows = new CsvReader(reader, CultureInfo.InvariantCulture);
                        foreach (var row in rows.GetRecords<RajasthanFile>())
                        {
                            string command5 = "insert into neel2.dbo.Rajasthan values(@CorporateNo, @CompanyName," +
                            " @CompanyStatus, @CompanyClass," +
                            " @CompanyCategory, @CompanySub, @DateOfRegistration," +
                            "@RegisterState, @AuthorizedCap, @PaidUp, @InduClass, " +
                            "@PrincipalActivity, @RegisterOffice, @regis, @Email, @LYrA, @LyrB)";

                            SqlCommand sqlCmd5 = new SqlCommand(command5, conn);
                            sqlCmd5.Parameters.Add("@CorporateNo", SqlDbType.VarChar, 200).Value = row.CORPORATE_IDENTIFICATION_NUMBER;
                            sqlCmd5.Parameters.Add("@CompanyName", SqlDbType.VarChar, 200).Value = row.Company_Name;
                            sqlCmd5.Parameters.Add("@CompanyStatus", SqlDbType.VarChar, 200).Value = row.Company_status;
                            sqlCmd5.Parameters.Add("@CompanyClass", SqlDbType.VarChar, 200).Value = row.Company_class;
                            sqlCmd5.Parameters.Add("@CompanyCategory", SqlDbType.VarChar, 200).Value = row.Company_Category;
                            sqlCmd5.Parameters.Add("@CompanySub", SqlDbType.VarChar, 200).Value = row.Company_sub_category;
                            sqlCmd5.Parameters.Add("@DateOfRegistration", SqlDbType.DateTime).Value = row.DATE_OF_REGISTRATION;
                            sqlCmd5.Parameters.Add("@RegisterState", SqlDbType.VarChar, 200).Value = row.REGISTERED_STATE;
                            sqlCmd5.Parameters.Add("@AuthorizedCap", SqlDbType.BigInt).Value = row.AUTHORIZED_CAP;
                            sqlCmd5.Parameters.Add("@PaidUp", SqlDbType.VarChar, 200).Value = row.PAIDUP_CAPITAL;
                            sqlCmd5.Parameters.Add("@InduClass", SqlDbType.VarChar, 200).Value = row.Industrial_Class;
                            sqlCmd5.Parameters.Add("@PrincipalActivity", SqlDbType.VarChar, 200).Value = row.PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN;
                            sqlCmd5.Parameters.Add("@RegisterOffice", SqlDbType.VarChar, 200).Value = row.Registered_Office_Address;
                            sqlCmd5.Parameters.Add("@regis", SqlDbType.VarChar, 200).Value = row.REGISTRAR_OF_COMPANIES;
                            sqlCmd5.Parameters.Add("@Email", SqlDbType.VarChar, 200).Value = row.EMAIL_ADDR;
                            sqlCmd5.Parameters.Add("@LYrA", SqlDbType.VarChar, 200).Value = row.Latest_Year_AR;
                            sqlCmd5.Parameters.Add("@LyrB", SqlDbType.VarChar, 200).Value = row.Latest_Year_BS;
                            conn.Open();
                            sqlCmd5.ExecuteNonQuery();
                            conn.Close();
                        }

                    
                }

                if (string.Equals("analyzer", args[0]))
                {
                  int x = 5;
                    do
                    {
                        Console.WriteLine("\n-----Data Project 2: Company Master------");
                        Console.WriteLine("Enter 1-For TestCase1 ");
                        Console.WriteLine("Enter 2-For TestCase2 ");
                        Console.WriteLine("Enter 3-For TestCase3 ");
                        Console.WriteLine("Enter 4-For TestCase4 ");
                        Console.WriteLine("Enter 5-For Exit ");
                        Console.Write("Enter your choice:");
                        x = Convert.ToInt32(Console.ReadLine());
                        switch (x)
                        {
                            case 1:
                                Testcase1();
                                break;
                            case 2:
                                Testcase2();
                                break;
                            case 3:
                                Testcase3();
                                break;
                            case 4:
                                Testcase4();
                                break;
                            case 5:
                                x = 5;
                                Console.WriteLine("Thank You !!");
                                break;
                            default:
                                Console.WriteLine("please enter valid choice");
                                break;
                        }


                    }
                    while (x != 5);
     
                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("You Entered Wrong");
            }

        }
        static void Testcase1()
        {
            string connectionString = "Server=LAPTOP-AS3FK0E2; Initial catalog=master;integrated security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            //testcase1
            //Method 1 
            /*
            string command6 = "select sum(case when AUTHORIZED_CAP <= 100000 then 1 else 0 end) as <=1L," +
            "sum(case when  AUTHORIZED_CAP > 100000 and AUTHORIZED_CAP <= 1000000 then 1 else 0 end) as 10L to 1Cr," +
            "sum(case when  AUTHORIZED_CAP > 1000000 and AUTHORIZED_CAP <= 10000000 then 1 else 0 end) as 1Cr to 10Cr, " +
            "sum(case when AUTHORIZED_CAP > 10000000 and AUTHORIZED_CAP <= 100000000 then 1 else 0 end) as >10Cr," +
            "sum(case when AUTHORIZED_CAP > 100000000 then 1 else 0 end) as range5 from neel2.dbo.Rajasthan";
            SqlCommand sqlCmd6 = new SqlCommand(command6, conn);
            conn.Open();
            SqlDataReader reader = sqlCmd6.ExecuteReader();
            var table = new ConsoleTable("Bin", "Counts");
            reader.Read();
            Console.WriteLine("\nTestcase1 output");
            table.AddRow("<=1L", reader.GetValue(0)).AddRow("1L to 10L", reader.GetValue(1))
                    .AddRow("10L to 1Cr", reader.GetValue(2))
                    .AddRow("1Cr to 10Cr", reader.GetValue(3))
                    .AddRow(">10Cr", reader.GetValue(4));
                //  Console.WriteLine("{0}\t{1}", reader.GetValue(0), reader.GetValue(1));






                table.Write();
                conn.Close();
            }


            */
            //OR
            //Method 2

            string command6 = "select count(*) from neel2.dbo.Rajasthan where AUTHORIZED_CAP <= 100000";
            string command7 = "select count(*) from neel2.dbo.Rajasthan where AUTHORIZED_CAP > 100000 and AUTHORIZED_CAP<= 1000000";
            string command8 = "select count(*) from neel2.dbo.Rajasthan where AUTHORIZED_CAP > 1000000 and AUTHORIZED_CAP<= 10000000";
            string command9 = "select count(*) from neel2.dbo.Rajasthan where AUTHORIZED_CAP > 10000000 and AUTHORIZED_CAP<= 100000000";
            string command10 = "select count(*) from neel2.dbo.Rajasthan where AUTHORIZED_CAP > 100000000";

            SqlCommand sqlCmd6 = new SqlCommand(command6, conn);
            SqlCommand sqlCmd7 = new SqlCommand(command7, conn);
            SqlCommand sqlCmd8 = new SqlCommand(command8, conn);
            SqlCommand sqlCmd9 = new SqlCommand(command9, conn);
            SqlCommand sqlCmd10 = new SqlCommand(command10, conn);
            conn.Open();
            Console.WriteLine("\nTestcase1 output");
            var table = new ConsoleTable("Bin", "Counts");
            table.AddRow("<=1L", sqlCmd6.ExecuteScalar())
            .AddRow("1L to 10L", sqlCmd7.ExecuteScalar())
            .AddRow("10L to 1Cr", sqlCmd8.ExecuteScalar())
            .AddRow("1Cr to 10Cr", sqlCmd9.ExecuteScalar())
            .AddRow(">10Cr", sqlCmd10.ExecuteScalar());
            table.Write();
            conn.Close();



        }    
    static void Testcase2()
        {
            //testcase2
            string connectionString = "Server=LAPTOP-AS3FK0E2; Initial catalog=master;integrated security=True";
            SqlConnection conn = new SqlConnection(connectionString);

            Console.WriteLine("\nTestcase2 output");

            var table = new ConsoleTable("Year", "No Of Registrations");
            for (int i = 2000; i <= 2019; i++)
            {
                string command11 = string.Format("select count(*) from neel2.dbo.Rajasthan where year(DATE_OF_REGISTRATION)= {0}", i);
                SqlCommand sqlCmd11 = new SqlCommand(command11, conn);
                conn.Open();
                table.AddRow(i, (int)sqlCmd11.ExecuteScalar());

                conn.Close();
            }
            table.Write();
            Console.WriteLine();

        }
        static void Testcase3()
        {
            //Testcase3
            string connectionString = "Server=LAPTOP-AS3FK0E2; Initial catalog=master;integrated security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            Console.WriteLine("\nTestcase3 output");

            string command12 = "select count(PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN),PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN from neel2.dbo.Rajasthan where year(DATE_OF_REGISTRATION) = '2015' group by PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN";
            SqlCommand sqlCmd12 = new SqlCommand(command12, conn);
            var table = new ConsoleTable("PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN", "No Of Registrations");

            conn.Open();

            SqlDataReader dr = sqlCmd12.ExecuteReader();
            while (dr.Read())
            {
                table.AddRow(dr[1].ToString(), (int)dr[0]);
            }
            table.Write();
            Console.WriteLine();

        }
        static void Testcase4()
        {
            //Testcase4
            string connectionString = "Server=LAPTOP-AS3FK0E2; Initial catalog=master;integrated security=True";
            SqlConnection conn = new SqlConnection(connectionString);
            Console.WriteLine("\nTestcase4 output");
            var table = new ConsoleTable("PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN", "No Of Registrations");

            for (int i = 2000; i <= 2019; i++)
            {
                string command13 = string.Format("select count(PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN),PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN from neel2.dbo.Rajasthan where year(DATE_OF_REGISTRATION) = {0} group by PRINCIPAL_BUSINESS_ACTIVITY_AS_PER_CIN", i);
                SqlCommand sqlCmd13 = new SqlCommand(command13, conn);
                table.AddRow(i, " ");
                conn.Open();
                SqlDataReader dr = sqlCmd13.ExecuteReader();

                while (dr.Read())
                {

                    table.AddRow(dr[1].ToString(), (int)dr[0]);
                }
                conn.Close();


            }
            table.Write();
            Console.WriteLine();



        }
    }
}