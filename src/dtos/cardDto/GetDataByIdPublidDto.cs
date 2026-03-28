
namespace src.dtos;

public class InputDataCardByIdPublic
{
    public string PublicIdCard {get;set;} = string.Empty;
}
public class CardDto
{
    

    public string Idpublic
    {
        get; set;
    } = string.Empty;

    
    public string StudentName{get;set;} = string.Empty;
    public string CareerStudent {get;set;} = string.Empty;


}
public class ResponseCardByIdPublic
{
    public bool Success {get;set;}
    public string Message {get;set;} = string.Empty;
    public CardDto? Data {get;set;}
};