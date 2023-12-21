using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloCodeaholics.Common.Interfaces;
using HelloCodeaholics.Core.Domain.Entities;
using HelloCodeaholics.Core.Interfaces;
using HelloCodeaholics.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HelloCodeaholics.Services.Application;

public class PharmacyService : IPharmacyService
{
    private readonly IGenericRepository<Pharmacy> _pharmacyRepository;

    public PharmacyService(IGenericRepository<Pharmacy> pharmacyRepository)
    {
        _pharmacyRepository = pharmacyRepository;
    }

    public async Task<Pharmacy?> GetPharmacyById(int id)
    {
        return await _pharmacyRepository.GetByIdAsync(id);
    }

    public async Task<List<Pharmacy>> GetPharmacyList(int pageNumber = 1, int pageSize = 10)
    {
        return await _pharmacyRepository.GetAsync(null, pageNumber, pageSize, true);
    }

    public async Task<bool> PharmacyExists(int id)
    {
        return await _pharmacyRepository.AnyAsync(x => x.Id == id);
    }
    public async Task<Pharmacy?> UpdatePharmacy(Pharmacy pharmacy)
    {
        if (!await PharmacyExists(pharmacy.Id))
        {
            return null!;
        }
        pharmacy.UpdatedDate = DateTime.Now;
        var updatedPharmacy = await _pharmacyRepository.UpdateAsync(pharmacy, x=> x.CreatedDate, x=> x.CreatedBy);
        return updatedPharmacy;
    }
}
