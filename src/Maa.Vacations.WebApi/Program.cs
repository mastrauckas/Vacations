var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration["ConnectionStrings:VacationDbConnectionString"];

builder.Services.ConfigureService<VacationsContext>(connectionString);

var app = builder.Build();

app.UseHttpsRedirection();
app.ConfigureApi();
app.Run();

public partial class Program
{
}