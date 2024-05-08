using System.ComponentModel.DataAnnotations;
using Domain.Common.Helper;

namespace Domain.Common.Dto.User
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = Message.NameRequiredErrorMessage)]
        public string Name { get; set; }

        [Required(ErrorMessage = Message.EmailRequiredErrorMessage)]
        [EmailAddress(ErrorMessage = Message.EmailFormatErrorMessage)]
        public string Email { get; set; }
    }
}
