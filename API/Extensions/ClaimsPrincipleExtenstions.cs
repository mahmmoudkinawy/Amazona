namespace API.Extensions;
public static class ClaimsPrincipleExtenstions
{
    public static string RetrieveEmailFromPrinciple(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(ClaimTypes.Email);
    }
}
