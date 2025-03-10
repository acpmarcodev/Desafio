using AutoMapper;
using Desafio.Backend.Application;
using Desafio.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

namespace Desafio.Controllers
{
    [Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly JwtService _jwtService; 

        public EmployeeController(IMapper _mapper, IConfiguration config, IEmployeeService service, JwtService jwtService)
        {
            this._service = service;
            this._mapper = _mapper;
            this._config = config;
            this._jwtService = jwtService; 
        }

        /// <summary>
        /// API para atenticar e receber o JWTToken
        /// </summary>
        /// <param name="userLogin">Numero do documento e senha (usuario padrão admindoc / doc)</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] UserLogin userLogin)
        {
            var login = await _service.LoginAsync(userLogin.Document, userLogin.Password);

            if (login)
            {
                var employee = await _service.GetByDocumentAsync(userLogin.Document);

                if (employee == null)
                    return Unauthorized();

                var token = this._jwtService.GenerateToken(employee.Id, employee.FirstName, (int)employee.RoleId);

                return Ok(new { token });
            }
            return Unauthorized();
        }

        /// <summary>
        /// Lista os funcionário
        /// </summary>
        /// <returns></returns>

        [HttpGet("list")]
        public async Task<IEnumerable<EmployeeDto>> List()
        {
            return await _service.ListAsync();
        }

        /// <summary>
        /// Obtém um funcionário pelo ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var employee = await _service.GetByIdAsync(id);

            if (employee == null)
                return NotFound();

            return new OkObjectResult(employee);
        }

       
        /// <summary>
        /// Adiciona um funcionário 
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] EmployeeDto employee)
        {
            if (employee.Id > 0) return BadRequest();

            var operatorIdAsString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if(operatorIdAsString == null)
                return Forbid();

            var operatorId = Int32.Parse(operatorIdAsString);

            var result = await _service.AddOrUpdateAsync(operatorId, employee);

            if (result != Backend.Domain.ValidationResult.Ok)
                return BadRequest(result.ToString());

            return CreatedAtAction(nameof(Get), new { id = employee.Id }, employee);
        }


      /// <summary>
      ///  Atualiza um funcionario
      /// </summary>
      /// <param name="id"></param>
      /// <param name="employee"></param>
      /// <returns></returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(
            int id, 
            [FromBody] EmployeeDto employee)
        {
            if (employee.Id <= 0) return BadRequest();

            var operatorIdAsString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (operatorIdAsString == null)
                return Forbid();

            var operatorId = Int32.Parse(operatorIdAsString);

            var result = await _service.AddOrUpdateAsync(operatorId, employee);

            if (result != Backend.Domain.ValidationResult.Ok)
                return BadRequest(result.ToString());

            return NoContent();
        }

        /// <summary>
        /// Altera a senha de um funcionario
        /// </summary>
        /// <param name="id"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>

        [HttpPut("{id}/change-password")]
        public async Task<IActionResult> ChangePassword(
            int id,
            [FromBody]string newPassword)
        {
            await _service.ChangePasswordAsync(id, newPassword);

            return NoContent();
        }


        /// <summary>
        /// Remove um funcionario
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}

