using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;

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

        public void CreateMatchesTable()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "create table Matches(Id int primary key,Name varchar(255) not null,tournment_id int not null,team1_id int not null,team2_id int not null," +
                    "foreign key(tournment_id) REFERENCES Tournments(Id)," +
                    "foreign key(team1_id) REFERENCES Teams(Id)," +
                    "foreign key(team2_id) REFERENCES Teams(Id)" +
                    "on delete cascade);";
                command.ExecuteNonQuery();
                Console.WriteLine("Matchess Table created");
            }
        }

        public void createScoreBoardTable()
        {
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "create table ScoreBoard(Id int primary key,Name varchar(255) not null,ScoreTeam1 int not null,ScoreTeam2 int not null,Win varchar(255),tournment_id int not null,match_id int not null," +
                    "foreign key(tournment_id) REFERENCES Tournments(Id)," +
                    "foreign key(match_id) REFERENCES Matches(Id)" +
                    "on delete cascade);";
                command.ExecuteNonQuery();
                Console.WriteLine("ScoreBoard Table created");
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



        public void AddSports()
        {
            Console.WriteLine("Enter the id of sports");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter the sport name:");
            string sportName = Console.ReadLine();

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = $"insert into Sports values({id},'{sportName}')";
                command.ExecuteNonQuery();
            }

            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = $"select * from Sports";
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    for (int j = 0; j < reader.FieldCount; j++)
                    {
                        Console.Write(reader[j] + " ");
                    }
                    Console.WriteLine();
                }
                reader.Close();
                command.ExecuteReader().Close();
            }
        }

       

        static void Main(string[] args)
        {
            CollegeSportsManagementSystem c = new();
            c.connection = new SqlConnection(c.connectionString);
            c.connection.Open();
            //c.createSportsTable();
            //c.CreateTournmentsTable();
            //c.CreateTeamsTable();
            //c.CreatePlayersTable();
            //c.CreateMatchesTable();
            //c.createScoreBoardTable();
            int choice;
            do
            {
                Console.WriteLine("Welcome to the College Sports Management System!");
                Console.WriteLine("1. Add Sport");
                Console.WriteLine("2. Add Tournament");
                Console.WriteLine("3. Add Team");
                Console.WriteLine("4. Add Player");
                Console.WriteLine("5. Add Match");
                Console.WriteLine("6. Add Scoreboard");
                Console.WriteLine("7. Remove Sport");
                Console.WriteLine("8. Remove Tournament");
                Console.WriteLine("9. Remove Team");
                Console.WriteLine("10. Remove Player");
                Console.WriteLine("11. Remove Match");
                Console.WriteLine("12. Edit Scoreboard");
                Console.WriteLine("15. To Quit");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        c.AddSports();
                        Console.WriteLine("Sport added successfully!");
                        break;

                    
                }

            } while (choice != 15);





















                    c.connection.Close();

        }
    }
}