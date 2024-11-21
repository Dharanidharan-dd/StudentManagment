using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace StudentSport
{
    public class DatabaseHelper
    {
        private readonly string _connectionString;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Sports>> GetSportsAsync()
        {
            var sports = new List<Sports>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT StudentID, Sport, House FROM dbo.Sport", connection);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        sports.Add(new Sports
                        {
                            StudentID = reader.GetString(0),
                            Sport = reader.GetString(1),
                            House = reader.GetString(2)
                        });
                    }
                }
            }
            return sports;
        }

        public async Task<Sports> GetSportAsync(int id)
        {
            Sports sport = null;
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("SELECT StudentID, Sport, House FROM dbo.Sport WHERE StudentID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        sport = new Sports
                        {
                            StudentID = reader.GetString(0),
                            Sport = reader.GetString(1),
                            House = reader.GetString(2)
                        };
                    }
                }
            }
            return sport;
        }

        public async Task<int> AddSportAsync(Sports sport)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("INSERT INTO dbo.Sport (StudentID, Sport, House) VALUES (@StudentID, @Sport, @House)", connection);
                command.Parameters.AddWithValue("@StudentID", sport.StudentID);
                command.Parameters.AddWithValue("@Sport", sport.Sport);
                command.Parameters.AddWithValue("@House", sport.House);
                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> UpdateSportAsync(Sports sport)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("UPDATE dbo.Sport SET Sport = @Sport, House = @House WHERE StudentID = @StudentID", connection);
                command.Parameters.AddWithValue("@StudentID", sport.StudentID);
                command.Parameters.AddWithValue("@Sport", sport.Sport);
                command.Parameters.AddWithValue("@House", sport.House);
                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<int> DeleteSportAsync(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = new SqlCommand("DELETE FROM dbo.Sport WHERE StudentID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                await connection.OpenAsync();
                return await command.ExecuteNonQueryAsync();
            }
        }
    }
}
