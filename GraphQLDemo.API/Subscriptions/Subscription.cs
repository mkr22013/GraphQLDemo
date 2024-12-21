using GraphQLDemo.API.Mutations;
using GraphQLDemo.API.Schema;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Subscriptions
{

    /// <summary>
    /// Subscription class for GraphQL
    /// </summary>
    public class Subscription
    {
        /// <summary>
        /// Subscription for when a course is added
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [Subscribe]
        [Topic("CourseAdded")]
        public CourseResults OnCourseAdded([EventMessage] CourseResults course)
        {
            return course;
        }

        /// <summary>
        /// Subscription for when a course is updated only for certain course id
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="topicEventReceiver"></param>
        /// <returns></returns>
        [SubscribeAndResolve]
        public ValueTask<ISourceStream<CourseResults>> OnCourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{courseId}_{nameof(Subscription.OnCourseUpdated)}";
            return topicEventReceiver.SubscribeAsync<CourseResults>(topicName);
        }
    }
}
