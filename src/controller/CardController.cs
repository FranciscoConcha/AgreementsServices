using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.dtos;
using src.dtos.userDto;
using src.services.interfaces;

namespace src.controller;
[ApiController]
[Route("api/[controller]")]
public class CardController(ICardServices cardServices) : ControllerBase
{
    private readonly ICardServices _cardServices = cardServices;

    [HttpGet("{rut}")]
    public async Task<ActionResult<ResponseViewCardDto>> GetCardForStudent(string rut)
    {
        try
        {
            var response = await _cardServices.GetCardForStudentView(rut);
            if (!response.Success)
            {
                return BadRequest(new
                {
                    Message = response.Meessage 
                });
            }    
            return Ok(new
            {
                Message = response.Meessage,
                Card = response.ViewCardDto 
            });
        }
        catch(Exception ex)
        {
             return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }
    }
    [HttpGet("")]
    [Authorize]
    public async Task<ActionResult<ResponseViewStudent>> GetAll()
    {
        try
        {
            var result = await _cardServices.GetAllCard();
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
                Student = result.ViewCardList
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }  
    }

    [HttpGet("id={id}")]
    [Authorize]
    public async Task<ActionResult<ResponseViewStudent>> GetById(int id)
    {
        try
        {
            var result = await _cardServices.GetByCard(id);
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
                Student = result.ViewCardList
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }  
    }
    [HttpGet("verificate/{rut}")]
    public async Task<ActionResult<ResponseVerificateRut>> GetVerificateRut(string rut)
    {
        try
        {
            var result = await _cardServices.GetVerificateRut(rut);
            if (!result.IsValid)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }catch(Exception ex)
        {
            return StatusCode(500,new
            {
                Message= "Error: Servidor con problemas " + ex.Message
            });
        }
    }
    [HttpPatch("")]
    public async Task<ActionResult<ResponseUseDto>> UpdateUseCard([FromBody] UpdateUseDto updateUseDto)
    {
        try
        {
            var response = await _cardServices.UpdateUseCard(updateUseDto);
            if (!response.Succes)
            {
                return BadRequest(response.Message);
            }
            return Ok(response);
        }catch(Exception err)
        {
            return StatusCode(500,
            "ERROR: EN EL SERVIDOR "+ err.Data);
        }
    }
}