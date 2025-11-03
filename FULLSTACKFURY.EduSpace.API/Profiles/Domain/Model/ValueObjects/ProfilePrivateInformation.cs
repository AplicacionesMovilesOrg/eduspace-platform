namespace FULLSTACKFURY.EduSpace.API.Profiles.Domain.Model.ValueObjects;

public record ProfilePrivateInformation
{
    public ProfilePrivateInformation() : this(string.Empty, string.Empty
        , string.Empty, string.Empty)
    {
    }

    public ProfilePrivateInformation(string email, string dni
        , string address, string phone)
    {
        Email = email;
        Dni = dni;
        Address = address;
        Phone = phone;
    }

    public string Email { get; set; }
    public string Dni { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string ObtainEmail => $"{Email}";
    public string ObtainDni => $"{Dni}";
}