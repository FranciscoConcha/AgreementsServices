namespace src.dtos.AgreementsDto;

public class GetAgreementsDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public string ForWhom { get; set; } = string.Empty;

}
public class ResponseGetAgreements
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public List<GetAgreementsDto>? Data { get; set; }
}