using System;
using System.Configuration;
using System.Data.SqlClient;
class AdoConnection
{
    public string connectionString;
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
            Console.ReadKey();
        }
    }
    public void DeleteData(int Id)
    {
        SqlConnection con = new SqlConnection(connectionString);
        string query = "DELETE FROM STUDENT WHERE id ="+Id;

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
            Console.ReadKey();
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
                          "4. Exit");
        
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
                case "2":
                    Console.WriteLine("enter the Name want to insert");
                    string Name = Console.ReadLine();
                    Console.WriteLine("enter the age want to insert");
                    int Age = Convert.ToInt16(Console.ReadLine());
                    adoObj.InsertData(Name, Age);
                    break;
                case "3":
                    Console.WriteLine("enter the Id which one you want to delete");
                    int Id = Convert.ToInt16(Console.ReadLine());
                    adoObj.DeleteData(Id);
                    break;
                case "4":
                    quit = true;
                    break;
                default:
                    break;

            }
        }
        while (!quit);

    }
}