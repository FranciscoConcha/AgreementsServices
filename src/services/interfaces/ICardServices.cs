using src.dtos;

namespace src.services.interfaces;

public interface ICardServices{
    
    /// <summary>
    /// eSTO ES PARA MOSTRAR AL LA GENTE
    /// </summary>
    /// <param name="rutStudent"></param>
    /// <returns></returns>
    Task<ResponseViewCardDto> GetCardForStudentView(string rutStudent);

    Task<ResponseVerificateRut> GetVerificateRut(string Rut);

    /// <summary>
    /// tAMBIEN ES DEL CRUD
    /// </summary>
    /// <returns></returns>
    Task<ResponseViewCardListDto> GetAllCard();

    /// <summary>
    /// ESTO ES PARA EL CRUD HACE TU LA DOCUMENTACIÓN BENJA
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    Task<ResponseViewCardUnicDto> GetByCard(int Id);

    Task<ResponseUseDto> UpdateUseCard(UpdateUseDto updateUseDto);

    Task<ResponseCardByIdPublic> GetByIdPublic(InputDataCardByIdPublic input);

}