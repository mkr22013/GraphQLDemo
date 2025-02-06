using GraphQLDemo.API.DataLoaders;
using GraphQLDemo.API.Models;

namespace GraphQLDemo.API.Schema.Queries
{
    /// <summary>
    /// Domain entity
    /// </summary>
    public class CourseType : ISearchResultType
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Subject Subject { get; set; }
        [IsProjected(true)] //This is most imp to make sure all 1 to many relations work in case in select we do not ask for InstructorId and then we ask for instructors to return then it will fail
        //IsProjected attribute makes sure that even in select query we do not ask for it, it will still selected as
        //it is required for all instructors to load based on that id
        public Guid InstructorId { get; set; }
        #region "Normal Call for Instructor property"

        /*[GraphQLNonNullType]*/ // If you want to make the Instructor to be not nullable and want to throw an error if it is null
        //public required InstructorType Instructor { get; set; }

        #endregion

        #region "N + 1 query Problem"

        //[GraphQLNonNullType]
        //public async Task<InstructorType?> Instructor([Service] InstructorsRepository instructorsRepository)
        //{
        //    var instructorDTO = await instructorsRepository.GetById(InstructorId);

        //    return instructorDTO == null
        //        ? throw new NullReferenceException("No instructors found")
        //        : new InstructorType()
        //        {
        //            Id = instructorDTO.Id,
        //            FirstName = instructorDTO.FirstName,
        //            LastName = instructorDTO.LastName,
        //            Salary = instructorDTO.Salary
        //        };
        //}

        #endregion

        #region "DataLoader"

        [GraphQLNonNullType]
        public async Task<InstructorType?> Instructor([Service] InstructorDataLoader instructorsDataLoader)
        {
            var instructorDTO = await instructorsDataLoader.LoadAsync(InstructorId, CancellationToken.None);

            return instructorDTO == null
                ? throw new NullReferenceException("No instructors found")
                : new InstructorType()
                {
                    Id = instructorDTO.Id,
                    FirstName = instructorDTO.FirstName,
                    LastName = instructorDTO.LastName,
                    Salary = instructorDTO.Salary
                };
        }

        #endregion

        public IEnumerable<StudentType>? Students { get; set; }
    }
}
