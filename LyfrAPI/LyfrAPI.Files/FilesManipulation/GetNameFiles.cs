using System;
using System.Collections.Generic;
using System.Text;

namespace LyfrAPI.Files
{
    public class GetNameFiles
    {
        public string GetNovoNomeAutor()
        {
            //atribui o nome novo do autor com a extensão jpg
            string nomeFoto = DateTime.Now.ToString() + ".jpg";
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
