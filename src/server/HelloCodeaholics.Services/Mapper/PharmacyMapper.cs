using HelloCodeaholics.Common.Models;
using HelloCodeaholics.Data.Entities;

namespace HelloCodeaholics.Services.Mapper;

public static class PharmacyMapper
{
    public static PharmacyViewModel Map(this Pharmacy pharmacy)
    {
        return new PharmacyViewModel()
        {
            Id = pharmacy.Id,
            Name = pharmacy.Name,
            Address = pharmacy.Address,
            City = pharmacy.City,
            State = pharmacy.State,
            Zip = pharmacy.Zip,
            FilledPrescriptionsCount = pharmacy.FilledPrescriptionsCount
        };
    }

    public static List<PharmacyViewModel> Map(this List<Pharmacy> pharmacyList)
    {
        var modelList = new List<PharmacyViewModel>();
        foreach(Pharmacy pharmacy in pharmacyList)
        {
            modelList.Add(pharmacy.Map());
        }
        return modelList;

    }

    public static void Update(this Pharmacy pharmacy, PharmacyViewModel model)
    {
        pharmacy.Name = model.Name;
        pharmacy.Address = model.Address;
        pharmacy.City = model.City;
        pharmacy.State = model.State;
        pharmacy.Zip = model.Zip;
        pharmacy.FilledPrescriptionsCount = model.FilledPrescriptionsCount;
        pharmacy.UpdatedDate = DateTime.Now;
        pharmacy.UpdatedBy = "Portal";
    }
}
