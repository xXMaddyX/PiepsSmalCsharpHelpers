using System.Text.Json;

PersonClass newPerson = new();
newPerson.SetData("Peter", 33, "peter@hotmail.de");
var JasonFromPerson = JsonSerializer.Serialize<PersonClass>(newPerson);

async Task WriteFileToDisk(string jsonString) {
    using var writeFile = File.Open("myJson.json", FileMode.Create, FileAccess.Write);
    var buffer = System.Text.Encoding.UTF8.GetBytes(jsonString);
    await writeFile.WriteAsync(buffer);
}
await WriteFileToDisk(JasonFromPerson);

void ReadDataAsJson() {
    using var openFile = File.Open("myJson.json", FileMode.Open, FileAccess.Read);
    List<byte> wholeBuffer = [];
    var buffer = new byte[2048];
    int bufferRead;

    while ((bufferRead = openFile.Read(buffer, 0, buffer.Length)) > 0) {
        wholeBuffer.AddRange(buffer.AsSpan(0, bufferRead).ToArray());
    };

    string result = System.Text.Encoding.UTF8.GetString([.. wholeBuffer]);
    Console.WriteLine(result);
}

ReadDataAsJson();
class PersonClass{
    public string? Name { get; set; } = null;
    public int? Age { get; set; } = null;
    public string? Mail { get; set; } = null;
    
    public void SetData(string name, int age, string mail) {
        Name = name;
        Age = age;
        Mail = mail;
    }

    public void PrintData() {
        Console.WriteLine($"Name: {Name}\nAge: {Age}\nMail: {Mail}");
    }
}