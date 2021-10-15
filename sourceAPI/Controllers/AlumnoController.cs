using ApiServ.Domain.Models;
using ApiServ.Domain.Repositories;
using ApiServ.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace ApiServ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnoController : Controller
    {
        private readonly IAlumnoService _alumnoService;
        private readonly ICreateTable _createTable;

        public AlumnoController(ICreateTable createTable, IAlumnoService alumnoService)
        {
            _createTable = createTable;
            _alumnoService = alumnoService;
        }

        [Route("createtable")]
        public IActionResult CreateDynamoDbTable()
        {
            _createTable.CreateDynamoDbTable();

            return Ok();
        }


        /// <summary>
        /// Agrega alumno en la base de datos dynamo.
        /// </summary>
        /// <param name="alumnos"></param>
        /// <returns>Lista de alumno con mensajes(success, message, mensajeServidor, List<Alumno> data).
        [Route("AgregaAlumno")]
        [HttpPost]
        public List<JsonResponse> AgregaAlumnos(Alumno alumnos)
        {
            List<JsonResponse> validaciones = new List<JsonResponse>();
            try {

                if (alumnos.Id.Equals(null) || alumnos.Id == 0 || alumnos.Nombre == "" || alumnos.Nombre == null || alumnos.AMaterno == "" || alumnos.AMaterno == null || alumnos.APaterno == "" || alumnos.Direccion == "" || alumnos.Correo == "") {

                    if (alumnos.Id.Equals(null) || alumnos.Id == 0)
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "El campo Id es un campo requerido.", mensajeServidor = HttpStatusCode.PreconditionRequired.ToString() });
                    }
                    if (alumnos.Nombre == "" || alumnos.Nombre == null)
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "El campo Nombre es un campo requerido.", mensajeServidor = HttpStatusCode.PreconditionRequired.ToString() });
                    }
                    if (alumnos.AMaterno == "" || alumnos.AMaterno == null)
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "El campo Apellido Materno es un campo requerido.", mensajeServidor = HttpStatusCode.PreconditionRequired.ToString() });
                    }
                    if (alumnos.APaterno == "" || alumnos.APaterno == null)
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "El campo Apellido paterno es un campo requerido.", mensajeServidor = HttpStatusCode.PreconditionRequired.ToString() });
                    }
                    if (alumnos.Direccion == "" || alumnos.Direccion == null)
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "El campo Direccion es un campo requerido.", mensajeServidor = HttpStatusCode.PreconditionRequired.ToString() });
                    }
                    if (alumnos.Correo == "" || alumnos.Correo == null)
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "El campo Correo es un campo requerido.", mensajeServidor = HttpStatusCode.PreconditionRequired.ToString() });
                    }

                }
                if (validaciones.Where(v => v.success == false).ToList().Count() > 0)
                {
                    return validaciones;
                }
                else
                {

                    var existeAlumno = _alumnoService.ObtenerlumnosService(alumnos.Id);

                    if (existeAlumno.Result.Alumnos.Count() > 0)
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "El registro con el id " + alumnos.Id + " ya existe", mensajeServidor = HttpStatusCode.NotAcceptable.ToString() });
                        return validaciones;
                    }
                    else
                    {
                        Alumno alumno = new Alumno();
                        alumno.Id = alumnos.Id;
                        alumno.Nombre = alumnos.Nombre;
                        alumno.AMaterno = alumnos.AMaterno;
                        alumno.APaterno = alumnos.APaterno;
                        alumno.Direccion = alumnos.Direccion;
                        alumno.Correo = alumnos.Correo;

                        _alumnoService.AgregaAlumnosService(alumno);
                        validaciones.Add(new JsonResponse { success = true, message = "El registro se insertó corrrectamente.", mensajeServidor = HttpStatusCode.OK.ToString() });

                        return validaciones;

                    }

                }
            }
            catch {
                validaciones.Add(new JsonResponse { success = false, message = "Error inesperado.", mensajeServidor = HttpStatusCode.InternalServerError.ToString() });
                return validaciones;
            }

        }


        /// <summary>
        /// Busca alumnos o alumno, 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Lista de alumnos con mensajes(success, message, mensajeServidor, List<Alumno> data).
        [Route("BuscaAlumno")]
        [HttpGet]
        public List<JsonResponse> BuscaAlumnos(int? Id)
        {
            List<JsonResponse> validaciones = new List<JsonResponse>();
            try
            {

                var existeAlumno = _alumnoService.ObtenerlumnosService(Id);


                if (existeAlumno.Result.Alumnos.Count() == 0)
                {
                    validaciones.Add(new JsonResponse { success = false, message = "No hay registros asociados.", mensajeServidor = HttpStatusCode.NoContent.ToString() });
                    return validaciones;
                }
                else
                {
                    validaciones.Add(new JsonResponse { success = true, message = "Todo bien", data = existeAlumno.Result.Alumnos.ToList(), mensajeServidor = HttpStatusCode.OK.ToString() });
                    return validaciones;
                }
            }
            catch {
                validaciones.Add(new JsonResponse { success = false, message = "Algo inesperado courrió.", mensajeServidor = HttpStatusCode.InternalServerError.ToString() });
                return validaciones;
            }

        }


        /// <summary>
        /// Actualiza alumno en la Base de datos Dynamo.
        /// </summary>
        /// <param name="alumnos"></param>
        /// <returns>Lista de alumno con mensajes(success, message, mensajeServidor, List<Alumno> data).
        [HttpPut]
        [Route("ActualizaAlumno")]
        public List<JsonResponse> ActualizaAlumno(Alumno alumnos)
        {

            List<JsonResponse> validaciones = new List<JsonResponse>();
            Alumno alumno = new Alumno();


            try
            {

                if (alumnos.Id.Equals(null) || alumnos.Id == 0 || alumnos.Direccion == "" || alumnos.Direccion == null)
                {
                    if (alumnos.Id.Equals(null) || alumnos.Id == 0)
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "El campo id es un campo requerido.", mensajeServidor = HttpStatusCode.PreconditionRequired.ToString() });
                    }
                    if (alumnos.Direccion == "" || alumnos.Direccion == null)
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "El campo direccion es un campo requerido.", mensajeServidor = HttpStatusCode.PreconditionRequired.ToString() });
                    }
                }
                else
                {

                    var existeAlumno = _alumnoService.ObtenerlumnosService(alumnos.Id);

                    if (existeAlumno.Result.Alumnos.Count() == 0)
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "El id no existe.", mensajeServidor = HttpStatusCode.NotFound.ToString() });
                    }
                    else
                    {
                        alumno.Id = alumnos.Id;
                    }
                }

                if (validaciones.Where(v => v.success == false).ToList().Count() > 0)
                {
                    return validaciones;
                }
                else
                {

                    alumno.Id = alumnos.Id;
                    alumno.Direccion = alumnos.Direccion;

                    var response = _alumnoService.ActualizaAlumnoService(alumno);

                    if (response.Result != null)
                    {
                        validaciones.Add(new JsonResponse { success = true, message = "Salio todo perfect", mensajeServidor = HttpStatusCode.OK.ToString() });
                        return validaciones;
                    }
                    else
                    {
                        validaciones.Add(new JsonResponse { success = false, message = "Algo inesperado courrió.", mensajeServidor = HttpStatusCode.InternalServerError.ToString() });
                        return validaciones;
                    }

                }
            }
            catch {
                validaciones.Add(new JsonResponse { success = false, message = "Algo inesperado courrió.", mensajeServidor = HttpStatusCode.InternalServerError.ToString() });
                return validaciones;
            }

        }

        /// <summary>
        /// Elimina alumno en la Base de datos Dynamo.
        /// </summary>
        /// <param name="alumnos"></param>
        /// <returns>Lista de alumno con mensajes(success, message, mensajeServidor).
        [HttpDelete]
        [Route("EliminaAlumno")]
        public List<JsonResponse> EliminaAlumno(int id)
        {

            Alumno alumno = new Alumno();
            List<JsonResponse> validaciones = new List<JsonResponse>();

            try
            {
                Int32.Parse(id.ToString());

           
            if (id.Equals(null) || id==0)
            {
                validaciones.Add(new JsonResponse { success = false, message = "El campo id es un campo requerido.", mensajeServidor = HttpStatusCode.PreconditionRequired.ToString() });
            }
            else {

                var existeAlumno = _alumnoService.ObtenerlumnosService(id);

                if (existeAlumno.Result.Alumnos.Count() == 0)
                {
                    validaciones.Add(new JsonResponse { success = false, message = "El id no existe.", mensajeServidor = HttpStatusCode.NotFound.ToString()});
                }
                else {
                    alumno.Id = id;
                }  
            }

            if (validaciones.Where(v => v.success == false).ToList().Count() > 0)
            {
                return validaciones;
            }
            else {
                _alumnoService.EliminaAlumnoService(alumno);

                validaciones.Add(new JsonResponse { success = true, message = "Salio todo perfect.", mensajeServidor = HttpStatusCode.OK.ToString() });
                return validaciones;

            }
            }
            catch
            {
                validaciones.Add(new JsonResponse { success = false, message = "El dato no es un int.", mensajeServidor = HttpStatusCode.InternalServerError.ToString() });
                return validaciones;
            }

        }

    }

}
