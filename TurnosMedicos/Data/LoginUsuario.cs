using TurnosMedicos.Models;

namespace TurnosMedicos.Data
{
    public class LoginUsuario
    {
        public List<Usuario> ListaUsuario()
        {
            return new List<Usuario> {
                    new Usuario{ Nombre = "Administrador", Email = "administrador@gmail.com", Password = "1234", Rol = "Administrador" },
                    new Usuario{ Nombre = "Supervisor", Email = "supervisor@gmail.com", Password = "4321", Rol = "Supervisor"},
                    new Usuario{ Nombre = "Paciente", Email = "paciente@gmail.com", Password = "1111", Rol = "Paciente"}
                };
        }

        public Usuario ValidarUsuario(String _email, String _password)
        {
            return ListaUsuario().Where(item => item.Email == _email && item.Password == _password).FirstOrDefault();
        }
    }
}
