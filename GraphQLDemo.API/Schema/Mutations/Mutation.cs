//using AppAny.HotChocolate.FluentValidation;
using AppAny.HotChocolate.FluentValidation;
using GraphQLDemo.API.DTOs;
using GraphQLDemo.API.Schema.Subscriptions;
using GraphQLDemo.API.Services.Courses;
using GraphQLDemo.API.Validators;

//using GraphQLDemo.API.Validators;
using HotChocolate.Subscriptions;

namespace GraphQLDemo.API.Schema.Mutations
{
    /// <summary>
    /// Mutation Resolver for Create, Update and Delete operations
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    public class Mutation([Service] ITopicEventSender topicEventSender, CoursesRepository coursesRepository)
    {
        private readonly CoursesRepository _coursesRepository = coursesRepository;
        private readonly ITopicEventSender topicEventSender = topicEventSender;

        /// <summary>
        /// Create a new course
        /// </summary>
        /// <param name="name"></param>
        /// <param name="subject"></param>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        public async Task<CourseResults> CreateCourse([UseFluentValidation,UseValidator<CourseTypeInputValidator>] CourseInputType courseInput)
        {
            //Map input with courseDTO
            var courseDTO = new CourseDTO()
            {
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,
            };

            //Call repository
            courseDTO = await _coursesRepository.Create(courseDTO);

            var course = new CourseResults
            { 
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };

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
            //Map input with courseDTO
            var courseDTO = new CourseDTO()
            {
                Id = id,
                Name = courseInput.Name,
                Subject = courseInput.Subject,
                InstructorId = courseInput.InstructorId,
            };

            //TODO: First check if course exist and if not throw exception 

            courseDTO = await _coursesRepository.Update(courseDTO);
            var course = new CourseResults
            {
                Id = courseDTO.Id,
                Name = courseDTO.Name,
                Subject = courseDTO.Subject,
                InstructorId = courseDTO.InstructorId
            };

            //Send event
            string updateCourseTopic = $"{course.Id}_{nameof(Subscription.SubscribeToCourse)}";
            await topicEventSender.SendAsync(updateCourseTopic, course);

            return course;
        }

        /// <summary>
        /// Delete a course
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteCourse(Guid id)
        {
            //First get the course from the list
            return await _coursesRepository.Delete(id);
        }
    }
}
