using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabate.APIs.Dtos;
using Talabate.APIs.Error;
using Talabate.APIs.Extantions;
using Talabate.Core.Entity.Identity;
using Talabate.Core.IServices;

namespace Talabate.APIs.Controllers
{

    public class AccountController : BaseApiController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthServices _authServices;
        private readonly IMapper _mapper;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IAuthServices authServices, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authServices = authServices;
            _mapper = mapper;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null) return Unauthorized(new ApiResponse(401, "Invalid Login"));
            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401, "Invalid Login"));
            return Ok(new UserDto { DisplayName = user.DisplayName, Email = user.Email, Token = await _authServices.CreateTokenAsync(user, _userManager) });
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto model)
        {
            if(CheckEmailExistsAsync(model.Email).Result.Value)
            {
                return BadRequest(new ApiValidationErrorResponse() { Errors = new string[] { "Email address is in use" } });
            }
            var user = new ApplicationUser()
            {
                Email = model.Email,
                DisplayName = model.DisplayName,
                PhoneNumber = model.Phone,
                UserName = model.Email.Split("@")[0]

            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded) return BadRequest(new ApiValidationErrorResponse() { Errors = result.Errors.Select(e => e.Description) });

            return Ok(new UserDto() { DisplayName = user.DisplayName, Email = user.Email, Token = await _authServices.CreateTokenAsync(user, _userManager) });


        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(new UserDto()
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                Token = await _authServices.CreateTokenAsync(user, _userManager)
            });
        }

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<AddressDto>> GetAddress()
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            var userAddress = _mapper.Map<AddressDto>(user.Address);

            return Ok(userAddress);

        }
        [Authorize]
        [HttpPut("Address")]
        public async Task<ActionResult<AddressDto>> UpdateAddress(AddressDto addressDto)
        {
            var user = await _userManager.FindUserWithAddressAsync(User);
            user.Address = _mapper.Map<Address>(addressDto);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400, "Problem updating the user"));
            return Ok(_mapper.Map<AddressDto>(user.Address));
        }

        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery] string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return Ok(user != null);
        }
    }
}
