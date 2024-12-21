namespace GraphQLDemo.API.Schema
{
    /// <summary>
    /// Domain Entity for Student
    /// </summary>
    public class StudentType
    {
        public Guid Id { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        [GraphQLName("gpa")]
        public double? GPA { get; set; }
    }
}
