using src.dtos.cardDTO;

namespace src.services.interfaces;

public interface ICardServices{
    
    Task<ResponseViewCardDto> GetCardForStudent(string rutStudent);


}