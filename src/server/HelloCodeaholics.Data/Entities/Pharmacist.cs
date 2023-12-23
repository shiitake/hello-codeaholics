using HelloCodeaholics.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloCodeaholics.Common.Domain.Entities
{
    public class Pharmacist
    {
        public int Id { get; set; }
        public int PharmacyId { get; set; }
        public string Name { get; set; } = null!;
        public string PrimaryDrug { get; set; } = null!;
        public int Age { get; set; }
        public DateTime HireDate { get; set; }
        public virtual Pharmacy Pharmacy { get; set; } = null!;

    }
}
