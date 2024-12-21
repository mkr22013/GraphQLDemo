using GraphQLDemo.API.Courses;
using GraphQLDemo.API.Mutations;
using GraphQLDemo.API.Queries;
using GraphQLDemo.API.Services;
using GraphQLDemo.API.Subscriptions;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API
{
    /// <summary>
    /// Service Registration Class
    /// </summary>
    public static class ServiceRegistrationsExtension
    {
        /// <summary>
        /// Static function to register all services to keep program.cs clean
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, string? connectionString)
        {
            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();

            if (connectionString != null)
            {
                services.AddPooledDbContextFactory<SchoolDbContext>(o => o.UseSqlite(connectionString).LogTo(Console.WriteLine));
            }
            else
            {
                throw new Exception("ConnectionString is not provided");
            }

            //Register repository
            services.AddScoped<CoursesRepository>();
            return services;
        }
    }
}
