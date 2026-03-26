using src.model;

namespace src.dtos.AgreementsDto;

public class UpdateAgreements
{
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string ForWhom { get; set; } = string.Empty;
}
public class ResponseUpdateAgreements
{
    public bool Success { get; set; }
    public string Message { get; set; } = string.Empty;
    public AgreementsModel? Data { get; set; }
}