using ClassLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace UBUWHdb
{
    public class WHdb : IWHdb
    {

        Dictionary<int,Usuario> tblUsuarios = new Dictionary<int,Usuario>();
        int siguienteUsuario = 1;

        public WHdb() { }

        bool IWHdb.GuardaUsuario(Usuario u)
        {

            if (u.IdUsuario < 0) {
                u.IdUsuario = siguienteUsuario;
                siguienteUsuario++;
            }

            tblUsuarios[u.IdUsuario] = u;
            return true;
        }

        bool IWHdb.GuardaComponente(Componente e)
        {
            throw new NotImplementedException();
        }

        Componente IWHdb.LeeComponente(int idElemento)
        {
            throw new NotImplementedException();
        }

        Usuario IWHdb.LeeUsuario(string email)
        {
            return null;
        }

        int IWHdb.NumComponente()
        {
            throw new NotImplementedException();
        }

        int IWHdb.NumUsuarios()
        {
            return tblUsuarios.Count;
        }

        int IWHdb.NumUsuariosActivos()
        {
            throw new NotImplementedException();
        }

        bool IWHdb.ValidaUsuario(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
