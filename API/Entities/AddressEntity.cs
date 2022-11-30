namespace API.Entities;

[Table("Addresses")]
public sealed class AddressEntity
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? ZipCode { get; set; }

    public int UserId { get; set; }
    public UserEntity User { get; set; }
}