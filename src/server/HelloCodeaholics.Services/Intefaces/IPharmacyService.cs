using HelloCodeaholics.Data.Entities;
using HelloCodeaholics.Common.Models;

namespace HelloCodeaholics.Services.Interfaces;

public interface IPharmacyService
{
    Task<PharmacyViewModel?> GetPharmacyById(int id);
    Task<List<PharmacyViewModel>> GetPharmacyList(int pageNumber = 1, int pageSize = 10);
    Task<bool> PharmacyExists(int id);
    Task<PharmacyViewModel?> UpdatePharmacy(PharmacyViewModel model);

}
