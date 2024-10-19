using Client.Domain.Common;

namespace Client.Domain.Entities;

public abstract class Person : EntityBase
{
    public string Name { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public string IdentificationNumber { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }   
}