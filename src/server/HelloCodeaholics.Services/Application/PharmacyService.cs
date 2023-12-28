using HelloCodeaholics.Common.Models;
using HelloCodeaholics.Data;
using HelloCodeaholics.Data.Entities;
using HelloCodeaholics.Services.Interfaces;
using HelloCodeaholics.Services.Mapper;

namespace HelloCodeaholics.Services.Application;

public class PharmacyService : IPharmacyService
{
    private readonly IGenericRepository<Pharmacy> _pharmacyRepository;

    public PharmacyService(IGenericRepository<Pharmacy> pharmacyRepository)
    {
        _pharmacyRepository = pharmacyRepository;
    }

    public async Task<PharmacyViewModel?> GetPharmacyById(int id)
    {
        var pharmacy = await _pharmacyRepository.GetByIdAsync(id);
        return pharmacy is null ? null : pharmacy.Map();
    }

    public async Task<List<PharmacyViewModel>> GetPharmacyList(int pageNumber = 1, int pageSize = 10)
    {
        var pharmacyList = await _pharmacyRepository.GetAsync(null, pageNumber, pageSize, true);
        return pharmacyList.Map();
    }

    public async Task<bool> PharmacyExists(int id)
    {
        return await _pharmacyRepository.AnyAsync(x => x.Id == id);
    }
    public async Task<PharmacyViewModel?> UpdatePharmacy(PharmacyViewModel model)
    {
        var pharmacy = await _pharmacyRepository.GetByIdAsync(model.Id);
        if (pharmacy is null)
        {
            return null!;
        }
        pharmacy.Update(model);
        
        var updatedPharmacy = await _pharmacyRepository.UpdateAsync(pharmacy, x=> x.CreatedDate, x=> x.CreatedBy);
        return updatedPharmacy is null ? null : updatedPharmacy.Map();
    }


}
