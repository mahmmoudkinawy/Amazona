namespace API.Entities;
public sealed class UserEntity : IdentityUser<int>
{
    public string? DisplayName { get; set; }
    public AddressEntity Address { get; set; }
}
