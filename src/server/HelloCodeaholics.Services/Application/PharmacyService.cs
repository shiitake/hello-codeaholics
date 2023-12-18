using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloCodeaholics.Core.Domain.Entities;
using HelloCodeaholics.Core.Interfaces;
using HelloCodeaholics.Infrastructure;

namespace HelloCodeaholics.Services.Application
{
    public class PharmacyService : IPharmacyService
    {
        private readonly HelloCodeDbContext _context;

        public PharmacyService(HelloCodeDbContext context)
        {
            _context = context;
        }

        public Task<Pharmacy> GetPharmacyById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Pharmacy>> GetPharmacyList()
        {
            throw new NotImplementedException();
        }

        public Task<Pharmacy> UpdatePharmacy(Pharmacy pharmacy)
        {
            throw new NotImplementedException();
        }
    }
}
