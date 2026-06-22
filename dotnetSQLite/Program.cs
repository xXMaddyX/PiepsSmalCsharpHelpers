using ServerHandlers;
using SQLConnector;

var Options = new WebApplicationOptions{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
    WebRootPath = "src",
};

var builder = WebApplication.CreateBuilder(Options);
builder.Services.AddCors(options => {
    options.AddPolicy("SomePoly", builder  => {
        builder.AllowAnyOrigin();
    });
});

var App = builder.Build();
App.Urls.Add("http://localhost:3030");
App.UseCors();
App.UseStaticFiles();

var ServerHandler = new ServerRoutHandler(App);
ServerHandler.InitRoutes();
var sql_connector = new SQLITEConnector();
sql_connector.InitConnection("app.db");

App.Run();