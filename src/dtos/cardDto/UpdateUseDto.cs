namespace src.dtos;

public class UpdateUseDto
{
    public string PublicIdCard {get;set;} = string.Empty;
    
}
public class ResponseUseDto
{
    public bool Succes {get;set;}
    public string Message {get;set;} = string.Empty;
}