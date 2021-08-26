using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace France___Fix_Events_0._1
{
    class SQLManager
    {
        public void UpdateGeometries(string[] eventsType, RoadPathInfo[] roadPaths, string connectionString)
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    string sql = "UPDATE geometries SET event = @eventType " +
                    "WHERE objectid = @roadSectionName " +
                    "AND chaussee = @roadNumber " +
                    "AND voie = @roadSide " +
                    "AND sens = @roadLaneNumber " +
                    "AND concac_deb = @roadMeter";

                    for (int i = 0; i < eventsType.Length; i++)
                    {
                        NpgsqlCommand command = new NpgsqlCommand(sql, connection);
                        command.Parameters.AddWithValue("eventType", eventsType[i]);
                        command.Parameters.AddWithValue("roadSectionName", roadPaths[i].RoadSectionName);
                        command.Parameters.AddWithValue("roadNumber", roadPaths[i].RoadNumber);
                        command.Parameters.AddWithValue("roadSide", roadPaths[i].RoadSide);
                        command.Parameters.AddWithValue("roadLaneNumber", roadPaths[i].RoadLaneNumber);
                        command.Parameters.AddWithValue("roadMeter", roadPaths[i].RoadMeter);

                        command.ExecuteNonQuery();
                        command.Parameters.Clear();
                    }
                    transaction.Commit();
                }
            }
        }


        
        private void ExecuteSQL(string commandString, string connectionString) {
            using (var connection = new NpgsqlConnection(connectionString)) {
                connection.Open();
                using (var command = new NpgsqlCommand(commandString, connection)) {
                    int a = command.ExecuteNonQuery();
                    Console.WriteLine(a);
                }
            }
        }
        private void UpdateGeometry(string eventType, RoadPathInfo roadPath, string connectionString) {
            using (var connection = new NpgsqlConnection(connectionString)) {
                string cmd = "UPDATE geometries SET event = @eventType " +
                    "WHERE objectid = @roadSectionName " +
                    "AND chaussee = @roadNumber " +
                    "AND voie = @roadSide " +
                    "AND sens = @roadLaneNumber " +
                    "AND concac_deb = @roadMeter";
                //Console.WriteLine(cmd);

                using (var command = new NpgsqlCommand(cmd, connection)) {
                    connection.Open();
                    command.Parameters.AddWithValue("eventType", eventType);
                    command.Parameters.AddWithValue("roadSectionName", roadPath.RoadSectionName);
                    command.Parameters.AddWithValue("roadNumber", roadPath.RoadNumber);
                    command.Parameters.AddWithValue("roadSide", roadPath.RoadSide);
                    command.Parameters.AddWithValue("roadLaneNumber", roadPath.RoadLaneNumber);
                    command.Parameters.AddWithValue("roadMeter", roadPath.RoadMeter);
                    command.ExecuteNonQuery();
                }
            }
        }
        private void InsertGeometry(RoadPathInfo roadPath, string connectionString, int gid) {
            using (var connection = new NpgsqlConnection(connectionString)) {
                string cmd = "INSERT INTO geometries (geometriesid, objectid, chaussee, voie, sens, concac_deb) " +
                    "VALUES (@gid, @roadSectionName, @roadNumber, @roadSide, @roadLaneNumber, @roadMeter)";
                //Console.WriteLine(cmd);

                using (var command = new NpgsqlCommand(cmd, connection)) {
                    connection.Open();
                    command.Parameters.AddWithValue("gid", gid);
                    command.Parameters.AddWithValue("roadSectionName", roadPath.RoadSectionName);
                    command.Parameters.AddWithValue("roadNumber", roadPath.RoadNumber);
                    command.Parameters.AddWithValue("roadSide", roadPath.RoadSide);
                    command.Parameters.AddWithValue("roadLaneNumber", roadPath.RoadLaneNumber);
                    command.Parameters.AddWithValue("roadMeter", roadPath.RoadMeter);
                    command.ExecuteNonQuery();
                }
            }
        }
        private void DeleteGeometry(RoadPathInfo roadPath, string connectionString) {
            using (var connection = new NpgsqlConnection(connectionString)) {
                string cmd = "DELETE FROM geometries " +
                    "WHERE objectid = @roadSectionName " +
                    "AND chaussee = @roadNumber " +
                    "AND voie = @roadSide " +
                    "AND sens = @roadLaneNumber " +
                    "AND concac_deb = @roadMeter";
                //Console.WriteLine(cmd);

                using (var command = new NpgsqlCommand(cmd, connection)) {
                    connection.Open();
                    command.Parameters.AddWithValue("roadSectionName", roadPath.RoadSectionName);
                    command.Parameters.AddWithValue("roadNumber", roadPath.RoadNumber);
                    command.Parameters.AddWithValue("roadSide", roadPath.RoadSide);
                    command.Parameters.AddWithValue("roadLaneNumber", roadPath.RoadLaneNumber);
                    command.Parameters.AddWithValue("roadMeter", roadPath.RoadMeter);
                    command.ExecuteNonQuery();
                }
            }
        }


        public string[] GetDatabaseNames(string connectionString) {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                List<string> list = new List<string>();
                var command = new NpgsqlCommand("SELECT datname FROM pg_database", connection);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                         list.Add(reader.GetString(0));
                    }
                }
                return list.ToArray();
            }
        }

        public string GetConnectionString(string host, int port, string login, string password, string dbName)
        {
            return string.Format("Server={0};Port={1};User Id={2};Password={3};Database={4};Timeout={5};",
                host,
                port,
                login,
                password,
                dbName,
                "5");
        }

    }
}
