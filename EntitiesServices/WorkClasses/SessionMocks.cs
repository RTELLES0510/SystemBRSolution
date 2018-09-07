using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesServices.Model;

namespace EntitiesServices.Work_Classes
{
    public static class SessionMocks
    {
        public static USUARIO UserCredentials { get; set; }
        public static Int32? IdAssinante { get; set; }
        public static ASSINANTE Assinante { get; set; }
        public static List<USUARIO> listaUsuario { get; set; }
        public static USUARIO Usuario { get; set; }
        public static String NomeLogado { get; set; }
        public static List<LOG> listaLog { get; set; }
        public static List<PERFIL> listaPerfil { get; set; }
        public static String voltaLogin { get; set; }
        public static String origem { get; set; }
        public static Int32 idVolta { get; set; }
        public static String arquivo { get; set; }
        public static COLABORADOR Colaborador { get; set; }
    }
}
