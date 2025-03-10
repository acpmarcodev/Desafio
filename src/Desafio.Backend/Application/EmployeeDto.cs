using Desafio.Backend.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Backend.Application
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Email { get; set; }
        public required string Document { get; set; }
        public required string[] Phones { get; set; }
        public int? ManagerId { get; set; }
        public required string Password { get; set; }
        public required DateTime Birthday { get; set; }
        public required RoleId RoleId { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is EmployeeDto other)
            {
                return Id == other.Id &&
                       FirstName == other.FirstName &&
                       LastName == other.LastName &&
                       Email == other.Email &&
                       Document == other.Document &&
                       ManagerId == other.ManagerId &&
                       Password == other.Password &&
                       Birthday == other.Birthday &&
                       RoleId == other.RoleId &&
                       Phones.SequenceEqual(other.Phones);
            }
            return false;
        }

        public override int GetHashCode()
        {
            int hash = HashCode.Combine(Id, FirstName, LastName, Email, Document, ManagerId, Password, Birthday);
            return HashCode.Combine(hash, RoleId);
        }
    }
}
