namespace src.dtos;

public class UpdateUserDto
{
    public string Name
    {
        get; set;
    } = string.Empty;

    public string Email
    {
        get; set;
    } = string.Empty;

    public string Password
    {
        get; set;
    } = string.Empty;

    public string Charge
    {
        get; set;
    } = string.Empty;

    public bool? IsActive
    {
        get; set;
    } 
}