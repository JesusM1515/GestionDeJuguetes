using Application.DTO;
using Application.Interfaces;
using Application.Mapping;
using Domain.Base;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class JuguetesServices : IJuguetes
    {
        //inyeccion
        private readonly IJuguetesRepository _juguetesRepository;
        private readonly ILogger<JuguetesServices> _logger;
        private readonly JuguetesMapping _juguetesMapping;
        public JuguetesServices(IJuguetesRepository juguetesRepository, ILogger<JuguetesServices> logger, JuguetesMapping _juguetesMapping)
        {
            this._juguetesRepository = juguetesRepository;
            this._logger = logger;
            this._juguetesMapping = _juguetesMapping;
        }

        //Añadir
        public async Task<OperationResult<JuguetesDTO>> AddJuguete(JuguetesDTO juguetesDTO)
        {
            try
            {
                _logger.LogInformation("Intentando añadir juguete");

                if (juguetesDTO == null)
                {
                    _logger.LogWarning("Se intento crear un juguete con los campos vacios");
                    return OperationResult<JuguetesDTO>.Failure("No se puede crear un juguete con los campos vacios");
                }

                if (juguetesDTO.Precio <=0)
                {
                    _logger.LogWarning("Precio invalido: {Precio}", juguetesDTO.Precio);
                    return OperationResult<JuguetesDTO>.Failure("El precio debe de ser mayor a 0");
                }

                if (juguetesDTO.Edad <=0)
                {
                    _logger.LogWarning("Edad invalida: {Edad}", juguetesDTO.Edad);
                    return OperationResult<JuguetesDTO>.Failure("La edad debe de ser mayor a 0");
                }

                var JugueteMap = _juguetesMapping.MapDTOAdd(juguetesDTO);

                await _juguetesRepository.CreateAsync(JugueteMap);

                _logger.LogInformation("Juguete con ID {ID_Juguete} añadido correctamente", juguetesDTO.ID_Juguete);

                var JugueteResult = _juguetesMapping.MapEntity(JugueteMap);


                return OperationResult<JuguetesDTO>.Success("Juguete añadido correctamente", JugueteResult);
            }
            
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al intentar añadir juguete");
                return OperationResult<JuguetesDTO>.Failure("Error inesperado al intentar añadir juguete");
            }

        }

        //Actualiar
        public async Task<OperationResult<JuguetesUpdateDTO>> Updateasync(string id, JuguetesUpdateDTO juguetesUpdateDTO)
        {
            try
            {
                _logger.LogInformation("Intentando actualizar juguete con ID {Id}", id);

                if (juguetesUpdateDTO == null)
                {
                    _logger.LogWarning("Se intento actualizar con datos vacios");
                    return OperationResult<JuguetesUpdateDTO>.Failure("No se puede actualizar con datos vacios");
                }
                
                if (juguetesUpdateDTO.Precio <= 0)
                {
                    _logger.LogWarning("Precio invalido: {Precio}", juguetesUpdateDTO.Precio);
                    return OperationResult<JuguetesUpdateDTO>.Failure("El precio debe de ser mayor a 0");
                }

                if (juguetesUpdateDTO.Edad <= 0)
                {
                    _logger.LogWarning("Edad invalida: {Edad}", juguetesUpdateDTO.Edad);
                    return OperationResult<JuguetesUpdateDTO>.Failure("La edad debe de ser mayor a 0");
                }

                var getJuguete = await _juguetesRepository.GetByIdStringAsync(id);

                if (getJuguete == null)
                {
                    _logger.LogWarning("No se encontro el juguete con ID {Id}", id);
                    return OperationResult<JuguetesUpdateDTO>.Failure("El juguete no existe en la base de datos");
                }

                _juguetesMapping.MapDTOUpdate(getJuguete, juguetesUpdateDTO);

                await _juguetesRepository.UpdateAsync(getJuguete);

                _logger.LogInformation("Juguete con ID {Id} actualizado correctamente", id);
                return OperationResult<JuguetesUpdateDTO>.Success("Juguete actualizado correctamente", juguetesUpdateDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al actualizar juguete con ID {Id}", id);
                return OperationResult<JuguetesUpdateDTO>.Failure("Error inesperado al actualizar juguete");
            }
        }

        //Traer todos
        public async Task<OperationResult<IEnumerable<JuguetesDTO>>> GetAllasync()
        {
            try
            {
                _logger.LogInformation("Intentando traer lista de juguetes");

                var juguetes = await _juguetesRepository.GetAllAsync();

                if (juguetes == null || !juguetes.Any())
                {
                    _logger.LogWarning("No se encontraron juguetes en la base de datos");
                    return OperationResult<IEnumerable<JuguetesDTO>>.Failure("No se encontraron juguetes");
                }

                //Mapea todos los juguetes sin utilizar un foreach y los pasa a una lista
                var juguetesMap = juguetes.Select(e => _juguetesMapping.MapEntity(e)).ToList();

                _logger.LogInformation("Se obtuvieron los juguetes con exito");

                return OperationResult<IEnumerable<JuguetesDTO>>.Success("Lista de juguetes obtenida correctamente", juguetesMap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al traer lista de juguetes");
                return OperationResult<IEnumerable<JuguetesDTO>>.Failure("Error inesperado al traer lista de juguetes");
            }
        }

        //TraerById
        public async Task<OperationResult<JuguetesDTO>> GetbyIdasync(string id)
        {
                try
                {
                    _logger.LogInformation("Intentando traer juguete en especifico con ID {Id}", id);

                    var juguete = await _juguetesRepository.GetByIdStringAsync(id);

                    if (juguete == null)
                    {
                        _logger.LogWarning("No se encontro el juguete con ID {Id}", id);
                        return OperationResult<JuguetesDTO>.Failure("No se encontraron juguetes");
                    }

                    var jugueteMap = _juguetesMapping.MapEntity(juguete);

                    _logger.LogInformation("Juguete encontrado correctamente con ID {Id}", id);

                    return OperationResult<JuguetesDTO>.Success("Juguete obtenido exitosamente", jugueteMap);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error inesperado al traer juguete con ID {Id}", id);
                    return OperationResult<JuguetesDTO>.Failure("Error inesperado al traer juguete");
                }
        }
        
        //TraerByName
        public async Task<OperationResult<IEnumerable<JuguetesDTO>>> GetbyNameasync(string nombre)
        {
            try
            {
                _logger.LogInformation("Intentando traer lista de juguetes por nombre: {Nombre}", nombre);

                

                if (!Regex.IsMatch(nombre, @"^[a-zA-Z0-9\s]+$"))
                {
                    _logger.LogWarning("Se intento buscar juguetes con caracteres invalidos en el nombre: {Nombre}", nombre);
                    return OperationResult<IEnumerable<JuguetesDTO>>.Failure("El nombre contiene caracteres no validos");
                }

                var juguetesGet = await _juguetesRepository.GetByNameAsync(nombre);

                if (juguetesGet == null || !juguetesGet.Any())
                {
                    _logger.LogWarning("No se encontraron juguetes con nombre: {Nombre}", nombre);
                    return OperationResult<IEnumerable<JuguetesDTO>>.Failure("No se encontraron juguetes");
                }

                var juguetesMap = juguetesGet.Select(e => _juguetesMapping.MapEntity(e)).ToList();

                _logger.LogInformation("Juguetes encontrados exitosamente");

                return OperationResult<IEnumerable<JuguetesDTO>>.Success("Lista de juguetes obtenida correctamente", juguetesMap);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al traer juguetes por nombre: {Nombre}", nombre);
                return OperationResult<IEnumerable<JuguetesDTO>>.Failure("Error inesperado al traer juguetes por nombre");
            }
        }

        //Borrar
        public async Task<OperationResult<bool>> DeleteAsync(string id, bool confirmar)
        {
            try
            {
                var juguete = await _juguetesRepository.GetByIdStringAsync(id);

                if (juguete == null)
                {
                    _logger.LogWarning("No se encontro el juguete con ID {Id}", id);
                    return OperationResult<bool>.Failure("El juguete no existe");
                }

                if (!confirmar)
                {
                    _logger.LogInformation("El usuario cancelo la eliminacion del juguete con ID {Id}", id);
                    return OperationResult<bool>.Failure("La eliminacion fue cancelada por el usuario");
                }

                var eliminado = await _juguetesRepository.DeleteByStringIdAsync(id);

                if (!eliminado)
                {
                    return OperationResult<bool>.Failure("No se pudo eliminar");
                }
                    
                return OperationResult<bool>.Success("Eliminado correctamente", true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inesperado al eliminar juguete con ID {Id}", id);
                return OperationResult<bool>.Failure("Error inesperado al eliminar juguete");
            } 
        }
    }
}
