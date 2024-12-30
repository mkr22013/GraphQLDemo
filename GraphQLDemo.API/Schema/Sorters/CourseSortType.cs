using GraphQLDemo.API.Schema.Queries;
using HotChocolate.Data.Sorting;

namespace GraphQLDemo.API.Schema.Sorters
{
    /// <summary>
    /// Custom sorting
    /// </summary>
    public class CourseSortType : SortInputType<CourseType>
    {
        /// <summary>
        /// Override configuration
        /// </summary>
        /// <param name="descriptor"></param>
        protected override void Configure(ISortInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(c => c.Id);
            descriptor.Ignore(c => c.Students);
            base.Configure(descriptor);
        }
    }
}
