using src.model;

namespace src.model;

public class UserModel
{
    public int Id
    {
        get; set;
    }

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

    public bool IsActive
    {
        get; set;
    }

    public int RolId
    {
        get; set;
    }

    public RolModel Rol
    {
        get; set;
    } = null!;
    
}