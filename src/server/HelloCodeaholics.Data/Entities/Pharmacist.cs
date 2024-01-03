namespace HelloCodeaholics.Data.Entities;

public class Pharmacist
{
    public int Id { get; set; }
    public int PharmacyId { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public int Age { get; set; }
    public string PrimaryDrugSold { get; set; } = null!;
    public DateTime DateOfHire { get; set; }
    public virtual Pharmacy Pharmacy { get; set; } = null!;

}
