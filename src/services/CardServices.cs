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
                                    .Include(u=>u.Uses)
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
                                    .Include(u=>u.Uses)
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

    public async Task<ResponseViewCardDto> GetCardForStudentView(string rutStudent)
    {
        try
        {
            var card = await _context.Cards
                                        .Include(u=>u.Uses)
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
}