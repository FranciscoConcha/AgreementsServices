
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.dtos;
using src.services.interfaces;

namespace src.controller;
[ApiController]
[Route("api/[controller]")]
public class StudentController(IStudentServices studentServices) : ControllerBase
{
    private readonly IStudentServices _studentServices = studentServices;

    
    [HttpGet("")]
    [Authorize]
    public async Task<ActionResult<ResponseViewListStudentDto>> GetAll()
    {
        try
        {
            var result = await _studentServices.GetAllStudent();
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
                Student = result.ViewStudentDto
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
    [Authorize]
    public async Task<ActionResult<ResponseViewStudent>> GetById(int id)
    {
        try
        {
            var result = await _studentServices.GetStudentById(id);
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
                Student = result.ViewStudentDto
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }  
    }

    [HttpPatch("state/{id}")]
    [Authorize(Roles = "Admin,CEAL")]
    public async Task<ActionResult<ResponseViewStudent>> UpdateState(int id)
    {
        try
        {
            var result = await _studentServices.UpdateStateById(id);
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
                Student = result.ViewStudentDto
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }  
    }

    [HttpPatch("update/{id}")]
    [Authorize(Roles = "Admin,CEAL")]
    public async Task<ActionResult<ResponseViewStudent>> UpdateStudent(int id, [FromBody] UpdateStudentDto input)
    {
        try
        {
            var result = await _studentServices.UpdateById(id, input);
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
                Student = result.ViewStudentDto
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }  
    }

    [HttpPost("")]
    [Authorize(Roles = "Admin,CEAL")]
    public async Task<ActionResult<ResponseViewStudent>> CreateStudent([FromBody] CreateStudentDto input)
    {
        try
        {
            var result = await _studentServices.CreateStudent( input);
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
                Student = result.ViewStudentDto
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }  
    }
    [HttpPost("Excel")]
    [Authorize(Roles = "Admin,CEAL")]
    public async Task<ActionResult<ResponseViewStudent>> CreateStudentExcel([FromForm] IFormFile input)
    {
        try
        {
            var result = await _studentServices.CreateStudentByExcel(input);
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
                Student = result.ViewStudentDto,
                Error = result.Errors
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