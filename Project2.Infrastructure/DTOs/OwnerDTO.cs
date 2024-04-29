using System.Runtime.CompilerServices;

namespace Project2.Infrastructure.DTOs
{
    public class OwnerDTO(int id, string firstName, string lastName) : BaseDTO(id)
    {
        public string FirstName { get; set; } = firstName;
        public string LastName { get; set; } = lastName;
    }
}
