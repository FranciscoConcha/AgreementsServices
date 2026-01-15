using src.dtos.cardDTO;
using src.services.interfaces;

namespace src.services;

public class CardServices : ICardServices
{
    public Task<ResponseViewCardDto> GetCardForStudent(string rutStudent)
    {
        throw new NotImplementedException();
    }
}