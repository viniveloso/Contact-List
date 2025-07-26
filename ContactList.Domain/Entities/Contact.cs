using System.ComponentModel.DataAnnotations;
namespace ContactList.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Email { get; set; } = "";
        public string Phone { get; set; } = "";
        public string Cep { get; set; } = "";
        public string Street { get; set; } = "";
        public string Neighborhood { get; set; } = "";
        public string City { get; set; } = "";
        public string State { get; set; } = "";

        public Contact() { }

        public Contact(string name, string email, string phone, string cep, string street, string city, string neighborhood, string state)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Cep = cep;
            Street = street;
            City = city;
            Neighborhood = neighborhood;
            State = state;
        }
    }
}
