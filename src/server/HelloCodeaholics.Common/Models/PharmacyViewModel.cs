namespace HelloCodeaholics.Common.Models;

public class PharmacyViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public int Zip { get; set; }
    public int FilledPrescriptionsCount { get; set; }
}
