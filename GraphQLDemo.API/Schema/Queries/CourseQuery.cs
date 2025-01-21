using GraphQLDemo.API.Schema.Filters;
using GraphQLDemo.API.Schema.Sorters;
using GraphQLDemo.API.Services.Courses;
using GraphQLDemo.API.Services;

namespace GraphQLDemo.API.Schema.Queries
{
    [ExtendObjectType(typeof(Query))]
    public class CourseQuery(CoursesRepository coursesRepository)
    {
        private readonly CoursesRepository _coursesRepository = coursesRepository;
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
        [UseDbContext(typeof(SchoolDbContext))]
        [UsePaging(IncludeTotalCount = true, DefaultPageSize = 10)]
        [UseFiltering(typeof(CourseFilterType))] //This is custom filter. CourseFilterType just ignores student object so we can not perform filter on student object 
        [UseSorting(typeof(CourseSortType))]
        public IQueryable<CourseType> GetPaginatedCourses([ScopedService] SchoolDbContext context)
        {
            return context.Cources.Select(c => new CourseType()
            {
                Id = c.Id,
                Name = c.Name,
                Subject = c.Subject,
                InstructorId = c.InstructorId
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
