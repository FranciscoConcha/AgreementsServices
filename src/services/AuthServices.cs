using Microsoft.EntityFrameworkCore;
using src.context;
using src.dtos.userDto;
using src.services.interfaces;
using src.utils;
using src.model;
using BCrypt.Net;
using System.IO.Compression;
using System.ComponentModel.DataAnnotations;
public class AuthServices(JwtUtils jwtUtils, CardDbContext context) : IAuthServices
{
    private readonly JwtUtils _utils = jwtUtils;
    private readonly CardDbContext _context = context;

    public async Task<ResponseLoginDto> LoginUser(LoginDto dataInput)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == dataInput.Email) ;
            
            if (user == null)
            {
                return new ResponseLoginDto
                {
                    Message ="Usuario no encontrado"
                };
            }
            

            if (!BCrypt.Net.BCrypt.Verify( dataInput.Password,user.Password))
            {
                return new ResponseLoginDto
                {
                    Message ="Contraseña incorrecta"
                };
            }

            var token = _utils.CreateToken(user);

            return new ResponseLoginDto
            {
                Message= "Usuario registrado",
                Email = user.Email,
                Token = token,
                Rol = user.Rol.Name,
                Charge = user.Charge
            };     
       }
        catch(Exception err)
        {
            return new ResponseLoginDto
                {
                    Message = "Error en el login " + err.Message
                };
        }
    }

    public async Task<ResponseRegisterDto> RegisterUser(RegisterDto dataInput)
    {
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u =>u.Email == dataInput.Email);
            if(user != null)
            {
                return new ResponseRegisterDto
                {
                    Message = "Email Registrado"
                };

            }
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dataInput.Password);   

            var rol = await _context. Roles.FirstOrDefaultAsync(r =>r.Name == dataInput.RolName);
            if(rol == null)
            {
                return new ResponseRegisterDto
                {
                    Message ="ROl no encontrado"
                };
            }
            var  userCreate = new UserModel
            {
                Name  = dataInput.Name,
                Email = dataInput.Email,
                Password = passwordHash,
                Charge = dataInput.Charge,
                IsActive = true,
                RolId = rol.Id
            };
            
            _context.Add(userCreate);
            await _context.SaveChangesAsync();
            return new ResponseRegisterDto
            {
                Message ="Usuario Creado",
                RegisterDto = dataInput
            };
        }
        catch(Exception err)
        {
            return new ResponseRegisterDto
                {
                    Message = "Error en el registro" + err.Message
                };
        }
    }
}