using System.ComponentModel.DataAnnotations;

namespace APIBackend.Controllers.Dtos;

public class UserDto
{
    // [JsonPropertyName("id")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; } = "";
    public string Username { get; set; } = "";
    public string Email { get; set; } = "";
    public AddressDto Address { get; set; } = new AddressDto();
    public string Phone { get; set; } = "";
    public string Website { get; set; } = "";
    public CompanyDto Company { get; set; } = new CompanyDto();
}

public class AddressDto
{
    public string Street { get; set; } = "";
    public string Suite { get; set; } = "";
    public string City { get; set; } = "";
    public string Zipcode { get; set; } = "";
    public GeoDto Geo { get; set; } = new GeoDto();
}

public class GeoDto
{
    public string Lat { get; set; } = "";
    public string Lng { get; set; } = "";
}

public class CompanyDto
{
    public string Name { get; set; } = "";
    public string CatchPhrase { get; set; } = "";
    public string Bs { get; set; } = "";
}