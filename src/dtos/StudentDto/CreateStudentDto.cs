namespace src.dtos;

public class CreateStudentDto
{
    public string Name {get;set;} = null!;
    public string Carrer {get;set;} =null!;
    public string Rut {get;set;} = null!;
}

public class CreateStudentByExcel
{
    public IFormFile File { get; set; } = null!;
}