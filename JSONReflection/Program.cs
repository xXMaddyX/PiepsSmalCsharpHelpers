using System.Text.Json;

var newData = new Data{
    Name = "Das ist nur bla",
    Age = "schalala",
    Mail = "das ist die Mail",
    Some = "Das ist anders zeuch so ist das"
};

var OpenFile = new FileStream(path: "text.txt", access: FileAccess.Write, mode: FileMode.OpenOrCreate);
var buffer = System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(newData));
await OpenFile.WriteAsync(buffer);
OpenFile.Close();

var FileToRead = new FileStream(path: "text.txt", access: FileAccess.Read, mode: FileMode.Open);
var newBuffer = new byte[FileToRead.Length];
await FileToRead.ReadExactlyAsync(newBuffer);
FileToRead.Close();

var extractData = JsonSerializer.Deserialize<Data>(System.Text.Encoding.UTF8.GetString(newBuffer));
extractData?.PrintData();

class Data{
    public string? Name { get; set; } = null;
    public string? Age { get; set; } = null;
    public string? Mail { get; set; } = null;
    public string? Some { get; set; } = null;

    public void PrintData() {
        Console.WriteLine($"Name: {Name}, Age: {Age}, Mail: {Mail}, Some: {Some}");
    }
}
