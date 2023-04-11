using CAD.DSSuministrosPDFTableAdapters;

namespace CAD
{
    public class CADUsuarios
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
