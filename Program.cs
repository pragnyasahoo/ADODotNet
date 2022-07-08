using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
class AdoConnection
{
    public string connectionString =String.Empty;
    public string getconnection()
    {
          connectionString = ConfigurationManager.ConnectionStrings["EntityContext"].ConnectionString.ToString();
          return connectionString;
    }

    public void readData()
    {        
        SqlConnection con = new SqlConnection(connectionString);
        string querystring = "Select * from student";
        con.Open();
        SqlCommand cmd = new SqlCommand(querystring, con);
        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(reader[0].ToString() + " " + reader[1].ToString() + " " + reader[2].ToString());
        }
        con.Close();
    }

    public void InsertData(String Name, int age)
    {
        SqlConnection con = new SqlConnection(connectionString);
        string query = "INSERT INTO STUDENT (StudentName,Age) VALUES('" + Name + "',"+ age +")";
       
            SqlCommand cmd = new SqlCommand(query, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery(); 
            Console.WriteLine("Records Inserted Successfully");
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error Generated. Details: " + e.ToString());
        }
        finally
        {
            con.Close(); 
        }
    }
    public void DeleteData(int Id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        string query = "DELETE FROM STUDENT WHERE id =" + Id;

        SqlCommand cmd = new SqlCommand(query, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            Console.WriteLine("Records deleted Successfully");
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error Generated. Details: " + e.ToString());
        }
        finally
        {
            con.Close(); 
        }

    }

    public void UpdateData(string stuName, int Id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        string query = "UPDATE STUDENT SET STUDENTNAME='" + stuName + "'" + "WHERE id=" + Id;

        SqlCommand cmd = new SqlCommand(query, con);
        try
        {
            con.Open();
            cmd.ExecuteNonQuery();
            Console.WriteLine("Records deleted Successfully");
        }
        catch (SqlException e)
        {
            Console.WriteLine("Error Generated Details: " + e.ToString());
        }
        finally
        {
            con.Close();
        }
    }

    
        public void InsertUsingStoreProcedure(string Name, int age)
        { 
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("InsertStudentRecords", con);
            try
            {
                con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@Age", age)); 
                int i = cmd.ExecuteNonQuery();
                if (i > 0)
                {
                    Console.WriteLine("Records Inserted Successfully.");
                }

            }
            catch (SqlException e)
            {
                Console.WriteLine("Error Generated. Details: " + e.ToString());
            }
            finally
            {
                con.Close(); 
            }
        }

    //Crud Opertaion uisng in a single store Procedure using Action Parameter

    public void UpdateUsingStoreProcedure(string Name, int Id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("SpMyProcedure", con);
        try
        {
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Update");
            cmd.Parameters.Add(new SqlParameter("@Id", Id));
            cmd.Parameters.Add(new SqlParameter("@Name", Name));
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Records updated   Successfully.");
            }

        }
        catch (SqlException e)
        {
            Console.WriteLine("Error Generated. Details: " + e.ToString());
        }
        finally
        {
            con.Close();
        }
    }

    public void DeleteUsingStoreProcedure(int Id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        SqlCommand cmd = new SqlCommand("SpMyProcedure", con);
        try
        {
            con.Open();
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Action", "Delete");
            cmd.Parameters.AddWithValue("@Id", Id);
            int i = cmd.ExecuteNonQuery();
            if (i > 0)
            {
                Console.WriteLine("Records updated   Successfully.");
            }

        }
        catch (SqlException e)
        {
            Console.WriteLine("Error Generated. Details: " + e.ToString());
        }
        finally
        {
            con.Close();
        }
    }



}
class program
{

    static void Main(string[] args)
    {
        AdoConnection adoObj = new AdoConnection();
        adoObj.getconnection();
        Console.WriteLine("1. Get the Student Details" + "\n" +
                          "2. Insert student data" + "\n" +
                          "3. Delete student data" + "\n" +
                          "4. Update student data" + "\n" +
                          "5. Insert student data using store procedure" + "\n" +
                          "6. Update student data using store procedure" + "\n" +
                          "7. Delete student data using store procedure" + "\n" +
                          "8. Exit");
        
        string input;
        bool quit = false;
        do
        {
            Console.WriteLine("enter the operation");
            input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    adoObj.readData();
                    break;
                case "2" or "5":
                    Console.WriteLine("enter the Name want to insert");
                    string Name = Console.ReadLine();
                    Console.WriteLine("enter the age want to insert");
                    int Age = Convert.ToInt16(Console.ReadLine());
                    if (Name != string.Empty && Age != null && input == "2")
                    {
                        adoObj.InsertData(Name, Age);
                    }
                    else
                    {
                        adoObj.InsertUsingStoreProcedure(Name, Age);
                    }
                    break;
                case "3" or "7":
                    Console.WriteLine("enter the Id which one you want to delete");
                    int Id = Convert.ToInt16(Console.ReadLine());
                    if (Id != null && input == "3")
                    {
                        adoObj.DeleteData(Id);
                    }
                    else
                    {
                        adoObj.DeleteUsingStoreProcedure(Id);
                    }
                    break;
                case "4" or "6":
                    Console.WriteLine("enter the Id which one you want to Update");
                    int stuId = Convert.ToInt16(Console.ReadLine());
                    Console.WriteLine("enter the updated name for id "+ stuId);
                    string studName = Console.ReadLine();
                    if (studName != string.Empty && input =="4")
                    {
                        adoObj.UpdateData(studName, stuId);
                    }
                    else
                    {
                        adoObj.UpdateUsingStoreProcedure(studName, stuId);
                    }
                    break; 
                case "8":
                    quit = true;
                    break;
                default:
                    break;

            }
        }
        while (!quit);
        Console.ReadKey();
    }
}