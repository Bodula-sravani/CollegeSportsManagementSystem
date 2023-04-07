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

        public void CreateTournmentsTable()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "create table Tournments(Id int primary key,Name varchar(255) not null,sports_id int not null," +
                    "foreign key(sports_id) REFERENCES Sports(Id)" +
                    "on delete cascade);";
                command.ExecuteNonQuery();
                Console.WriteLine("Tournments Table created");
            }

        }

        public void CreateTeamsTable()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "create table Teams(Id int primary key,Name varchar(255) not null,tournment_id int not null," +
                    "foreign key(tournment_id) REFERENCES Tournments(Id)" +
                    "on delete cascade);";
                command.ExecuteNonQuery();
                Console.WriteLine("Teams Table created");


            }

        }

        public void CreatePlayersTable() 
        {
           using(SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "create table Players(Id int primary key,Name varchar(255) not null,team_id int not null," +
                    "foreign key(team_id) REFERENCES Teams(Id)" +
                    "on delete cascade);";
                command.ExecuteNonQuery();
                Console.WriteLine("Players Table created");


            }

        }


        static void Main(string[] args)
        {
            CollegeSportsManagementSystem c = new();
            c.connection = new SqlConnection(c.connectionString);
            c.connection.Open();
            //c.createSportsTable();
            //c.CreateTournmentsTable();
            c.CreateTeamsTable();
            c.CreatePlayersTable();
            c.connection.Close();

        }
    }
}