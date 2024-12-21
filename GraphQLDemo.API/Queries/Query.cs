using Bogus;
using GraphQLDemo.API.Schema;

namespace GraphQLDemo.API.Queries
{
    /// <summary>
    /// Query class to hold all the queries 
    /// </summary>
    public class Query
    {
        private readonly Faker<InstructorType> _instructorType;
        private readonly Faker<StudentType> _studentType;
        private readonly Faker<CourseType> _courseType;

        /// <summary>
        /// Constructor
        /// </summary>
        public Query()
        {
            _instructorType = new Faker<InstructorType>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.Salary, f => double.Round(f.Random.Double(0, 100000), 2));

            _studentType = new Faker<StudentType>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.FirstName, f => f.Name.FirstName())
                .RuleFor(c => c.LastName, f => f.Name.LastName())
                .RuleFor(c => c.GPA, f => double.Round(f.Random.Double(1, 4), 1));

            _courseType = new Faker<CourseType>()
                .RuleFor(c => c.Id, f => Guid.NewGuid())
                .RuleFor(c => c.Name, f => f.Name.FirstName())
                .RuleFor(c => c.Subject, f => f.PickRandom<Subject>())
                .RuleFor(c => c.Instructor, f => _instructorType.Generate())
                .RuleFor(c => c.Students, f => _studentType.Generate(3));
        }

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
        /// Get all courses 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseType>> GetCourses()
        {
            return await Task.Run(() => _courseType.Generate(5));
        }

        /// <summary>
        /// Get course by Id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<CourseType> GetCoursesById(Guid guid)
        {
            return await Task.Run(() =>
            {
                var course = _courseType.Generate();
                course.Id = guid;
                return course;
            });
        }
    }
}
