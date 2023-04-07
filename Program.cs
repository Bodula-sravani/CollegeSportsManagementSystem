using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace CricketTorunment
{
    internal class CollegeSportsManagementSystem
    {
        public string connectionString = "Data Source=DESKTOP-8C83AOT;Initial Catalog=CollegeSportsTorunments;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";
        SqlConnection connection = new SqlConnection();

        // CREATING ALL THE REQUIRED TABLES
        public void createSportsTable()
        {
            // Sports Table 
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "create table Sports(Id int primary key,Name varchar(255) not null);";
                command.ExecuteNonQuery();
                Console.WriteLine("Sports Table created");
            }
        }

        public void CreateTournmentsTable()
        {
            // Tournments Table
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
            // Teams Table
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
            // Matches Table
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
            // ScoreBoard Table
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = "create table ScoreBoard(Id int primary key,Name varchar(255) not null,tournment_id int not null,match_id int not null,ScoreTeam1 int ,ScoreTeam2 int ,Win varchar(255)," +
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

        public void ViewData(string tableName)
        {
            // Displays all rows in any given table
            using (SqlCommand command = connection.CreateCommand())
            {
                command.CommandText = $"select * from {tableName}";
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

        public bool AddSports()
        {
            // Inserts data into Sports Table

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

        public bool AddTournment()
        {
            // Inserts data into Tournments Table
            Console.WriteLine("Here are the available sports");
            ViewData("Sports");
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
        public bool AddTeam()
        {
            // Inserts data into Teams Table
            Console.WriteLine();
            Console.WriteLine("Enter the id of team");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter the team name:");
            string teamName = Console.ReadLine();
            Console.WriteLine("Enter the tournment id in which team participates");
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

        public bool AddPlayer()
        {
            // Inserts data into Players Table
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

        public bool AddMatch()
        {
            // Inserts data into Matches Table
            Console.WriteLine();
            Console.WriteLine("Enter the id of match");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter the match name:");
            string matchName = Console.ReadLine();
            Console.WriteLine("Enter the tournment id ");
            int tourid = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter the team 1 ID for this player:");
            int team1ID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the team 2 ID for this player:");
            int team2ID = Convert.ToInt32(Console.ReadLine());
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"insert into Matches values({id},'{matchName}',{tourid},{team1ID},{team2ID})";
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

        public bool DeleteMethod(string selectCommad, string deleteCommand)
        {
            // Takes select query,delete query and deletes respective table row if exists
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = selectCommad;
                    int count = (int)command.ExecuteScalar();
                    if (count > 0)
                    {
                        command.CommandText = deleteCommand;
                        command.ExecuteNonQuery();
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(".........Doesnt exist........");
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error: " + e.Message);

            }
            return false;
        }


        public bool RemoveTournment()
        {
            // uses deleteCommand method by passing the respective queires
            Console.WriteLine("Enter the tournment id to be removed");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            string commandText1 = $"select count(*) from Tournments where Id={id}";
            string deleteCommand = $"delete from Tournments where Id={id}";
            return DeleteMethod(commandText1, deleteCommand);
        }

        public bool RemoveSport()
        {
            Console.WriteLine("Enter the sports id to be removed");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            string commandText1 = $"select count(*) from Sports where Id={id}";
            string deleteCommand = $"delete from Sports where Id={id}";
            return DeleteMethod(commandText1, deleteCommand);
        }

       
        public bool RemoveTeam()
        {
            Console.WriteLine("Enter the team id to be removed");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            string commandText1 = $"select count(*) from Teams where Id={id}";
            string deleteCommand = $"delete from Teams where Id={id}";
            return DeleteMethod(commandText1, deleteCommand);
        }

        
        public bool RemovePlayer()
        {
            Console.WriteLine("Enter the player id to be removed");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            string commandText1 = $"select count(*) from Players where Id={id}";
            string deleteCommand = $"delete from Players where Id={id}";
            return DeleteMethod(commandText1, deleteCommand);
        }

        public bool AddScoreBoard()
        {
            // Adds scoreboad name and tournment id match id into table
            Console.WriteLine();
            Console.WriteLine("Enter the ScoreBoard Id");
            int id = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter the SocreBoard name:");
            string matchName = Console.ReadLine();
            Console.WriteLine("Enter the tournment id ");
            int tourid = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter the match id");
            int matchID = Convert.ToInt32(Console.ReadLine());
            try
            {
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"insert into ScoreBoard(Id,Name,tournment_id,match_id) values({id},'{matchName}',{tourid},{matchID})";
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

        public bool EditScoreBoard()
        {
            // Updates the respective match's score board 
            Console.WriteLine("Enter the match id to enter score");
            int matchId = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter team 1 score");
            int team1 = Convert.ToInt32(Console.ReadLine().Trim());
            Console.WriteLine("Enter team 2 score");
            int team2 = Convert.ToInt32(Console.ReadLine().Trim());
            string won_team = team1 > team2 ? "team1_id" : "team2_id";
            using (SqlCommand command = connection.CreateCommand())
            {
                try
                {
                    command.CommandText = $"select T.Name from Matches M inner join Teams T on T.Id = M.{won_team} and M.Id = {matchId}";
                    SqlDataReader reader = command.ExecuteReader();
                    string won = "";
                    while (reader.Read())
                    {
                        Console.WriteLine("fied: " + reader.FieldCount);
                        for (int j = 0; j < reader.FieldCount; j++)
                        {
                           won = reader[j].ToString().Trim();
                        }

                    }
                    //string won = reader.GetString(0);
                    reader.Close();
                    try
                    {
                        command.CommandText = $"update ScoreBoard set ScoreTeam1= {team1},ScoreTeam2 = {team2},Win='{won}' where match_id={matchId}";
                        command.ExecuteNonQuery();
                        return true;
                    }
                    catch(SqlException e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                    }
                }
                catch(SqlException e)
                {
                    Console.WriteLine("Error: " + e.Message);
                }
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
                // switch case to execute the required task on a loop
                Console.WriteLine("Welcome to the College Sports Management System!");
                Console.WriteLine("1. Add a Sport");
                Console.WriteLine("2. Add a Tournament");
                Console.WriteLine("3. Remove a Sport");
                Console.WriteLine("4. Add a Team");
                Console.WriteLine("5. Remove a Team");
                Console.WriteLine("6. Add a Player");
                Console.WriteLine("7. Remove a Player");
                Console.WriteLine("8. Remove a Tournment");
                Console.WriteLine("9. Add a match");
                Console.WriteLine("10. Add a ScoreBoard");
                Console.WriteLine("11 Edit a ScoreBoard");
                Console.WriteLine("14. view rows of any table");
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
                        if(c.AddMatch())
                        {
                            Console.WriteLine("Match added successfully");
                        }
                        break;
                    case 10:
                        if (c.AddScoreBoard())
                            Console.WriteLine("ScoreBoard added successfully");
                        break;
                    case 11:
                        if (c.EditScoreBoard())
                            Console.WriteLine("ScoreBoard edited successfully");
                        break;
                    case 14:
                        Console.WriteLine("Enter table name to be viewed");
                        string tableName = Console.ReadLine().Trim();
                        c.ViewData(tableName);
                        break;

                }
                Console.WriteLine();
            } while (choice != 15);

         c.connection.Close();
        }
    }
}