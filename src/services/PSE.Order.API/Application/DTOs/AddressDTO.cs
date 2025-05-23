namespace PSE.Order.API.Application.DTOs;

public class AddressDTO
{
    public string Street { get; set; }

    public string Number { get; set; }

    public string Complement { get; set; }

    public string ZipCode { get; set; }

    public string Neighborhood { get; set; }

    public string City { get; set; }

    public string State { get; set; }
}