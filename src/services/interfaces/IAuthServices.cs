using src.dtos;
using src.dtos.userDto;

namespace src.services.interfaces;

public interface IAuthServices
{
    Task<ResponseLoginDto> LoginUser(LoginDto dataInput);

    Task<ResponseRegisterDto> RegisterUser(RegisterDto dataInput);
}
