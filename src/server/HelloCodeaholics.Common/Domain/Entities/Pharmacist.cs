namespace HelloCodeaholics.Common.Domain.Entities
{
    public class Pharmacist
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public string PrimaryDrugSold { get; set; } = string.Empty;
        public DateTime DateOfHire { get; set; }
        public virtual Pharmacy Pharmacy { get; set; } = null!;
    }
}
