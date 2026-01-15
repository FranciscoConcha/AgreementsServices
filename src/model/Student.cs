namespace src.model.student;
using src.model;

public class StudentModel{
        public int Id {get;set;}    
        public string Name{get;set;} = "";
        public string Rut {get;set;} = "";
        public string Career {get;set;} = "";

        public CardModel? Card {get;set;}

        public bool IsActive {get;set;}
        
        

    }
