using GraphQLDemo.API.Schema.Queries;
using HotChocolate.Data.Filters;

namespace GraphQLDemo.API.Schema.Filters
{
    /// <summary>
    /// Custom filter type
    /// </summary>
    public class CourseFilterType : FilterInputType<CourseType>
    {
        /// <summary>
        /// Override the method
        /// </summary>
        /// <param name="descriptor"></param>
        protected override void Configure(IFilterInputTypeDescriptor<CourseType> descriptor)
        {
            descriptor.Ignore(x => x.Students);
            base.Configure(descriptor);
        }
    }
}
