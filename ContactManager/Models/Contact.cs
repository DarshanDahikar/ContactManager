using System.ComponentModel.DataAnnotations;

namespace ContactManager.Models
{
    public class Contact
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required")]
        //[RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}
