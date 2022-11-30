namespace API.Extensions;
public static class UserManagerExtenstions
{
    public static async Task<UserEntity> FindUserByEmailWithAddressAsync(
        this UserManager<UserEntity> userManager,
        ClaimsPrincipal user)
    {
        return await userManager.Users
            .Include(_ => _.Address)
            .FirstOrDefaultAsync(_ => _.Email == user.FindFirstValue(ClaimTypes.Email));
    }
}
