using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Prueba1_Real.src.Interfaces;

namespace Prueba1_Real.src.Controllers
{
    [Route("user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _userRepository.SelectAll();
            return Ok("Usuarios obtenidos exitosamente");
        }

        [HttpPut("{rut}")]
        public async Task<IActionResult> Put(string rut, [FromBody] DTOs.UpdateUserDto user)
        {
            var userToUpdate = await _userRepository.FindByCode(rut);
            if (userToUpdate == null)
            {
                return NotFound("Usuario no encontrado");
            }
            if (DateTime.Parse(userToUpdate.fechaNacimiento) >= DateTime.Now)
            {
                return BadRequest("La fecha de nacimiento no puede ser mayor o igual a la fecha actual.");
            }

            userToUpdate.Nombre = user.Nombre;
            userToUpdate.correo = user.correo;
            userToUpdate.genero = user.genero;
            userToUpdate.fechaNacimiento = user.fechaNacimiento;

            await _userRepository.SaveChanges();
            return Ok("Usuario actualizado exitosamente");

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DTOs.CreateUserDto user)
        {
            if (await _userRepository.ExistsByCode(user.Rut))
            {
                return Conflict("El rut ya existe");
            }
            if (DateTime.Parse(user.fechaNacimiento) >= DateTime.Now)
            {
                return BadRequest("La fecha de nacimiento no puede ser mayor o igual a la fecha actual.");
            }

            var userToInsert = new Models.User
            {
                Rut = user.Rut,
                Nombre = user.Nombre,
                correo = user.correo,
                genero = user.genero,
                fechaNacimiento = user.fechaNacimiento
            };

            await _userRepository.Insert(userToInsert);
            return Ok("Usuario creado exitosamente");
        }
        
        [HttpDelete("{rut}")]
        public async Task<IActionResult> Delete(string rut)
        {
            var userToDelete = await _userRepository.FindByCode(rut);
            if (userToDelete == null)
            {
                return NotFound("Usuario no encontrado");
            }

            await _userRepository.Delete(userToDelete);
            return Ok("Usuario eliminado exitosamente");
        }


    }
}