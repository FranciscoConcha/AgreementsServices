using src.dtos.AgreementsDto;
using src.dtos.AgrementsDto;

namespace src.services.interfaces;

public interface IAgreementsServices
{
    Task<ResponseGetAgreements> GetAllAgreements();
    Task<ResponseGetAgreements> GetAgreementById(int id);
    Task<ResponseCreateAgreements> CreateAgreement(CreateAgreements agreement);
    Task<ResponseUpdateAgreements> UpdateAgreement(int id, UpdateAgreements agreement);
    Task<ResponseUpdateStateAgreements> UpdateStateAgreement(int id);
    Task<ResponseDeleteAgreements> DeleteAgreement(int id);
}