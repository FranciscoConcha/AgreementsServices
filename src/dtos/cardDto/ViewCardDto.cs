namespace src.dtos.cardDTO;

public class ViewCardDto
{
    public string IdCardPublic {get;set;} = "";
    public string NameStudent {get;set;} ="";
    public DateTime PeriodStundet {get;set;}
    public string Career {get;set;} = "";

}

public class ResponseViewCardDto
{
    public string Meessage {get;set;} ="";
    public ViewCardDto ViewCardDto {get;set;} =null!;
}

