namespace src.dtos;

public class ViewStudentDto
{
    public int Id 
    {
        get;
        set;
    }
    public string Name 
    {
        get;
        set;
        } =null!;

    public string Rut
    {
        get; set;
    } = string.Empty;

    public string Career
    {
        get; set;
    } = string.Empty;
    
    public bool IsActive
    {
        get;
        set;
    }
}

public class ResponseViewStudent
{
    public string Message 
    {
        get;
        set;
    } ="";
    public bool Success {get;set;}

    public ViewStudentDto ViewStudentDto
    {
        get;
        set;
    } = null!;
}

public class ResponseViewListStudentDto
{
    public string Message 
    {
        get;
        set;
    } ="";
    public bool Success {get;set;}
    public List<ViewStudentDto>? ViewStudentDto
    {
        get;
        set;
    } = [];
    public List<string>? Errors {get;set;} 

}
