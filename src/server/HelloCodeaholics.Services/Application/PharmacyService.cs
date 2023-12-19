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
        return await _context.Pharmacies.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<List<Pharmacy>> GetPharmacyList()
    {
        return await _context.Pharmacies.ToListAsync();
    }

    public async Task<bool> PharmacyExists(int id)
    {
        return await _context.Pharmacies.AnyAsync(x => x.Id == id);
    }
    public async Task<Pharmacy> UpdatePharmacy(Pharmacy pharmacy)
    {
        pharmacy.UpdatedDate = DateTime.Now;
        _context.Update(pharmacy);
        await _context.SaveChangesAsync();
        return pharmacy;
    }
}
