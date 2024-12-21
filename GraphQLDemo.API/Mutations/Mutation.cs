using GraphQLDemo.API.MutationResolver;
using GraphQLDemo.API.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Mutations
{
    /// <summary>
    /// Mutation Resolver for Create, Update and Delete operations
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    public class Mutation([Service] ITopicEventSender topicEventSender)
    {
        private readonly List<CourseResults> _courses = [];
        private readonly ITopicEventSender topicEventSender = topicEventSender;

        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        public async Task<CourseResults> CreateCourse(CourseInputType courseInput)
        {
            var course = new CourseResults
            {
                Id = Guid.NewGuid(),
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId
            };
            _courses.Add(course);
            await topicEventSender.SendAsync("CourseAdded", course);
            return course;
        }

        /// <summary>
        /// Update an existing course
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        public async Task<CourseResults> UpdateCourse(Guid id, CourseInputType courseInput)
        {
            //First get the course from the list
            var course = _courses.FirstOrDefault(c => c.Id == id) ?? throw new GraphQLException(new Error("Course not found", "COURSE_NOT_FOUND"));
            course.Name = courseInput.Name;
            course.Subject = courseInput.Subject;
            course.InstructorId = courseInput.InstructorId;

            //Send event
            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.OnCourseUpdated)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return await Task.Run(() => course);
        }

        /// <summary>
        /// Delete a course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCourse(Guid id)
        {
            //First get the course from the list
            return await Task.Run(() => _courses.RemoveAll(c => c.Id == id) >= 1);
        }
    }
}
