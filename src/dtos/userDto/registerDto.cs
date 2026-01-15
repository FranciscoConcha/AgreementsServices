namespace src.dtos.userDto;

public class RegisterDto
{
    public string Name {get;set;} =null!;

    public string Email{get;set;} = null!;

    public string Password {get;set;} = null!;

    public string Charge {get;set;} =null!;

    public string RolName {get;set;} = null!;

}

public class ResponseRegisterDto
{
    public string? Message {get;set;}

    public RegisterDto? RegisterDto {get;set;}
}
