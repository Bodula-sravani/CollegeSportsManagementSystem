using Microsoft.Data.SqlClient;
namespace CricketTorunment
{
    internal class CollegeSportsManagementSystem
    {
        public string connectionString = "Data Source=DESKTOP-8C83AOT;Initial Catalog=CollegeSportsTorunments;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        SqlConnection connection = new SqlConnection();

       

        public void createSportsTable()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "create table Sports(Id int primary key,Name varchar(255) not null);";
                command.ExecuteNonQuery();
                Console.WriteLine("Sports Table created");
            }


        }


        static void Main(string[] args)
        {
            CollegeSportsManagementSystem c = new();
            c.connection = new SqlConnection(c.connectionString);
            c.connection.Open();
            c.createSportsTable();
            c.connection.Close();

        }
    }
}