namespace Project2.Infrastructure.Model
{
    public class Owner : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }


        public Owner() { }


        public Owner(string firstName, string lastName)
        {
            Firstname = firstName;
            Lastname = lastName;
        }
    }
}
