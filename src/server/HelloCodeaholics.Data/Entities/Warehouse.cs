using HelloCodeaholics.Data.Entities;

namespace HelloCodeaholics.Common.Domain.Entities;

public class Warehouse : Location
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}
