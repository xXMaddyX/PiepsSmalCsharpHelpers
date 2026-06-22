namespace SQLConnector;

using Microsoft.Data.Sqlite;

public class SQLITEConnector{
    public string _connectionString = $"Data Source={Path.Combine(AppContext.BaseDirectory, "app.db")}";

    public void InitConnection(string connectionString) {
        using var Connection = new SqliteConnection($"Data Source={Path.Combine(AppContext.BaseDirectory, connectionString)}");
        Connection.Open();
        var sql = Connection.CreateCommand();
        sql.CommandText =  @"
        CREATE TABLE IF NOT EXISTS users(
        id INTEGER PRIMARY KEY,
        user_name TEXT NOT NULL,
        user_pass TEXT NOT NULL
        )
        ";
        sql.ExecuteNonQuery();
        Connection.Close();
    }

    public void CreateUser(string username, string userpassword) {
        using var Connection = new SqliteConnection(_connectionString);
        Connection.Open();
        var sql = Connection.CreateCommand();
        sql.CommandText = @"
        INSERT INTO users(user_name, user_pass)
        VALUES (@user_name, @user_pass)
        ";
        sql.Parameters.AddWithValue("@user_name", username);
        sql.Parameters.AddWithValue("@user_pass", userpassword);
        sql.ExecuteNonQuery();
        Connection.Close();
    }

    public void DeleteUser(string username) {
        using var Connection = new SqliteConnection(_connectionString);
        Connection.Open();
        var sql = Connection.CreateCommand();
        sql.CommandText = @"
        DELETE FROM users WHERE user_name = @user_name
        ";
        sql.Parameters.AddWithValue("@user_name", username);
        sql.ExecuteNonQuery();
        Connection.Close();
    }

    public void GetUserData(string userName) {
        var Connection = new SqliteConnection(_connectionString);
        Connection.Open();
        var sql = Connection.CreateCommand();
        sql.CommandText = @"
        SELECT * FROM users WHERE user_name = @user_name
        ";
        sql.Parameters.AddWithValue("@user_name", userName);
        using var reader = sql.ExecuteReader();
        if (reader.HasRows) {
            while (reader.Read()) {
                var id = reader.GetInt32(0);
                var username = reader.GetString(1);
                var userpass = reader.GetString(2);
                Console.WriteLine($"id: {id}\nUsername: {username}\nUserPass: {userpass}");
            }
        }
        Connection.Close();
    }
}