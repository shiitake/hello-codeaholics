using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloCodeaholics.Core.Domain.Entities;

namespace HelloCodeaholics.Core.Interfaces;

public interface IPharmacyService
{
    Task<Pharmacy?> GetPharmacyById(int id);
    Task<List<Pharmacy>> GetPharmacyList(int pageNumber = 1, int pageSize = 10);
    Task<bool> PharmacyExists(int id);
    Task<Pharmacy?> UpdatePharmacy(Pharmacy pharmacy);

}
