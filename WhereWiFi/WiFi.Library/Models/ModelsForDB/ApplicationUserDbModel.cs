using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WiFi.Library.Models.ModelsForDB
{
    public class ApplicationUserDbModel
    {
        public int Id { get; set; }
        public Role UserRole { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}