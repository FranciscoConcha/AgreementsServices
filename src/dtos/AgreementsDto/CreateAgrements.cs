namespace src.dtos.AgrementsDto;

public class CreateAgreements
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ForWhom { get; set; } = string.Empty;
}

public class DataCreateAgreements
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public string ForWhom { get; set; } = string.Empty;

}
public class ResponseCreateAgreements
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public DataCreateAgreements? Data { get; set; }
}