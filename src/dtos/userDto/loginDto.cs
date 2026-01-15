namespace src.dtos.userDto;

public class LoginDto
{
    public string Email {get;set;} ="";
    public string Password {get;set;} = "";

}

public class ResponseLoginDto
{
    public string? Message {get;set;}

    public string? Email {get;set;}

    public string? Token {get;set;}
    public string? Rol {get;set;}

    public string? Charge {get;set;}
}
