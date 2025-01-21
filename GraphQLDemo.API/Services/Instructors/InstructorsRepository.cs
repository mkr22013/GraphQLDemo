using GraphQLDemo.API.DTOs;
using Microsoft.EntityFrameworkCore;

namespace GraphQLDemo.API.Services.Instructors
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="dbContextFactory"></param>
    public class InstructorsRepository(IDbContextFactory<SchoolDbContext> dbContextFactory)
    {
        private readonly IDbContextFactory<SchoolDbContext> _dbContextFactory = dbContextFactory;

        /// <summary>
        /// Get all instructors
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<InstructorDTO>> GetAll()
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Instructors
                .Include(i => i.Courses)
                .ToListAsync();
        }

        /// <summary>
        /// Get instructors by id
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        public async Task<InstructorDTO?> GetById(Guid instructorId)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Instructors.FirstOrDefaultAsync(i => i.Id == instructorId);
        }

        /// <summary>
        /// It will return all the instructors based on provided keys
        /// </summary>
        /// <param name="keys"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [DataLoader]
        public async Task<IEnumerable<InstructorDTO>> GetManyByIds(IReadOnlyList<Guid> instructorIds)
        {
            using var context = _dbContextFactory.CreateDbContext();
            return await context.Instructors.Where(i => instructorIds.Contains(i.Id)).ToListAsync();
        }
    }
}
