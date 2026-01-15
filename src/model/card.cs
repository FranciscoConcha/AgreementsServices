namespace src.model.card;
using src.model.student;

public class CardModel
{
    public int Id {get;set;}
    public string Idpublic {get;set;} ="";

    public int Uses {get;set;}

    public int StudentId {get;set;}
    
    public StudentModel? Student {get;set;}
}