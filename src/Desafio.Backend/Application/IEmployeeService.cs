using Desafio.Backend.Domain;

namespace Desafio.Backend.Application
{
    public interface IEmployeeService
    {
        Task<ValidationResult> AddOrUpdateAsync(int operatorId, EmployeeDto employee);
        Task ChangePasswordAsync(int id, string newPassword);
        Task DeleteAsync(int id);
        Task<EmployeeDto?> GetByDocumentAsync(string document);
        Task<EmployeeDto?> GetByIdAsync(int id);
        Task<IEnumerable<EmployeeDto>> ListAsync();
        Task<bool> LoginAsync(string document, string password);
        Task<ValidationResult> ValidateAsync(int operatorId, EmployeeDto employee);
    }
}