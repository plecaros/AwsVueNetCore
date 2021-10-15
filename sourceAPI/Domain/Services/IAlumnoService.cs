using ApiServ.Domain.Models;
using System.Threading.Tasks;

namespace ApiServ.Domain.Services
{
    public interface IAlumnoService
    {
         void AgregaAlumnosService(Alumno alumno);
         Task<ItemsTableAlumnos> ObtenerlumnosService(int? id);
         Task<Alumno> ActualizaAlumnoService(Alumno alumno);
         Task<bool> EliminaAlumnoService(Alumno alumno);
    }
}
