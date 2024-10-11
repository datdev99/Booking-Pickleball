using AutoMapper;
using BookingApi.Models.DTO.UserDto;
using BookingApi.Repositories.Token;
using BookingApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;
        private readonly IMapper _mapper;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository, IMapper mapper)
        {
            this._userManager = userManager;
            this._tokenRepository = tokenRepository;
            this._mapper = mapper;
        }

        //API đăng ký
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto model)
        {
            var response = new ServiceResponse<GetUserDto>();
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            var data = _mapper.Map<GetUserDto>(model);

            if (result.Succeeded)
            {
                response.Data = data;
                response.Message = "User registered successfully";

                return Ok(response);
            }
            response.Success = false;
            response.Message = string.Join(", ", result.Errors.Select(e => e.Description));
            return BadRequest(response);
        }


    }
}
