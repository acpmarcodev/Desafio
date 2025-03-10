using AutoMapper;
using Desafio.Backend.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Backend.Infra.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly DesafioDbContext _context;

        public EmployeeRepository(DesafioDbContext context)
        {
            _context = context;
        }

        public async Task AddOrUpdateAsync(EmployeeEntity employeeEntity)
        {
            if (employeeEntity.birthday.Kind == DateTimeKind.Unspecified)
                employeeEntity.birthday = DateTime.SpecifyKind(employeeEntity.birthday, DateTimeKind.Unspecified);

            if (!employeeEntity.password.StartsWith("PBKDF2$"))
            {
                employeeEntity.password = PasswordHasher.HashPassword(employeeEntity.password);
            }

            if (employeeEntity.id <= 0)
            {
                await _context.employees.AddAsync(employeeEntity);
            }
            else
            {
                var existingEntity = _context.employees.Local.FirstOrDefault(e => e.id == employeeEntity.id);
                
                if (existingEntity != null)
                    _context.Entry(existingEntity).State = EntityState.Detached;
                
                _context.employees.Update(employeeEntity);
            }
            await _context.SaveChangesAsync();

        }

        public async Task ChangePasswordAsync(int id, string newPassword)
        {
            if (!newPassword.StartsWith("PBKDF2$"))
            {
                newPassword = PasswordHasher.HashPassword(newPassword);
            }


            var employee = await _context.employees.FirstOrDefaultAsync(e => e.id == id);

            if (employee == null)
            {
                throw new InvalidOperationException("Employee not found.");
            }

            employee.password = newPassword;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var employee = await _context.employees.FirstOrDefaultAsync(e => e.id == id);

            if (employee == null)
            {
                throw new InvalidOperationException("Employee not found.");
            }

            _context.employees.Remove(employee);

            await _context.SaveChangesAsync();
        }

        public async Task<EmployeeEntity?> GetByDocumentAsync(string document)
        {
            var employeeEntity = await _context.employees
                     .FirstOrDefaultAsync(e => e.document == document);

            return employeeEntity;
        }

        public async Task<EmployeeEntity?> GetByIdAsync(int id)
        {
            var employeeEntity = await _context.employees
                .FirstOrDefaultAsync(e => e.id == id);

            return employeeEntity;
        }

        public async Task<IEnumerable<EmployeeEntity>> ListAsync()
        {
            var employeeEntities = await _context.employees.ToListAsync();

            return employeeEntities;
        }

        public async Task<bool> LoginAsync(string document, string password)
        {
            var employeeEntity = await _context.employees
                       .FirstOrDefaultAsync(e => e.document == document);

            if (employeeEntity == null)
            {
                return false;
            }

            return PasswordHasher.VerifyPassword(password, employeeEntity.password);
        }

       
    }
}
