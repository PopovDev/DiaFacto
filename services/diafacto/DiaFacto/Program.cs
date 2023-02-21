using DiaFacto;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("Redis");
if (string.IsNullOrEmpty(connectionString))
{
    throw new Exception("Redis connection string is missing");
}

var multiplexer = ConnectionMultiplexer.Connect(connectionString);
builder.Services.AddSingleton<IConnectionMultiplexer>(multiplexer);
builder.Services.AddScoped<DbAccess>();
var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();

app.Run();