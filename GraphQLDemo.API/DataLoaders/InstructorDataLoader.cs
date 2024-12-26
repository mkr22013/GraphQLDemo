using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Services.Instructors;

namespace GraphQLDemo.API.DataLoaders
{
    /// <summary>
    /// DataLoader class for Instrctor to process batch
    /// </summary>
    public class InstructorDataLoader : BatchDataLoader<Guid, InstructorDTO>
    {
        private readonly InstructorsRepository _instructorsRepository;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="instructorsRepository"></param>
        /// <param name="batchScheduler"></param>
        /// <param name="options"></param>
        public InstructorDataLoader(
          InstructorsRepository instructorsRepository,
          IBatchScheduler batchScheduler,
          DataLoaderOptions options = null)
          : base(batchScheduler, options)
        {
            _instructorsRepository = instructorsRepository;
        }

        /// <summary>
        /// Override abstract method
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        protected override async Task<IReadOnlyDictionary<Guid, InstructorDTO>> LoadBatchAsync(IReadOnlyList<Guid> keys, CancellationToken cancellationToken)
        {
            IEnumerable<InstructorDTO> instructors = await _instructorsRepository.GetManyByIds(keys);

            return instructors.ToDictionary(i => i.Id);
        }
    }
}
