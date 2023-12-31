﻿namespace HelloCodeaholics.Data.Entities;

public class Pharmacy : Location
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int FilledPrescriptionsCount { get; set; }
    public DateTime CreatedDate {  get; set; }
    public DateTime? UpdatedDate { get; set;}
    public string CreatedBy { get; set; } = string.Empty;
    public string? UpdatedBy { get; set;}
    public virtual List<Pharmacist> Pharmacists { get; set; } = new List<Pharmacist> { };
    public virtual List<Delivery> Deliveries { get; set; } = new List<Delivery> { };

}
