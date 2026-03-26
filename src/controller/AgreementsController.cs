using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using src.dtos.AgreementsDto;
using src.dtos.AgrementsDto;
using src.services.interfaces;

namespace src.controller;
[Route("Api/[controller]")]
[ApiController]
public class AgreementsController (IAgreementsServices agreementsServices): ControllerBase
{
    private readonly IAgreementsServices _agreementsServices = agreementsServices;

    [HttpGet()]
    [Authorize]
    public async Task<ActionResult<ResponseGetAgreements>> getAll()
    {
        try{
            var response = await _agreementsServices.GetAllAgreements();
            if(!response.Success)
            {
                return BadRequest(new
                {
                    Message= response.Message
                });
            }
            return Ok(new
            {
                Message = response.Message,
                Data = response.Data
            });
            
        }catch(Exception ex){
            return StatusCode(500,"ERROR: consulte con el administrador" + ex.Message);
            
        }
    }
    [HttpGet("{id}")]
    [Authorize]
    public async Task<ActionResult<ResponseGetAgreements>> getById(int id)
    {
        try
        {
            var response = await _agreementsServices.GetAgreementById(id);
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
                Data = response.Data
            });
            
        }catch(Exception ex)
        {
            return StatusCode(500,"ERROR: consulte con el administrador" + ex.Message);
        }
    }
    [HttpPost()]
    [Authorize]
    public async Task<ActionResult<ResponseCreateAgreements>> createAgre([FromBody] CreateAgreements createAgreements)
    {
        try
        {
            var response = await _agreementsServices.CreateAgreement( createAgreements);
            if (!response.Success)
            {
                return BadRequest(new
                {
                   Message= response.Message 
                });
            }
            return CreatedAtAction(
                nameof(getById),
                new {id = response.Data?.Id},
                new
                {
                    Message = response.Message,
                    Data = response.Data
                }
            );
            
        }   catch(Exception err)
        {
            return StatusCode(500,"ERROR: consulte con el administrador " +err.Message);
        }
    }
    
    [HttpPatch("{id}")]
    [Authorize]
    public async Task<ActionResult<ResponseUpdateAgreements>> updateAgre(int id, UpdateAgreements updateAgreements)
    {
        try
        {
            var response = await _agreementsServices.UpdateAgreement(id,updateAgreements);
            if (!response.Success)
            {
                return BadRequest(new
                {
                   Message= response.Message 
                }); 
            }
            return Ok(new
            {
                Message = response.Message,
                Data = response.Data
            });
            
        }catch(Exception err)
        {
            return StatusCode(500,"ERROR: consulte con el administrador " +err.Message);
        }
    }
    
    [HttpPatch("State/{id}")]
    [Authorize]
    public async Task<ActionResult<ResponseUpdateStateAgreements>> updateState(int id)
    {
        try
        {
            var response = await _agreementsServices.UpdateStateAgreement(id);
            if (!response.Success)
            {
                return BadRequest(new
                {
                   Message= response.Message 
                });    
            }
            return Ok(new
            {
                Message = response.Message
            });

        }catch(Exception ex)
        {
            return StatusCode(500,"ERROR: consulte con el administrador " +ex.Message);

        }
    }
    [HttpDelete("{id}")]
    [Authorize]
    public async Task<ActionResult<ResponseDeleteAgreements>> deleteAgre(int id)
    {
        try
        {
            var response = await _agreementsServices.DeleteAgreement(id);
            if (!response.Success)
            {
                return BadRequest(new
                {
                   Message= response.Message 
                });   
                
            }
            return Ok(new
            {
                Message = response.Message
            });
        }catch(Exception ex)
        {
            return StatusCode(500,"ERROR: consulte con el administrador " +ex.Message);
         
        }
    }

}