namespace Client.Application.Features.Clients.Queries.GetClientsList;

public class ClientsVm
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Gender { get; set; }
    public int Age { get; set; }
    public string IdentificationNumber { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public bool Status { get; set; }
}