var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:VacationDbConnectionString"];

builder.Services.ConfigureService<VacationsContext>(connectionString);

var app = builder.Build();

await app.Services.RunMigrations<VacationsContext>();
app.UseHttpsRedirection();
app.ConfigureApi();
app.Run();


public partial class Program { }