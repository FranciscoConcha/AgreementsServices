using Microsoft.EntityFrameworkCore;
using src.context;
using src.dtos;
using src.services.interfaces;

namespace src.services;

public class CardServices(CardDbContext context) : ICardServices
{
    private readonly CardDbContext _context = context;
    public async Task<ResponseViewCardListDto> GetAllCard()
    {
        try
        {
            var cards = await _context.Cards
                                    .Include(u=>u.Student)
                                    .ToListAsync();
            if (cards == null || !cards.Any())
            {
                return new ResponseViewCardListDto
                {
                    Message = "No se encontraron datos",
                    Success =false
                };
            }
            var cardList = cards.Select(card =>new ViewCardListDto
            {
                Id = card.Id,
                IdCardPublic = card.Idpublic,
                NameStudent = card.Student?.Name ?? "Estudiante sin nombre",
                PeriodStundet = card.ValidDate,
                Career = card.Student?.Career ?? "Estudiante sin carrera",
                Uses = card.Uses
            }).ToList();
            return new ResponseViewCardListDto
            {
                Message = "Se encontraron datos",
                Success =true,
                ViewCardList = cardList
            
            };
        }
        catch(Exception ex)
        {
            return new ResponseViewCardListDto
            {
                Message = "Error en la consulta" + ex.Message,
                Success =false
            };
        }
    }

    public async Task<ResponseViewCardUnicDto> GetByCard(int Id)
    {
        try
        {
            var Cards = await _context.Cards
                                    .Include(u=>u.Student)
                                    .FirstOrDefaultAsync(s =>s.Id == Id);
            if(Cards == null)
            {
                return new ResponseViewCardUnicDto
                {
                    Message = "No se encontraron Estudiantes",
                    Success =false  
                };
            }
            return new ResponseViewCardUnicDto
            {
                Message = "Estudiantes encontrados",
                Success =true,
                ViewCardList = new ViewCardListDto
                {
                    Id = Cards.Id,
                    IdCardPublic = Cards.Idpublic,
                    NameStudent = Cards.Student?.Name ?? "Estudiante sin nombre",
                    PeriodStundet = Cards.ValidDate,
                    Career = Cards.Student?.Career ?? "Estudiante sin carrera",
                    Uses = Cards.Uses
                }          
            };
                                         
        }
        catch(Exception ex)
        {
            return new ResponseViewCardUnicDto
            {
                Message = "Error en la consulta " + ex.Message,
                Success =false
            };
        }
    }

    public async Task<ResponseCardByIdPublic> GetByIdPublic(InputDataCardByIdPublic input)
    {
        try
        {
            var response = await _context.Cards
                                        .Include(s =>s.Student)
                                        .FirstOrDefaultAsync(s=>s.Idpublic == input.PublicIdCard);

            var isActiveStudent = response?.Student?.IsActive ?? false;
            if(response == null || !isActiveStudent)
            {
                return new ResponseCardByIdPublic
                {
                    Success = false,
                    Message ="No se encontraron datos"
                };
            }
            var completName = response?.Student?.Name ?? "Nombre no encontrado";
            var carrerStudent = response?.Student?.Career ?? "Carrera no encontrada";
            return new ResponseCardByIdPublic
            {
                Success =true,
                Message ="Se encontraron datos",
                Data = new CardDto
                {
                    Idpublic = input.PublicIdCard,
                    StudentName = completName,
                    CareerStudent = carrerStudent
                }
            };
        }catch(Exception err)
        {
            return new ResponseCardByIdPublic
            {
                Success =false,
                Message = "ERROR: PROBLEMAS EN EL SERVIDOR " +err.Message
            };
        }
        throw new NotImplementedException();
    }

    public async Task<ResponseViewCardDto> GetCardForStudentView(string rutStudent)
    {
        try
        {
            var card = await _context.Cards
                                        .Include(u=>u.Student)
                                        .FirstOrDefaultAsync(s => s.Student != null
                                                        && s.Student.IsActive == true
                                                        && s.Student.Rut == rutStudent);

            if(card == null)
            {
                return new ResponseViewCardDto
                {
                    Meessage = "No se encontraron datos",
                    Success =false
                };
            }
            return new ResponseViewCardDto
            {
                Meessage="Se encontraron datos",
                Success =true,
                ViewCardDto = new ViewCardDto
                {
                    IdCardPublic = card.Idpublic,
                    NameStudent = card.Student?.Name ?? "Usuario sin nombre",
                    Rut = card.Student?.Rut ?? "Usuario sin rut",
                    PeriodStundet = card.ValidDate,
                    Career = card.Student?.Career ?? "Usuario sin carrera"
                }
            };
            
        }catch(Exception ex)
        {
            return new ResponseViewCardDto
            {
                Meessage = "Error en la consulta " + ex.Message,
                Success =false
            };
        }
    }

    public async Task<ResponseVerificateRut> GetVerificateRut(string Rut)
    {
        try
        {
            var student = await _context.Students
                                        .AnyAsync(s => s.Rut == Rut && s.IsActive == true);
            return new ResponseVerificateRut
            {
                Message = student ? "RUT verificado" : "RUT no encontrado",
                IsValid = student
            };
        }
        catch(Exception ex)
        {
            return new ResponseVerificateRut
            {
                Message = "Error en la consulta" + ex.Message,
                IsValid = false
            };
        }
    }

    public async Task<ResponseUseDto> UpdateUseCard(UpdateUseDto updateUseDto)
    {
        try
        {
            var data = await _context.Cards
                                    .Include(u=>u.Student)
                                    .FirstOrDefaultAsync(u =>u.Idpublic == updateUseDto.PublicIdCard);
            var isActive = data?.Student?.IsActive ?? false;
            Console.Write(data);
            if(data == null || !isActive)
            {
                return new ResponseUseDto
                {
                    Succes = false,
                    Message = "No existe el usuario"
                };
            }
            data.Uses++;
            await _context.SaveChangesAsync();
            return new ResponseUseDto
            {
                Succes = true,
                Message = "Se aumento el uso"
            };
        }catch(Exception err)
        {
            return new ResponseUseDto
            {
                Succes =false,
                Message = "ERROR: Error en el servidor " + err.Message
            };
        }
    }
}