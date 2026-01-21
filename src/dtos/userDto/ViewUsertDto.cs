namespace src.dtos;

public class UserData
{
    public int Id {get;set;}

    public string Name {get;set;} = null!;

    public string Email {get;set;} = null!;
    
    public string Charge {get;set;} = null!;
    
    public bool IsActive {get;set;}

}

public  class ResponseViewUserData
{
    public string Message {get;set;} = "";
    public bool Success {get;set;}

    public UserData? Users {get;set;}
}
public  class ResponseViewUserListData
{
    public string Message {get;set;} = "";
    public bool Success {get;set;}

    public List<UserData>? Users {get;set;}
}