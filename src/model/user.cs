using src.model;

namespace src.model;

public class UserModel
{
    public int Id {get;set;}
    public string Name {get;set;} ="";
    public string Email {get;set;} = "";
    public string Password{get;set;} = "";
    public string Charge {get;set;} = "";
    public bool IsActive {get;set;}
    public int RolId {get;set;}
    public RolModel Rol {get;set;} = null!;
}
