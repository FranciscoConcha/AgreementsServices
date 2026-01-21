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

    [HttpGet()]
    public async Task<ActionResult<ResponseViewCardDto>> GetCardForStudent(string rut)
    {
        try
        {
            var response = await _cardServices.GetCardForStudentView(rut);
            if (response.Success)
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
             return BadRequest(new
            {
                Message= "ERROR: Servidor con problemas " +ex.Message
            });
        }
    }
}