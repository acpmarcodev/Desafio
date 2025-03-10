using AutoMapper;
using Desafio.Backend.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Backend.Application
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeService(IMapper mapper, IEmployeeRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EmployeeDto>> ListAsync()
        {
            var employeeEntities = await _repository.ListAsync();

            return _mapper.Map<IEnumerable<EmployeeDto>>(employeeEntities);
        }


        public async Task<EmployeeDto?> GetByIdAsync(int id)
        {
            var employeeEntity = await _repository.GetByIdAsync(id);

            if (employeeEntity == null)
                return null;

            return _mapper.Map<EmployeeDto>(employeeEntity);
        }


        public async Task<ValidationResult> AddOrUpdateAsync(int operatorId, EmployeeDto employee)
        {
            var employeeEntity = _mapper.Map<EmployeeEntity>(employee);

            var validation = await ValidateAsync(operatorId, employee);

            if (validation == ValidationResult.Ok)
            {
                await _repository.AddOrUpdateAsync(employeeEntity);

                employee.Id = employeeEntity.id;

                return ValidationResult.Ok;
            }
            else
            {
                return validation;
            }
        }

        public async Task<ValidationResult> ValidateAsync(int operatorId, EmployeeDto employee)
        {
            var operatorEmployee = await GetByIdAsync(operatorId);

            if (operatorEmployee == null)
                return ValidationResult.InvalidOperator;

            if (operatorEmployee.RoleId == RoleId.Admin && employee.RoleId == RoleId.SuperAdmin)
                return ValidationResult.InvalidRoleId;

            if (operatorEmployee.RoleId == RoleId.Operator && employee.RoleId != RoleId.Operator)
                return ValidationResult.InvalidRoleId;

            if (employee.Birthday > DateTime.UtcNow.Date.AddYears(-18))
                return ValidationResult.InvalidBirthday;

            if (string.IsNullOrWhiteSpace(employee.FirstName))
                return ValidationResult.MissingName;

            if (string.IsNullOrWhiteSpace(employee.LastName))
                return ValidationResult.MissingLastName;

            if (string.IsNullOrWhiteSpace(employee.Email))
                return ValidationResult.MissingEmail;

            try
            {
                var addr = new MailAddress(employee.Email);
            }
            catch
            {
                return ValidationResult.InvalidEmail;
            }

            var retrivedEmployee = await GetByDocumentAsync(employee.Document);

            if (retrivedEmployee != null && retrivedEmployee.Id != employee.Id)
                return ValidationResult.InvalidDocument;

            return ValidationResult.Ok;
        }

        public async Task DeleteAsync(int id) =>
            await _repository.DeleteAsync(id);

        public async Task ChangePasswordAsync(
            int id,
            string newPassword) =>
            await _repository.ChangePasswordAsync(id, newPassword);

        public async Task<EmployeeDto?> GetByDocumentAsync(string document)
        {
            var employeeEntity = await _repository.GetByDocumentAsync(document);

            if (employeeEntity == null)
                return null;

            return _mapper.Map<EmployeeDto>(employeeEntity);
        }

        public async Task<bool> LoginAsync(string document, string password) =>
           await _repository.LoginAsync(document, password);

    }
}
