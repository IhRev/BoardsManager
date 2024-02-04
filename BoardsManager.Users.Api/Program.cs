using BoardsManager.Users.Api.Exceptions;
using BoardsManager.Users.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterServices();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connectionString = builder.Configuration.GetConnectionString("BoardsUsersDb") 
    ?? throw new MissingConfigurationException("Missing configuration BoardsUsersDb");

builder.Services.RegisterDbContext(connectionString);
builder.Services.RegisterServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
