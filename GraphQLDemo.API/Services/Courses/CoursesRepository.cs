using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services.Courses
{
    /// <summary>
    /// Courses repository
    /// </summary>
    public class CoursesRepository(IDbContextFactory<SchoolDbContext> dbContextFactory)
    {
        private readonly IDbContextFactory<SchoolDbContext> _dbContextFactory = dbContextFactory;

        /// <summary>
        /// Get all courses
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<CourseDTO>> GetAll()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Cources
                //.Include(c => c.Instructor)
                //.Include(c => c.Students)
                .ToListAsync();
        }

        /// <summary>
        /// Get course by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CourseDTO?> GetById(Guid courseId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Cources
                //.Include(c => c.Instructor)
                //.Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.Id == courseId);
        }

        /// <summary>
        /// Create courses 
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<CourseDTO> Create(CourseDTO course)
        {
            ArgumentNullException.ThrowIfNull(course);

            using var context = _dbContextFactory.CreateDbContext();
            context.Cources.Add(course);
            await context.SaveChangesAsync();

            return course;
        }

        /// <summary>
        /// Update courses
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        public async Task<CourseDTO> Update(CourseDTO course)
        {
            ArgumentNullException.ThrowIfNull(course);

            using var context = _dbContextFactory.CreateDbContext();
            context.Cources.Update(course);
            await context.SaveChangesAsync();

            return course;
        }

        /// <summary>
        /// Delete cources
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid id)
        {
            using var context = _dbContextFactory.CreateDbContext();
            var course = new CourseDTO()
            {
                Id = id
            };
            context.Cources.Remove(course);
            return await context.SaveChangesAsync() != 0;

        }
    }
}
