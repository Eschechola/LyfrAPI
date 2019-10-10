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

        public string GetNovoNomeLivro()
        {
            //atribui o nome novo do autor com a extensão jpg
            string nomeFoto = "lyfr_book" + DateTime.Now.ToString() + ".epub";
            //retira espaços em branco
            nomeFoto = nomeFoto.Replace(' ', '_');
            //retira barras
            nomeFoto = nomeFoto.Replace('/', '_');
            //retira dois pontos
            nomeFoto = nomeFoto.Replace(':', '_');

            return nomeFoto;
        }

        public string GetNovoNomeCapa()
        {
            //atribui o nome novo do autor com a extensão jpg
            string nomeFoto = "lyfr_cover" + DateTime.Now.ToString() + ".jpg";
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
