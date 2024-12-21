using GraphQLDemo.API;
using GraphQLDemo.API.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Get appsettings values
var connectionString = builder.Configuration.GetConnectionString("default");
//Register Services 
builder.Services.RegisterServices(connectionString);

var app = builder.Build();

//Below code is only required if we want to run migration everytime application starts
using (IServiceScope scope = app.Services.CreateScope())
{
    var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<SchoolDbContext>>();
    using var context = contextFactory.CreateDbContext();
    context.Database.Migrate();
}

app.UseRouting();
app.UseWebSockets(); //As it is graphQL API, Use WebSockets for subscriptions

app.MapGraphQL(); //As it is graphQL API, Map it to graphQL

app.Run();
