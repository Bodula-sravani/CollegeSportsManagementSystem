using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;

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



        public bool AddSports()
        {
            Console.WriteLine("Enter the id of sports");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter the sport name:");
            string sportName = Console.ReadLine();

            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"insert into Sports values({id},'{sportName}')";
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }


        }

        public void ViewSports()
        {
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

        public bool AddTournment()
        {
            Console.WriteLine("Here are the available sports");
            ViewSports();
            Console.WriteLine();
            Console.WriteLine("Enter the id of tournment");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter the tournament name:");
            string tournamentName = Console.ReadLine();
            Console.WriteLine("Enter the sport ID for this tournament:");
            int sportID = Convert.ToInt32(Console.ReadLine());
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"insert into Tournments values({id},'{tournamentName}',{sportID})";
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch(SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
        }
        public bool RemoveTournment()
        {
            Console.WriteLine("Enter the tournment id to be removed");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            try
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"select count(*) from Tournments where Id={id}";
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        command.CommandText = $"delete from Tournments where Id={id}";
                        command.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Tourment doesnt exist");
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);

            }

            return false;
        }
        public bool RemoveSport()
        {
            Console.WriteLine("Enter the sports id to be removed");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            try
            {
                
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"select count(*) from Sports where Id={id}";
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        command.CommandText = $"delete from Sports where Id={id}";
                        command.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Sports doesnt exist");
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
               
            }

            return false;
        }

        public bool AddTeam()
        {

            Console.WriteLine();
            Console.WriteLine("Enter the id of team");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter the player name:");
            string teamName = Console.ReadLine();
            Console.WriteLine("Enter the team ID for this player:");
            int tournmentID = Convert.ToInt32(Console.ReadLine());
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"insert into Teams values({id},'{teamName}',{tournmentID})";
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
        }
        public bool RemoveTeam()
        {
            Console.WriteLine("Enter the team id to be removed");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            try
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"select count(*) from Teams where Id={id}";
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        command.CommandText = $"delete from Teams where Id={id}";
                        command.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Team doesnt exist");
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
            return false;
        }

        public bool AddPlayer()
        {

            Console.WriteLine();
            Console.WriteLine("Enter the id of player");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter the player name:");
            string playerName = Console.ReadLine();
            Console.WriteLine("Enter the team ID for this player:");
            int teamID = Convert.ToInt32(Console.ReadLine());
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"insert into Players values({id},'{playerName}',{teamID})";
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);
                return false;
            }
        }
        public bool RemovePlayer()
        {
            Console.WriteLine("Enter the player id to be removed");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            try
            {

                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"select count(*) from Players where Id={id}";
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        command.CommandText = $"delete from Players where Id={id}";
                        command.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("Player doesnt exist");
                    }

                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
            return false;
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
                Console.WriteLine("3. Remove Sport");
                Console.WriteLine("4. Add Team");
                Console.WriteLine("5. Remove Team");
                Console.WriteLine("6. Add Player");
                Console.WriteLine("7. Remove Player");
                Console.WriteLine("8. Remove Tournment");
                Console.WriteLine("15. To Quit");

                choice = Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        if(c.AddSports())
                        Console.WriteLine("Sport added successfully!");
                        break;

                    case 2:
                        if(c.AddTournment())
                        Console.WriteLine("Tournament added successfully!");
                        break;

                    case 3:
                        if (c.RemoveSport())
                        { Console.WriteLine("Sport removed successfully!"); }
                        break;
                    case 4:
                        if (c.AddTeam())
                            Console.WriteLine("Team added successfully!");
                        break;
                    case 5:
                        if (c.RemoveTeam())
                            Console.WriteLine("Team removed successfully!");
                        break;
                    case 6:
                        if (c.AddPlayer())
                            Console.WriteLine("Player added successfully!");
                        break;
                    case 7:
                        if(c.RemovePlayer())
                        { Console.WriteLine("Player removed successfully!"); }
                        break;
                    case 8:
                        if (c.RemoveTournment())
                            Console.WriteLine("Tourmnent removed successfully");
                        break;
                    case 9:
                        c.ViewSports();
                        break;

                }

            } while (choice != 15);





















                    c.connection.Close();

        }
    }
}