namespace src.dtos;

public class ViewCardDto
{
    public string IdCardPublic { get; set; } = "";
    
    public string NameStudent { get; set; } = "";
    
    public DateTime PeriodStundet { get; set; }
    
    public string Career { get; set; } = "";
    
}

public class ViewCardListDto
{
    public int Id {get;set;}
    public string IdCardPublic { get; set; } = "";
    
    public string NameStudent { get; set; } = "";
    
    public DateTime PeriodStundet { get; set; }
    
    public string Career { get; set; } = "";
    
    public int Uses {get;set;}
}

/// <summary>
/// Clase para Visualizar tarjeta
/// </summary>
public class ResponseViewCardDto
{
    public string Meessage { get; set; } = "";
    public bool Success {get;set;}

    public ViewCardDto? ViewCardDto { get; set; }
    
}
/// <summary>
/// Clase para tarjeta unica
/// </summary>
public class ResponseViewCardUnicDto
{
    public string Message {get;set;} = "";
    public bool Success {get;set;}

    public ViewCardListDto? ViewCardList {get;set;} 
}
/// <summary>
/// Clase para listas de Tarjetas
/// </summary>
public class ResponseViewCardListDto
{
    public string Message {get;set;} = "";
    public bool Success {get;set;}

    public List<ViewCardListDto>? ViewCardList {get;set;} = []; 
}