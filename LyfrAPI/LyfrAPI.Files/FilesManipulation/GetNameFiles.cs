using System;
using System.Collections.Generic;
using System.Text;

namespace LyfrAPI.Files
{
    public class GetNameFiles
    {
        public string GetNovoNome(string prefixo, string extensao)
        {
            //atribui o nome novo do autor com a extensão jpg
            string nomeFoto = prefixo + DateTime.Now.ToString() + extensao;
            //retira espaços em branco
            nomeFoto = nomeFoto.Replace(' ', '_');
            //retira barras
            nomeFoto = nomeFoto.Replace('/', '_');
            //retira dois pontos
            nomeFoto = nomeFoto.Replace(':', '_');

            return nomeFoto;
        }
    }
}
