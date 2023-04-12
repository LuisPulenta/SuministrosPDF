using CADSistema.DSSistemaTableAdapters;

namespace CADSistema
{
    public class CADUsuario
    {
        private static UsuariosTableAdapter adaptador = new UsuariosTableAdapter();
        public static bool ValidaUsuario(string IDUsuario, string Clave)
        {
            if (adaptador.ValidaUsuario(IDUsuario, Clave) == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}