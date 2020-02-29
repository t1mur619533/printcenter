using System.Collections.Generic;
using System.ComponentModel;

namespace PrintCenter.Data.Models
{
	public class User
	{
		public int Id { get; set; }

		public string Login { get; set; }

		public string Password { get; set; }

		public string Surname { get; set; }
		
		public string Name { get; set; }

		public Role Role { get; set; }

		public List<Technology> Technologies { get; set; }
	}

	public enum Role
	{
		[Description("Неaктивен")] Disable,
		[Description("Менеджер")] Manager,
		[Description("Печатник")] Printer,
		[Description("Админ")] Admin,
		[Description("СуперАдмин")] SuperAdmin
	}
}
