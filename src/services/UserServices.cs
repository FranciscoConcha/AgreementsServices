using Microsoft.EntityFrameworkCore;
using src.context;
using src.dtos;
using src.services.interfaces;

namespace src.services;

public class UserServices(CardDbContext context) : IUserServices
{
    private readonly CardDbContext _context = context;
    public async Task<ResponseViewUserListData> GetAllUser()
    {
        try
        {
            var users = await _context.Users.ToListAsync();
            if (users == null || !users.Any())
            {
                return new ResponseViewUserListData
                {
                    Message = "No se encontraron datos",
                    Success =false
                };
            }
            var usersList = users.Select(users =>new UserData
            {
                Id = users.Id,
                Name = users.Name,
                Email = users.Email,
                Charge = users.Charge,
                IsActive = users.IsActive
            }).ToList();
            return new ResponseViewUserListData
            {
                Message = "Se encontraron datos",
                Success =true,
                Users = usersList
            
            };
        }
        catch(Exception ex)
        {
            return new ResponseViewUserListData
            {
                Message = "Error en la consulta" + ex.Message,
                Success =false
            };
        }
    }

    public async Task<ResponseViewUserData> GetUserById(int id)
    {
        try
        {
            var users = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (users == null )
            {
                return new ResponseViewUserData
                {
                    Message = "No se encontraron datos",
                    Success =false
                };
            }
            var userData = new UserData
            {
                Id = users.Id,
                Name = users.Name,
                Email = users.Email,
                Charge = users.Charge,
                IsActive = users.IsActive
            };
            return new ResponseViewUserData
            {
                Message = "Se encontraron datos",
                Success =true,
                Users = userData
            
            };
        }
        catch(Exception ex)
        {
            return new ResponseViewUserData
            {
                Message = "Error en la consulta" + ex.Message,
                Success =false
            };
        }
    }

    public async Task<ResponseViewUserData> UpdateStateUserById(int id)
    {
        try
        {
            var users = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (users == null )
            {
                return new ResponseViewUserData
                {
                    Message = "No se encontraron datos",
                    Success =false
                };
            }
            users.IsActive = !users.IsActive;
            await _context.SaveChangesAsync();
            var userData = new UserData
            {
                Id = users.Id,
                Name = users.Name,
                Email = users.Email,
                Charge = users.Charge,
                IsActive = users.IsActive
            };
            return new ResponseViewUserData
            {
                Message = "Se encontraron datos",
                Success =true,
                Users = userData
            
            };
        }
        catch(Exception ex)
        {
            return new ResponseViewUserData
            {
                Message = "Error en la consulta" + ex.Message,
                Success =false
            };
        }    }

    public async Task<ResponseViewUserData> UpdateUser(int id, UpdateUserDto input)
    {
        try
        {
            if(input == null)
            {
                return new ResponseViewUserData
                {
                    Message = "No se enviaron datos para actualizar",
                    Success =false
                };
                
            }
            var users = await _context.Users.FirstOrDefaultAsync(s => s.Id == id);
            if (users == null )
            {
                return new ResponseViewUserData
                {
                    Message = "No se encontraron datos",
                    Success =false
                };
            }
            users.Name = input.Name ?? users.Name;
            users.Email = input.Email ?? users.Email;
            users.Password = input.Password ?? users.Password;
            users.Charge = input.Charge ?? users.Charge;
            users.IsActive = input.IsActive ?? users.IsActive;
            var userData = new UserData
            {
                Id = users.Id,
                Name = users.Name,
                Email = users.Email,
                Charge = users.Charge,
                IsActive = users.IsActive
            };
            return new ResponseViewUserData
            {
                Message = "Se encontraron datos",
                Success =true,
                Users = userData
            
            };
        }
        catch(Exception ex)
        {
            return new ResponseViewUserData
            {
                Message = "Error en la consulta" + ex.Message,
                Success =false
            };
        }
    }
}