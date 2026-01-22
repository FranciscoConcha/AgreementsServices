using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.dtos;
using src.services.interfaces;

namespace src.controller;

[ApiController]
[Route("api/[controller]")]
public class UserController(IUserServices userServices) : ControllerBase
{
    private readonly IUserServices _userServices = userServices;

    [HttpGet()]
    [Authorize(Roles = "Admin,CEAL")]
    public async Task<ActionResult<ResponseViewUserListData>> GetAll()
    {
        try
        {
            var result = await _userServices.GetAllUser();
            if (!result.Success)
            {
                return BadRequest(new
                {
                    Message = result.Message 
                });
            }
            return Ok(new
            {
                Message = result.Message,
                Student = result.Users
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }  
    }
    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,CEAL")]
    public async Task<ActionResult<ResponseViewUserListData>> GetById(int id)
    {
        try
        {
            var result = await _userServices.GetUserById(id);
            if (!result.Success)
            {
                return BadRequest(new
                {
                    Message = result.Message 
                });
            }
            return Ok(new
            {
                Message = result.Message,
                Student = result.Users
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }  
    }
    [HttpPut("id")]
    [Authorize(Roles = "Admin,CEAL")]
    public async Task<ActionResult<ResponseViewUserListData>> UpdateUser(int id,[FromBody] UpdateUserDto input)
    {
        try
        {
            var result = await _userServices.UpdateUser(id,input);
            if (!result.Success)
            {
                return BadRequest(new
                {
                    Message = result.Message 
                });
            }
            return Ok(new
            {
                Message = result.Message,
                Student = result.Users
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }  
    }
    [HttpPatch("id")]
    [Authorize(Roles = "Admin,CEAL")]
    public async Task<ActionResult<ResponseViewUserListData>> UpdateStateUser(int id)
    {
        try
        {
            var result = await _userServices.UpdateStateUserById(id);
            if (!result.Success)
            {
                return BadRequest(new
                {
                    Message = result.Message 
                });
            }
            return Ok(new
            {
                Message = result.Message,
                Student = result.Users
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }  
    }
}