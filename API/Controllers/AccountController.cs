namespace API.Controllers;

[Route("api/account")]
[ApiController]
public sealed class AccountController : ControllerBase
{
    private readonly UserManager<UserEntity> _userManager;
    private readonly SignInManager<UserEntity> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;

    public AccountController(
        UserManager<UserEntity> userManager,
        SignInManager<UserEntity> signInManager,
        ITokenService tokenService,
        IMapper mapper)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    [Authorize]
    [HttpGet("current-user")]
    public async Task<IActionResult> GetCurrentUser()
    {
        var user = await _userManager.FindByEmailAsync(User.FindFirstValue(ClaimTypes.Email));

        return Ok(new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        });
    }

    [HttpGet("email-exists")]
    public async Task<IActionResult> CheckEmailExists([FromQuery] string email)
    {
        return Ok(await UserExists(email));
    }

    [Authorize]
    [HttpGet("address")]
    public async Task<IActionResult> GetCurrentUserAddress()
    {
        var user = await _userManager.FindUserByEmailWithAddressAsync(User);

        return Ok(_mapper.Map<AddressDto>(user.Address));
    }

    [Authorize]
    [HttpPut("address")]
    public async Task<IActionResult> UpdateUserAddress([FromBody] AddressDto address)
    {
        var user = await _userManager.FindUserByEmailWithAddressAsync(User);

        user.Address = _mapper.Map<AddressEntity>(address);

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            return BadRequest("Problem Updating the Address");
        }

        return Ok(_mapper.Map<AddressDto>(user.Address));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = await _userManager.FindByEmailAsync(loginDto.Email);

        if (user is null)
        {
            return Unauthorized("Invalid email or password");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

        if (!result.Succeeded)
        {
            return Unauthorized("Invalid email or password");
        }

        return Ok(new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        });
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (await UserExists(registerDto.Email))
        {
            return BadRequest("Email already exists");
        }

        var user = new UserEntity
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            UserName = registerDto.Email
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            var errors = new List<string>();
            foreach (var error in result.Errors)
            {
                errors.Add(error.Description);
            }

            return BadRequest(errors);
        }

        return Ok(new UserDto
        {
            DisplayName = user.DisplayName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        });
    }

    private async Task<bool> UserExists(string email)
    {
        return await _userManager.FindByEmailAsync(email) is not null;
    }
}
