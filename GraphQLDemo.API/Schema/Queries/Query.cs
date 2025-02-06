using GraphQLDemo.API.Services;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Schema.Queries
{
    /// <summary>
    /// Query class to hold all the queries 
    /// </summary>
    public class Query
    {
        /// <summary>
        /// Depcricated method declaration. GraphQL does not have versioning hence it is better to depricate query when not in use
        /// </summary>
        [GraphQLDeprecated("This query is depricated.")]
        public string Instructions
        {
            get
            {
                return "Smash that like button and subscribe to Maulin.....";
            }
        }
        
        /// <summary>
        /// Interface type search query 
        /// </summary>
        /// <param name="term"></param>
        /// <returns></returns>
        [UseDbContext(typeof(SchoolDbContext))]
        public async Task<IEnumerable<ISearchResultType>> Search(string term, [ScopedService] SchoolDbContext context)
        {
            var courses = await context.Cources
                .Where(c => c.Name.Contains(term))
                .Select(c => new CourseType()
                {
                    Id = c.Id,
                    Name = c.Name,
                    Subject = c.Subject,
                    InstructorId = c.InstructorId
                }).ToListAsync();

            var instructors = await context.Instructors
                .Where(i => i.FirstName.Contains(term) || i.LastName.Contains(term))
                .Select(i => new InstructorType()
                {
                    Id = i.Id,
                    FirstName = i.FirstName,
                    LastName = i.LastName,
                    Salary = i.Salary
                }).ToListAsync();

            return new List<ISearchResultType>().Concat(courses).Concat(instructors);
        }       
    }
}
