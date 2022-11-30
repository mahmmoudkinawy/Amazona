namespace API.DTOs;
public sealed class LoginDto
{
    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public string Password { get; set; }
}
