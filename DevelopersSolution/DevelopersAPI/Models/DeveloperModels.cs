namespace DevelopersAPI.Models;

public record DeveloperDetailsModel(string Id, string FirstName, string LastName, string PhoneNumber, string Email);

public record DeveloperSummaryModel(string Id, string FirstName, string LastName, string Email);

public class CollectionResponse<T>
{
    public List<T> Data { get; set; } = new();
}

public record DeveloperCreateModel(string FirstName, string LastName, string Email, string Phone);