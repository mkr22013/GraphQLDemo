using AppAny.HotChocolate.FluentValidation;
using GraphQLDemo.API.DataLoaders;
using GraphQLDemo.API.Schema.Mutations;
using GraphQLDemo.API.Schema.Queries;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services;
using GraphQLDemo.API.Services.Courses;
using GraphQLDemo.API.Services.Instructors;
using GraphQLDemo.API.Validators;
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
                .AddQueryType<Query>() //This is for query
                .AddMutationType<Mutation>() //This is for DML
                .AddSubscriptionType<Subscription>() //This is for eventing
                .AddFiltering()//This is for query filtering. It apply where condition while querying
                .AddType<CourseType>()
                .AddType<InstructorType>()
                .AddTypeExtension<CourseQuery>()
                .AddFluentValidation(o =>
                {
                    o.UseDefaultErrorMapper();
                })
                .AddSorting()
                .AddProjections()
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
            services.AddTransient<CourseTypeInputValidator>();
            return services;
        }
    }
}
