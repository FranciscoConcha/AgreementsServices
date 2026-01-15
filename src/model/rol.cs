using src.model;

namespace src.model;

public class RolModel
{
    public int Id {get;set;}

    public string Name {get;set;} ="";

    public ICollection<UserModel> Users {get;set; }= [];
    

}