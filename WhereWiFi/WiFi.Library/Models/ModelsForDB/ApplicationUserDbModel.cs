using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace WiFi.Library.Models.ModelsForDB
{
    public class ApplicationUserDbModel
    {
        public int Id { get; set; }
        public Role UserRole { get; set; }
        public string Login { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Email { get; set; }
    }
}