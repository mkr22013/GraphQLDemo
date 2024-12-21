using GraphQLDemo.API;

var builder = WebApplication.CreateBuilder(args);

//Register Services 
builder.Services.RegisterServices();

var app = builder.Build();

app.UseRouting();
app.UseWebSockets(); //As it is graphQL API, Use WebSockets for subscriptions

app.MapGraphQL(); //As it is graphQL API, Map it to graphQL

app.Run();
