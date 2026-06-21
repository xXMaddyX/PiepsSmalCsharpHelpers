namespace DbSystem;

using Microsoft.Data.Sqlite;

public class DBConnector{
    public SqliteConnection? MyConnection { get; set; } = null;

    public void InitConnection(string path) {
        MyConnection = new SqliteConnection($"Data Source={Path.Combine(AppContext.BaseDirectory, path)}");
        MyConnection.Open();
        var sql = MyConnection.CreateCommand();
        sql.CommandText = """
            CREATE TABLE IF NOT EXISTS users ( 
            id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, 
            username TEXT NOT NULL UNIQUE, 
            password_hash TEXT NOT NULL
            )
        """;
        sql.ExecuteNonQuery();
        MyConnection.Close();
        DeleteUser("MyTestUser");
    }

    public void CreateUser(string Username, string PlainPassword) {
        MyConnection?.Open();
        var sql = MyConnection?.CreateCommand();
        sql?.CommandText = $"""
        INSERT INTO users (username, password_hash)
        VALUES (@username, @password_hash)
        """;
        sql?.Parameters.AddWithValue("@username", Username);
        sql?.Parameters.AddWithValue("@password_hash", PlainPassword);
        sql?.ExecuteNonQuery();
        MyConnection?.Close();
    }

    public void DeleteUser(string Username) {
        MyConnection?.Open();
        var sql = MyConnection?.CreateCommand();
        sql?.CommandText = """
        DELETE FROM users WHERE username = @username
        """;
        sql?.Parameters.AddWithValue("@username", Username);
        sql?.ExecuteNonQuery();
        MyConnection?.Close();
    }
}

public class GenertateHash() {

}