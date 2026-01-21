using src.dtos;

namespace src.services.interfaces;

public interface IUserServices
{
    Task<ResponseViewUserListData> GetAllUser();
    Task<ResponseViewUserData> GetUserById(int id);
    Task<ResponseViewUserData> UpdateStateUserById(int id);
    Task<ResponseViewUserData> UpdateUser(int id, UpdateUserDto input);
    
}