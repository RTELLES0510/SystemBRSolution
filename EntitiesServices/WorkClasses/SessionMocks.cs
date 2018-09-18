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
        public static CONFIGURACAO Configuracao { get; set; }
        public static List<BANCO> listaBanco { get; set; }
        public static List<TIPO_CONTA> listaTipoConta { get; set; }
        public static List<CONTA_BANCARIA> listaContaBancaria { get; set; }
        public static BANCO banco { get; set; }
        public static Int32? IdMatriz { get; set; }
        public static MATRIZ Matriz { get; set; }
        public static List<FILIAL> listaFilial { get; set; }
        public static List<CLIENTE> listaCliente { get; set; }
        public static CLIENTE cliente { get; set; }
        public static Int32 voltaCliente { get; set; }
        public static List<FORNECEDOR> listaFornecedor { get; set; }
        public static FORNECEDOR fornecedor { get; set; }
        public static Int32 voltaFornecedor { get; set; }
        public static List<PRODUTO> listaProduto { get; set; }
        public static PRODUTO produto { get; set; }
        public static Int32 voltaProduto { get; set; }
        public static LOG filtroLog { get; set; }
        public static List<MATERIA_PRIMA> listaMateria { get; set; }
        public static MATERIA_PRIMA materiaPrima { get; set; }
        public static Int32 voltaMateria { get; set; }
        public static MATERIA_PRIMA filtroMateria { get; set; }
    }
}
