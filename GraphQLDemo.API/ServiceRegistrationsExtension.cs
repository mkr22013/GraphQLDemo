using GraphQLDemo.API.DataLoaders;
using GraphQLDemo.API.Schema.Mutations;
using GraphQLDemo.API.Schema.Queries;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services;
using GraphQLDemo.API.Services.Courses;
using GraphQLDemo.API.Services.Instructors;
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
            services.AddScoped<InstructorsRepository>();
            services.AddScoped<InstructorDataLoader>();

            return services;
        }
    }
}
