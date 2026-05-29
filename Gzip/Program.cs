using System.IO.Compression;

var PathToSave = "test.txt";
var DataToSafe = "Das sind die Daten die Geschrieben wiederen";

var FileStreamToWrite = new FileStream(path: PathToSave, access: FileAccess.Write, mode: FileMode.OpenOrCreate);
var GzipStreamToWrite = new GZipStream(FileStreamToWrite, CompressionMode.Compress);
await GzipStreamToWrite.WriteAsync(System.Text.Encoding.UTF8.GetBytes(DataToSafe));
GzipStreamToWrite.Close();

var FileStreamToRead = new FileStream(path: PathToSave, access: FileAccess.Read, mode: FileMode.Open);
var GzipStreamToRead = new GZipStream(FileStreamToRead, CompressionMode.Decompress);
var MemStream = new MemoryStream();
await GzipStreamToRead.CopyToAsync(MemStream);
GzipStreamToRead.Close();

var result = System.Text.Encoding.UTF8.GetString(MemStream.ToArray());
Console.WriteLine(result);