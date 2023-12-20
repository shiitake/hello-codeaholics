using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloCodeaholics.Core.Domain.Entities;
using HelloCodeaholics.Core.Interfaces;
using HelloCodeaholics.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HelloCodeaholics.Services.Application;

public class PharmacyService : IPharmacyService
{
    private readonly HelloCodeDbContext _context;

    public PharmacyService(HelloCodeDbContext context)
    {
        _context = context;
    }

    public async Task<Pharmacy?> GetPharmacyById(int id)
    {
        return await _context.Pharmacies.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
    }

    public async Task<List<Pharmacy>> GetPharmacyList(int pageNumber = 1, int pageSize = 10)
    {
        return await _context.Pharmacies
            .AsNoTracking()
            .Skip((pageNumber -1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<bool> PharmacyExists(int id)
    {
        return await _context.Pharmacies.AnyAsync(x => x.Id == id);
    }
    public async Task<Pharmacy?> UpdatePharmacy(Pharmacy pharmacy)
    {
        if (!await PharmacyExists(pharmacy.Id))
        {
            return null!;
        }
        pharmacy.UpdatedDate = DateTime.Now;
        _context.Update(pharmacy);
        _context.Entry(pharmacy).Property(x => x.CreatedDate).IsModified = false;
        _context.Entry(pharmacy).Property(x => x.CreatedBy).IsModified = false;
        await _context.SaveChangesAsync();
        return pharmacy;
    }
}
