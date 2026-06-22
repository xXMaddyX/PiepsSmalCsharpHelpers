
namespace ServerHandlers;

public class ServerRoutHandler(WebApplication serverApp) {
    public WebApplication ServerApp { get; set; } = serverApp;

    public void InitRoutes() {
        ServerApp.MapGet("/", ServHome);
        ServerApp.MapPost("/Data", CreateNewUser);
    }

    private async Task ServHome(HttpContext ctx) {
        string? FileData;
        try {
            FileData = await File.ReadAllTextAsync("./src/index.html");
            ctx.Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Html;
            await ctx.Response.WriteAsync(FileData);
        } catch (Exception err) {
            ctx.Response.ContentType = System.Net.Mime.MediaTypeNames.Text.Plain;
            ctx.Response.StatusCode = 404;
            await ctx.Response.WriteAsync(err.Message);
        }
    }

    private async Task CreateNewUser(HttpContext ctx) {
        Console.WriteLine(ctx.Request.QueryString);
        foreach (var item in ctx.Request.Query) {
            Console.WriteLine(item);
        }
        using var reader = new StreamReader(ctx.Request.Body);
        var body = await reader.ReadToEndAsync();
        Console.WriteLine(body);
    }
}