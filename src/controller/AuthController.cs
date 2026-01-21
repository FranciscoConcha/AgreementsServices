using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.dtos.userDto;
using src.services.interfaces;

namespace src.controller;
[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthServices authServices): ControllerBase
{
    private readonly IAuthServices _authServices = authServices;

    [HttpPost("Login")]
    public async Task<ActionResult<ResponseRegisterDto>> Login(LoginDto input)
    {
        try
        {
            var response =await _authServices.LoginUser(input);
            if (!response.Success)
            {
                return BadRequest(new
                {
                    Message = response.Message 
                });
            }
            return Ok( new
            {
                Message = response.Message,
                data = new
                {
                    Email = response.Email,
                    Token = response.Token,
                    Rol = response.Rol,
                    Charge = response.Charge 
                }
            });
        }catch(Exception ex)
        {
            return BadRequest(new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        };
    } 
    [HttpPost("Register")]
    [Authorize(Roles="Admin")]
    public async Task<ActionResult<ResponseRegisterDto>> Register(RegisterDto input)
    {
        try
        {
            var response =await _authServices.RegisterUser(input);
            if (!response.Success)
            {
                return BadRequest(new
                {
                    Message = response.Message 
                });
            }
            return Ok(new
            {
                Message = response.Message,
                data = response.RegisterDto
            });
        }
        catch(Exception ex)
        {
            return BadRequest(new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }
    }
}