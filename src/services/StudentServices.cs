using Microsoft.EntityFrameworkCore;
using src.context;
using src.dtos;
using src.model;
using src.model.student;
using src.services.interfaces;
using OfficeOpenXml;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;


namespace src.services;

public class StudentServices(CardDbContext context) : IStudentServices
{
    private readonly CardDbContext _context = context;
    public async Task<ResponseViewListStudentDto> GetAllStudent()
    {
        try
        {
            var student = await _context.Students.ToListAsync();
            if (student == null || !student.Any())
            {
                return new ResponseViewListStudentDto
                {
                    Message = "No se encontraron datos",
                    Success =false
                };
            }    
            var studentList = student.Select(s=> new ViewStudentDto
            {
                Id = s.Id,
                Name = s.Name,
                Rut = s.Rut,
                Career = s.Career,
                IsActive= s.IsActive
            }).ToList();
            return new ResponseViewListStudentDto
            {
                Message = "Datos encontrados",
                Success =true,
                ViewStudentDto = studentList
            };
        }
        catch(Exception er)
        {
            return new ResponseViewListStudentDto
            {
                Message = "Error en la consulta de estudiantes " + er.Message,
                Success =false
            };
        }
    }

    public async Task<ResponseViewStudent> GetStudentById(int Id)
    {
        try
        {
            var student = await _context.Students.FirstOrDefaultAsync(s=>s.Id == Id);
            if (student == null)
            {
                return new ResponseViewStudent
                {
                    Message = "No se encontraron datos",
                    Success =false
                };
            }            

            return new ResponseViewStudent
            {
                Message ="Datos encontrados",
                Success =true,
                ViewStudentDto = new ViewStudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Rut = student.Rut,
                    Career = student.Career,
                    IsActive = student.IsActive
                }
            };
        }
        catch(Exception ex)
        {
            return new ResponseViewStudent
            {
                Message = "Error en la consulta " + ex.Message,
                Success =false
            };
        }
    }

    public async Task<ResponseViewStudent> UpdateStateById(int Id)
    {
        try
        {
            var student = await _context.Students.FirstOrDefaultAsync(s =>s.Id == Id);
            if (student == null)
            {
                return new ResponseViewStudent
                {
                    Message = "No se encontro dato",
                    Success =false
                };
            }   
            student.IsActive = !student.IsActive;
            await _context.SaveChangesAsync();
            return new ResponseViewStudent
            {
                Message ="Se actualizo el estado",
                Success =true,
                ViewStudentDto = new ViewStudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Rut = student.Rut,
                    Career = student.Career,
                    IsActive = student.IsActive
                }
            };
        }
        catch(Exception ex)
        {
            return new ResponseViewStudent
            {
                Message = "Error en la consulta " + ex.Message,
                Success =false
            };
        }

    }

    public async Task<ResponseViewStudent> UpdateById(int Id, UpdateStudentDto Intput)
    {
        try
        {
            var update = await _context.Students.FirstOrDefaultAsync(s =>s.Id == Id);
            if (update == null)
            {
                return new ResponseViewStudent
                {
                    Message = "No se encontro dato",
                    Success =false
                };
            }  
            update.Name = Intput.Name ?? update.Name;
            update.Career = Intput.Carrer ?? update.Career;
            await _context.SaveChangesAsync();
            return new ResponseViewStudent
            {
                Message ="Se actualizo el estado",
                Success =true,
                ViewStudentDto = new ViewStudentDto
                {
                    Id = update.Id,
                    Name = update.Name,
                    Rut = update.Rut,
                    Career = update.Career,
                    IsActive = update.IsActive
                }
            };

        }
        catch(Exception ex)
        {
            return new ResponseViewStudent
            {
                Message = "Error en la consulta " + ex.Message,
                Success =false
            };
        }
    }

    public async Task<ResponseViewStudent> CreateStudent(CreateStudentDto Intput)
    {
        try
        {
            if (Intput == null)
            {
                return new ResponseViewStudent
                {
                    Message = "No se enviaron datos",
                    Success =false
                }; 
            }
            var student = new StudentModel
            {
                Name = Intput.Name,
                Career= Intput.Carrer,
                Rut = Intput.Rut,
                IsActive = true
            };
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();

            var card = new CardModel
            {
                Idpublic =  Guid.NewGuid().ToString(),
                Uses = 0,
                StudentId=student.Id,
                ValidDate=DateTime.UtcNow,
                Student =student
            };
            await  _context.Cards.AddAsync(card);
            await _context.SaveChangesAsync();
            return new ResponseViewStudent
            {
                Message = "Estudiante creado exitosamente",
                Success =true,
                ViewStudentDto = new ViewStudentDto
                {
                    Id = student.Id,
                    Name = student.Name,
                    Rut = student.Rut,
                    Career = student.Career,
                    IsActive = student.IsActive
                }
            };
        }
    
        catch(Exception ex)
        {
           return new ResponseViewStudent
            {
                Message = "Error en la consulta " + ex.Message,
                Success =false
            }; 
        }
        throw new NotImplementedException();
    }

    public async Task<ResponseViewListStudentDto> CreateStudentByExcel(IFormFile file)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            if(file == null || file.Length == 0)
            {
                return new ResponseViewListStudentDto
                {
                    Message = "No se enviaron datos",
                    Success =false
                }; 
            }
            var extension = Path.GetExtension(file.FileName).ToLower();
            if(extension!=".xlsx" && extension != ".xls")
            {
                 return new ResponseViewListStudentDto
                {
                    Message = "El archivo debe de ser (.xlsx o .xls)",
                    Success =false
                };   
            }
            var studentCreated = 0;
            var studentList = new List<ViewStudentDto>();
            var errors = new List<string>();

            #pragma warning disable CS0618 // Suprimir warning de obsoleto
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            #pragma warning restore CS0618
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using var package =new ExcelPackage(stream);

                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension?.Rows ?? 0;
                if(rowCount <= 1)
                {
                    return new ResponseViewListStudentDto
                    {
                        Message = "El archivo está vacío o solo tiene encabezados",
                        Success = false
                    };
                }
                for (int row = 2; row<= rowCount; row++)
                {
                    try
                    {
                        var name = worksheet.Cells[row,1].Value?.ToString()?.Trim();
                        var career = worksheet.Cells[row,2].Value?.ToString()?.Trim();
                        var rut = worksheet.Cells[row,3].Value?.ToString()?.Trim();    
                        
                        if(string.IsNullOrEmpty(name) ||string.IsNullOrEmpty(career)|| string.IsNullOrEmpty(rut))
                        {
                            errors.Add($"Fila {row}: Datos incompletos");
                            continue;
                        }

                        var existingStudent = await _context.Students
                            .FirstOrDefaultAsync(s => s.Rut == rut);
                        
                        if(existingStudent != null)
                        {
                            errors.Add($"Fila {row}: El RUT {rut} ya existe");
                            continue;
                        }
                        var student = new StudentModel
                            {
                                Name = name,
                                Career = career,
                                Rut = rut,
                                IsActive = true
                            };

                        await _context.Students.AddAsync(student);
                        await _context.SaveChangesAsync();

                        var card = new CardModel
                            {
                                Idpublic =  Guid.NewGuid().ToString(),
                                Uses = 0,
                                StudentId=student.Id,
                                ValidDate=DateTime.UtcNow,
                                Student =student
                            };
                        await  _context.Cards.AddAsync(card);
                        await _context.SaveChangesAsync();
                        
                        var studentDtoCreated = new ViewStudentDto
                        {
                            Id = student.Id,
                            Name = student.Name,
                            Rut = student.Rut,
                            Career = student.Career,
                            IsActive = student.IsActive
                        };
                        studentList.Add(studentDtoCreated);
                        studentCreated++;
                    }
                    catch(Exception ex)
                    {
                        errors.Add($"Fila {row}: {ex.Message}");
                    }
                }
                
            } 
            await transaction.CommitAsync();
            
            return new ResponseViewListStudentDto
            {
                Message = $"Se crearon {studentCreated} estudiantes exitosamente",
                Success = true,
                ViewStudentDto = studentList,
                Errors = errors
            };
            
        }catch(Exception ex)
        {
            await transaction.RollbackAsync();
            return new ResponseViewListStudentDto
            {
                Message = "Error en la consulta " + ex.Message,
                Success =false
            }; 
        }
    }
}