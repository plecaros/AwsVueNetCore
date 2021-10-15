using ApiServ.Domain.Models;
using System.Threading.Tasks;

namespace ApiServ.Domain.Repositories
{
    public interface IAlumnoRepository
    {
        Task AgregaAlumnosRepo(Alumno alumno);
        Task<ItemsTableAlumnos> ObtenerAlumnosRepo(int? Id);
        Task<Alumno> ActualizaAlumnoRepo(Alumno alumno);
        Task<bool> EliminaAlumnoRepo(Alumno alumno);

    }
}
