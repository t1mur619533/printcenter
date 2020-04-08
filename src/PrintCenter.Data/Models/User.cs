using System.Collections.Generic;
using System.ComponentModel;

namespace PrintCenter.Data.Models
{
    public class User : IHasId
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }
        
        public Role Role { get; set; }
        
        public List<UserTechnology> UserTechnologies { get; set; }

        public List<MaterialMovement> MaterialMovements { get; set; }

        public List<Plan> Plans { get; set; }

        public List<Request> Requests { get; set; }

        public List<Invoice> Invoices { get; set; }
    }

    public enum Role
    {
        [Description("Неaктивен")]
        Disable = 0,

        [Description("Менеджер")]
        Manager = 1,

        [Description("Печатник")]
        Printer = 2,

        [Description("Админ")]
        Admin = 3,

        [Description("СуперАдмин")]
        SuperAdmin = 4
    }
}