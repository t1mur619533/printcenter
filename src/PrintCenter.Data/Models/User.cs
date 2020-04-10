using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;

namespace PrintCenter.Data.Models
{
    public class User : IHasId
    {
        public int Id { get; set; }

        public string Login { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }
        
        public Role Role { get; set; }

        [JsonIgnore]
        public List<UserTechnology> UserTechnologies { get; set; }
        
        [JsonIgnore]
        public List<MaterialMovement> MaterialMovements { get; set; }

        [JsonIgnore]
        public List<Plan> Plans { get; set; }

        [JsonIgnore]
        public List<Request> Requests { get; set; }

        [JsonIgnore]
        public List<Invoice> Invoices { get; set; }

        [NotMapped, JsonIgnore]
        public List<Technology> Technologies => UserTechnologies.Where(technology => technology.UserId.Equals(Id)).Select(technology => technology.Technology).ToList();
    }

    public enum Role
    {
        [Display(Name = "Неaктивен")]
        Disable = 0,

        [Display(Name = "Менеджер")]
        Manager = 1,

        [Display(Name = "Печатник")]
        Printer = 2,

        [Display(Name = "Админ")]
        Admin = 3,

        [Display(Name = "СуперАдмин")]
        SuperAdmin = 4
    }
}