using ApiServ.Domain.Models;
using ApiServ.Domain.Repositories;
using ApiServ.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiServ.Services
{
    public class AlumnoService : IAlumnoService
    {
        private readonly IAlumnoRepository _alumnoRepository;

        public AlumnoService(IAlumnoRepository AlumnoRepository)
        {
            _alumnoRepository = AlumnoRepository;
        }

        public void AgregaAlumnosService(Alumno alumno)
        {
            _alumnoRepository.AgregaAlumnosRepo(alumno);
        }

        public async Task<ItemsTableAlumnos> ObtenerlumnosService(int? Id)
        {
            var resultado = await _alumnoRepository.ObtenerAlumnosRepo(Id);
            return resultado;
        }

        public async Task<Alumno> ActualizaAlumnoService(Alumno alumno)
        {
            var resultado = await _alumnoRepository.ActualizaAlumnoRepo(alumno);

            return resultado;
        }

        public async Task<bool> EliminaAlumnoService(Alumno alumno)
        {

            var resultado = await _alumnoRepository.EliminaAlumnoRepo(alumno);

            return resultado;
        }

    }
}
