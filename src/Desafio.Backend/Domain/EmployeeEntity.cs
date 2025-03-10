using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Backend.Domain
{
    public class EmployeeEntity
    {
        public int id { get; set; }
        public required string first_name { get; set; }
        public required string last_name { get; set; }
        public required string email { get; set; }
        public required string document { get; set; }
        public required string[] phones { get; set; }
        public int? manager_id { get; set; }
        public required string password { get; set; }
        public required DateTime birthday { get; set; }
        public required int role_id { get; set; }
        public EmployeeEntity? manager { get; set; }
        public ICollection<EmployeeEntity> subordinates { get; set; } = new List<EmployeeEntity>();
    }
}
