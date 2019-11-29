using LyfrAPI.Models.ModelRoute;
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


        public string GetCoverNameFileByLink(string link)
        {
            var rotaPadrao = @DefaultRoute.RotaPadrao + "/Livros/Capas/";
            var nomeArquivo = link.Replace(rotaPadrao, "");

            return nomeArquivo;
        }

        public string GetFileNameFileByLink(string link)
        {
            var rotaPadrao = @"wwwroot/Livros/Epubs/";
            var nomeArquivo = link.Replace(rotaPadrao, "");

            return nomeArquivo;
        }
    }
}
