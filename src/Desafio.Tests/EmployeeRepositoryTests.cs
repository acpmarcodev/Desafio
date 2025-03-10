using AutoMapper;
using Desafio.Backend.Application;
using Desafio.Backend.Domain;
using Desafio.Backend.Infra;
using Desafio.Backend.Infra.Profiles;
using Desafio.Backend.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Tests
{
    public class EmployeeRepositoryTests : IClassFixture<DatabaseFixture>
    {
        private readonly IEmployeeService _service;
        private readonly IEmployeeRepository _repository;
        private readonly IMapper _mapper;

        public EmployeeRepositoryTests(DatabaseFixture fixture)
        {
            var mapperConfiguration = new MapperConfiguration(cfg =>
            { 
                cfg.AddProfile(new EmployeeProfile());  
            });

            _mapper = mapperConfiguration.CreateMapper();
            _repository = new EmployeeRepository(fixture._dbContext); 
            _service = new EmployeeService(_mapper, _repository);
        }

        [Fact]
        public async Task ValidateDocumentEmployee_ShouldInvalid()
        {
            var employee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 12345678901",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "12345678901",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, employee);

            var employeeDuplicated = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 12345678901",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "12345678901",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, employee);

            var result = await _service.AddOrUpdateAsync(1, employeeDuplicated);

            Assert.Equal(ValidationResult.InvalidDocument, result);
        }

        [Fact]
        public async Task ValidateOperatorEmployee_ShouldValid()
        {
            var operatorEmployee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 123456789013",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "123456789013",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, operatorEmployee);

            var employee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 123456789012",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "123456789012",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, operatorEmployee);

            var result = await _service.AddOrUpdateAsync(operatorEmployee.Id, employee);

            Assert.Equal(ValidationResult.Ok, result);
        }

        [Fact]
        public async Task ValidateOperatorEmployee_ShouldInvalid()
        {
            var operatorEmployee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 1234567890134",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "1234567890134",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, operatorEmployee);

            var employee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 123456789012345",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "123456789012345",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.SuperAdmin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, operatorEmployee);

            var result = await _service.AddOrUpdateAsync(operatorEmployee.Id, employee);

            Assert.Equal(ValidationResult.InvalidRoleId, result);
        }
        [Fact]
        public async Task ListEmployee_ShouldNotEmpty()
        {
            var s = PasswordHasher.HashPassword("admin");

            var employees = await _service.ListAsync();

            Assert.True(employees.Count() > 0);
        }

        [Fact]
        public async Task AddEmployee_ShouldAddEmployee()
        {
            var employee = new EmployeeDto() {
               Id = 0,
               FirstName = "Marco 123",
               LastName = "Pisani",
               Email = "acp.marco@outlook.com",
               Document = "123",
               Phones = new string[] { "911" },
               ManagerId = null,
               Password = "pass@word1",
               RoleId = RoleId.Admin,
               Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, employee);
            var employees = await _service.ListAsync();
            Assert.Contains(employee, employees);
        }

        [Fact]
        public async Task UpdateEmployee_ShouldModifyEmployee()
        {
            var employee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 1234",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "1234",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, employee);

            var updatedEmployee = new EmployeeDto()
            {
                Id = employee.Id,
                FirstName = "Marco 1234",
                LastName = "Pisani",
                Email = "acp.marco12345@outlook.com",
                Document = "1234",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, updatedEmployee);

            var retrievedEmployee = await _service.GetByIdAsync(employee.Id);
            
            Assert.NotNull(retrievedEmployee);
            Assert.Equal("acp.marco12345@outlook.com", retrievedEmployee.Email);
        }

        [Fact]
        public async Task ChangePasswordEmployee_ShouldModifyPassword()
        {
            var employee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 123456",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "123456",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, employee);

            await _service.ChangePasswordAsync(employee.Id, "newPassword");

            var login = await _service.LoginAsync("123456", "newPassword");

            Assert.True(login);
        }

        [Fact]
        public async Task LoginEmployee_ShouldLogin()
        {
            var employee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 1234567",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "1234567",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, employee);

            var login = await _service.LoginAsync("1234567", "pass@word1");

            Assert.True(login);
        }

        [Fact]
        public async Task LoginMasterEmployee_ShouldLogin()
        {
            var login = await _service.LoginAsync("docadmin", "admin");

            Assert.True(login);
        }


        [Fact]
        public async Task GetEmployeeById_ShouldReturnCorrectEmployee()
        {
            var employee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 12345678",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "12345678",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, employee);
            var retrieved = await _service.GetByIdAsync(employee.Id);

            Assert.NotNull(retrieved);

            Assert.Equal(employee.Id, retrieved.Id);
        }

        [Fact]
        public async Task GetEmployeeByDocument_ShouldReturnCorrectEmployee()
        {
            var employee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 123456789",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "123456789",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, employee);

            var retrieved = await _service.GetByDocumentAsync(employee.Document);

            Assert.NotNull(retrieved);

            Assert.Equal(employee.Id, retrieved.Id);
        }

        [Fact]
        public async Task DeleteEmployee_ShouldRemoveEmployee()
        {
            var employee = new EmployeeDto()
            {
                Id = 0,
                FirstName = "Marco 1234567890",
                LastName = "Pisani",
                Email = "acp.marco@outlook.com",
                Document = "1234567890",
                Phones = new string[] { "911" },
                ManagerId = null,
                Password = "pass@word1",
                RoleId = RoleId.Admin,
                Birthday = new DateTime(1982, 01, 01, 0, 0, 0, 0, 0, DateTimeKind.Utc)
            };

            await _service.AddOrUpdateAsync(1, employee);
            await _service.DeleteAsync(employee.Id);

            var employees = await _service.ListAsync();

            Assert.DoesNotContain(employee, employees);
        }
    }
}
