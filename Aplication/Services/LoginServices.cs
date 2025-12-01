using Application.DTO;
using Application.Interfaces;
using Application.Mapping;
using Domain.Base;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class LoginServices : ILoginUser
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<LoginServices> _logger;
        private readonly LoginMapping _loginmapping;

        public LoginServices(IUserRepository userRepository, ILogger<LoginServices> logger, LoginMapping loginmapping)
        {
            this._userRepository = userRepository;
            this._logger = logger;
            this._loginmapping = loginmapping;
        }

        async Task<OperationResult<LoginDTO>> ILoginUser.Login(LoginDTO loginDTO)
        {
            try
            {
                _logger.LogInformation("Intentando iniciar sesion");

                var resultUser = await _userRepository.GetByCorreo(loginDTO.Correo);

                if (loginDTO ==  null)
                {
                    _logger.LogWarning("Si los campos estan vacios no se puede iniciar sesion");

                    return OperationResult<LoginDTO>.Failure("La informacion de usuario recibida esta vacia");
                }

                if (resultUser == null)
                {
                    _logger.LogWarning("Usuario {Correo} no encontrado", loginDTO.Correo);

                    return OperationResult<LoginDTO>.Failure("Usuario no encontrado");
                }

                if (resultUser.Password != loginDTO.Password)
                {
                    _logger.LogWarning($"La contraseña ingresada es incorrecta");

                    return OperationResult<LoginDTO>.Failure("Contraseña incorrecta");
                }

                var usuario = _loginmapping.MapDTO(resultUser);

                return OperationResult<LoginDTO>.Success("Sesion iniciada correctamente", usuario);
            }

            catch (Exception ex) 
            {
                _logger.LogError(ex, "Ocurrio un error al intentar iniciar sesion");

                return OperationResult<LoginDTO>.Failure("Error inesperado al iniciar sesion");
            }
        }
    }
}
