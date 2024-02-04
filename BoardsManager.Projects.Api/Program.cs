using BoardsManager.Projects.Api.Exceptions;
using BoardsManager.Projects.IoC;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string connectionString = builder.Configuration.GetConnectionString("BoardsProjectsDb")
    ?? throw new MissingConfigurationException("Missing configuration BoardsProjectsDb");
builder.Services.RegisterDbContext(connectionString);
builder.Services.RegisterServices();
WebApplication app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();