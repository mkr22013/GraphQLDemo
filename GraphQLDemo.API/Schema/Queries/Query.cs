using GraphQLDemo.API.Services.Courses;

namespace GraphQLDemo.API.Schema.Queries
{
    /// <summary>
    /// Query class to hold all the queries 
    /// </summary>
    public class Query(CoursesRepository coursesRepository)
    {
        private readonly CoursesRepository _coursesRepository = coursesRepository;

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
            var courseDTOs = await _coursesRepository.GetAll();
            return courseDTOs.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId,
                //Instructor = new InstructorType()
                //{
                //    Id = c.Instructor.Id,
                //    FirstName = c.Instructor.FirstName,
                //    LastName = c.Instructor.LastName,
                //    Salary = c.Instructor.Salary
                //},
            });
        }

        /// <summary>
        /// Get course by Id
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public async Task<CourseType?> GetCoursesById(Guid guid)
        {
            var courseDTOs = await _coursesRepository.GetById(guid);
            if (courseDTOs == null)
            {
                return null;
            }
            return new CourseType()
            {
                Id = courseDTOs.Id,
                Name = courseDTOs.Name,
                Subject = courseDTOs.Subject,
                InstructorId = courseDTOs.InstructorId,
                //Instructor = new InstructorType()
                //{
                //    Id = courseDTOs.Instructor.Id,
                //    FirstName = courseDTOs.Instructor.FirstName,
                //    LastName = courseDTOs.Instructor.LastName,
                //    Salary = courseDTOs.Instructor.Salary
                //},
            };
        }
    }
}
