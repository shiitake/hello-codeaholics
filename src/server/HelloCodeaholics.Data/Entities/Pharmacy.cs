using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCodeaholics.Data.Entities;

public class Pharmacy
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public int Zip { get; set; }
    public int FilledPrescriptionsCount { get; set; }
    public DateTime CreatedDate {  get; set; }
    public DateTime? UpdatedDate { get; set;}
    public string CreatedBy { get; set; } = string.Empty;
    public string? UpdatedBy { get; set;}


}
