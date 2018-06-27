using Model.Models;

namespace Library.Models
{
    public class ContactOutModel
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        public ContactOutModel(Contact contact)
        {
            Email = contact.Email;
            FirstName = contact.FirstName;
            LastName = contact.LastName;
            PhoneNumber = contact.PhoneNumber;
        }
    }
}
