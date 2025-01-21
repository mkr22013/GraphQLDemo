namespace GraphQLDemo.API.Schema.Queries
{
    /// <summary>
    /// Interface type which holds common property of class where it is implemented 
    /// </summary>
    [InterfaceType("SearchResult")]
    public interface ISearchResultType
    {
        Guid Id { get; }
    }
}
