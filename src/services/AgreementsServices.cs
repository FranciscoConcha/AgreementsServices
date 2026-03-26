
using Microsoft.EntityFrameworkCore;
using src.context;
using src.dtos.AgreementsDto;
using src.dtos.AgrementsDto;
using src.model;
using src.services.interfaces;

namespace src.services;

public class AgreementsServices(CardDbContext context) : IAgreementsServices
{
    private readonly CardDbContext _context = context;

    public async Task<ResponseCreateAgreements> CreateAgreement(CreateAgreements agreement)
    {
        try
        {
            if(agreement == null)
            {
                return new ResponseCreateAgreements
                {
                    Success = false,
                    Message = "No se enviaron datos"
                };
            }
            var agre = new AgreementsModel
            {
                Name = agreement.Name,
                Description = agreement.Description,
                StartDate = agreement.StartDate,
                EndDate = agreement.EndDate,
                IsActive = true,
                ForWhom = agreement.ForWhom

            };
            await _context.Agreements.AddAsync(agre);
            await _context.SaveChangesAsync();
            return new ResponseCreateAgreements
            {
                Success = true,
                Message = "Se logro crear un nuevo convenio",
                Data = new()
                {
                    Id = agre.Id,
                    Name = agre.Name,
                    Description = agre.Description,
                    StartDate = agre.StartDate,
                    EndDate = agre.EndDate,
                    IsActive = true,
                    ForWhom = agre.ForWhom
                }
            };
        }
        catch(Exception Err)
        {
            return new ResponseCreateAgreements
            {
                Success = false,
                Message = "Error al crear un convenio nuev " + Err.Message,
                Data = null
            };
        }
    }

    public async Task<ResponseDeleteAgreements> DeleteAgreement(int id)
    {
        try
        {
            var data = await _context.Agreements.FirstOrDefaultAsync(s => s.Id == id);
            if(data == null)
            {
                return new ResponseDeleteAgreements
                {
                    Success = false,
                    Message = "No se encuentran datos"
                };
            }
            await _context.Agreements.Where(s => s.Id == id).ExecuteDeleteAsync();
            await _context.SaveChangesAsync();
            return new ResponseDeleteAgreements
            {
                Success = true,
                Message ="Se logro eliminar los datos correctamente"
            };
        }
        catch(Exception Err)
        {
            return new ResponseDeleteAgreements
            {
                Success = false,
                Message = "Error al eliminar el convenio: " + Err.Message
            };
        }
    }

    public async Task<ResponseGetAgreements> GetAgreementById(int id)
    {
        try
        {
            var data = await _context.Agreements.FirstOrDefaultAsync(s=>s.Id == id);
            if (data == null)
            {
                return new ResponseGetAgreements
                {
                    Success =false,
                    Message ="No se encontraron los datos"
                };
            } 
            return new ResponseGetAgreements
            {
                Success = true,
                Message ="Se logranron encontrar los datos",
                Data =
                [
                    new() {
                        Id = data.Id,
                        Name = data.Name,
                        Description = data.Description,
                        StartDate = data.StartDate,
                        EndDate = data.EndDate,
                        IsActive = data.IsActive,
                        ForWhom = data.ForWhom
                    }
                ]
            };
        }
        catch(Exception Err)
        {
            return new ResponseGetAgreements
            {
                Success =false,
                Message ="No se logro encontrar el dato " + Err.Message
            };
        }
    }

    public async Task<ResponseGetAgreements> GetAllAgreements()
    {
        try
        {
            var data = await _context.Agreements.ToListAsync();
            if (data.Count == 0)
            {
                return new ResponseGetAgreements{
                   Success =false,
                   Message = "No se encontraron datos" 
                };
            }
            return new ResponseGetAgreements
            {
                Success = true,
                Message="Se encontraron datos",
                Data = [.. data.Select(x=>new GetAgreementsDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    IsActive = x.IsActive,
                    ForWhom = x.ForWhom
                })]
            };
            
        }catch(Exception Err)
        {
            return 
                new ResponseGetAgreements
                {
                    Success =false,
                    Message = "No se logro encontrar datos" + Err.Message
                }
            ;
        }
    }

    public async Task<ResponseUpdateAgreements> UpdateAgreement(int id, UpdateAgreements agreement)
    {
        try
        {
            var data = await _context.Agreements.FirstOrDefaultAsync(s => s.Id == id);
            if(data == null)
            {
                return new ResponseUpdateAgreements
                {
                    Success = false,
                    Message ="No se encontraron datos"
                };
            }
            data.Description = agreement.Description;
            data.StartDate = agreement.StartDate;
            data.EndDate = agreement.EndDate;
            data.ForWhom = agreement.ForWhom;
            await _context.SaveChangesAsync();
            return new ResponseUpdateAgreements{
                Success =false,
                Message = "Se actualizaron los datos",
                Data = data
            };
            
        }
        catch(Exception Err)
        {
            return new ResponseUpdateAgreements
            {
                Success =false,
                Message="ERROR: "+ Err.Message
            };
        }
        throw new NotImplementedException();
    }

    public async Task<ResponseUpdateStateAgreements> UpdateStateAgreement(int id)
    {
        try
        {
            var data = await _context.Agreements.FirstOrDefaultAsync(s=> s.Id ==id);
            if(data == null)
            {
                return new ResponseUpdateStateAgreements
                {
                    Success =false,
                    Message= "No se encontraron datos"
                };

            }
            data.IsActive = data.IsActive!;
            await _context.SaveChangesAsync();
            return new ResponseUpdateStateAgreements
            {
              Success =true,
              Message ="Estado cambiado"  
            };
        }catch(Exception Err)
        {
            return new ResponseUpdateStateAgreements
            {
                Success = false,
                Message = "Error  " + Err.Message
            };
        }
    }
}