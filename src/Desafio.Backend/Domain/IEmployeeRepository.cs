using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Backend.Domain
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> ListAsync();
        Task<EmployeeEntity?> GetByIdAsync(int id);
        Task AddOrUpdateAsync(EmployeeEntity employee);
        Task DeleteAsync(int id);
        Task<bool> LoginAsync(string document, string password);
        Task ChangePasswordAsync(int id, string newPassword);
        Task<EmployeeEntity?> GetByDocumentAsync(string document);
    }
}
