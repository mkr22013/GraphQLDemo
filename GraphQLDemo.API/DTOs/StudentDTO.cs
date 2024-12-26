﻿using System.ComponentModel.DataAnnotations;

namespace GraphQLDemo.API.DTOs
{
    public class StudentDTO
    {
        [Key]
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public double? GPA { get; set; }

        /// <summary>
        /// Navigation Property
        /// </summary>
        public IEnumerable<CourseDTO> Courses { get; set; }
    }
}
