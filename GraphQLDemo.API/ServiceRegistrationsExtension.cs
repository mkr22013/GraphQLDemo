using GraphQLDemo.API.Mutations;
using GraphQLDemo.API.Queries;
using GraphQLDemo.API.Subscriptions;

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
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>()
                .AddSubscriptionType<Subscription>()
                .AddInMemorySubscriptions();

            return services;
        }
    }
}
