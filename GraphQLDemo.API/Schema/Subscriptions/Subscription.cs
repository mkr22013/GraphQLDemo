using GraphQLDemo.API.Schema.Mutations;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema.Subscriptions
{

    /// <summary>
    /// Subscription class for GraphQL
    /// </summary>
    public class Subscription
    {
        /// <summary>
        /// Subscription when a course is added
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [Subscribe]
        [Topic("CourseAdded")]
        public CourseResults OnCourseAdded([EventMessage] CourseResults course)
        {
            return course;
        }

        #region "SubscribeAndResolve"

        ///// <summary>
        ///// Subscription for when a course is updated only for certain course id
        ///// </summary>
        ///// <param name="courseId"></param>
        ///// <param name="topicEventReceiver"></param>
        ///// <returns></returns>
        //[SubscribeAndResolve]
        //public ValueTask<ISourceStream<CourseResults>> OnCourseUpdated(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
        //{
        //    string topicName = $"{courseId}_{nameof(Subscription.OnCourseUpdated)}";
        //    return topicEventReceiver.SubscribeAsync<CourseResults>(topicName);
        //}

        #endregion

        /// <summary>
        /// Declare the custom subscription details
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="topicEventReceiver"></param>
        /// <returns></returns>
        public ValueTask<ISourceStream<CourseResults>> SubscribeToCourse(Guid courseId, [Service] ITopicEventReceiver topicEventReceiver)
        {
            string topicName = $"{courseId}_{nameof(SubscribeToCourse)}";
            return topicEventReceiver.SubscribeAsync<CourseResults>(topicName);
        }

        /// <summary>
        /// Subscription when a course is updated
        /// </summary>
        /// <param name="course"></param>
        /// <returns></returns>
        [Subscribe(With = nameof(SubscribeToCourse))]
        public CourseResults OnCourseUpdated([EventMessage] CourseResults course) { return course; }
    }
}
