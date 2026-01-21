using src.dtos;

namespace src.services.interfaces;

public interface IStudentServices
{
    Task<ResponseViewListStudentDto> GetAllStudent();

    Task<ResponseViewStudent> GetStudentById(int Id);
    
    Task<ResponseViewStudent> UpdateStateById(int Id );

    Task<ResponseViewStudent> UpdateById(int Id, UpdateStudentDto Intput);
    Task<ResponseViewStudent> CreateStudent(CreateStudentDto Intput);

    Task<ResponseViewListStudentDto> CreateStudentByExcel(IFormFile file);
    

}