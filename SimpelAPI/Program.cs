var MyAllowSpecsCors = "_myAllowSpecsCors";
var options = new WebApplicationOptions{
    Args = args,
    WebRootPath = "src",
};
var builder = WebApplication.CreateBuilder(options);
builder.Services.AddCors(options => {
    options.AddPolicy(name: MyAllowSpecsCors,
    policy => {
        policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var App = builder.Build();
App.UseStaticFiles();
App.UseCors(MyAllowSpecsCors);
App.Urls.Add("http://localhost:4040");

App.MapGet("/", async (HttpContext ctx) => {
    var file = await File.ReadAllTextAsync("./src/index.html");
    await ctx.Response.WriteAsync(file);
});

App.Run();