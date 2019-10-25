using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LyfrAPI.Files
{
    public class FilesManipulation
    {
        //a api vai receber o arquivo em formato base64,
        //vai transformar em arquivo e vai salvar no diretório informado 

        public string ConverterDeArquivoEmBase64(string diretorioArquivo)
        {
            var diretorioArquivoArrumado = diretorioArquivo.Replace(@"\\", @"\");
            try
            {
                //le todos os bytes do arquivo que está no diretorio informado
                byte[] bytesDoArquivo = File.ReadAllBytes(Path.Combine(diretorioArquivoArrumado));
                //converte esses bytes para a base64
                string arquivoBase64 = Convert.ToBase64String(bytesDoArquivo);
                //retorna a base64 em formato de stringg
                return arquivoBase64.ToString();
            }
            catch (Exception)
            {
                return "Não foi possível converter o arquivo";
            }
        }

        public bool ConverterDeBase64EmArquivo(string diretorioArquivo, string nomeArquivo, string arquivoEmBase64)
        {
            var diretorioArquivoArrumado = diretorioArquivo.Replace(@"\\", @"\");
            try
            {
                //transforma a string de base64 em arquivo, e salva com o nome informado no diretorio informado
                File.WriteAllBytes(Path.Combine(diretorioArquivoArrumado, nomeArquivo), Convert.FromBase64String(arquivoEmBase64));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeletarArquivo(string diretorioArquivo)
        {
            try
            {
                //se for o arquivo NotFound não deleta
                if (diretorioArquivo.IndexOf("NotFound") >= 0)
                {
                    return false;
                }
                else
                {
                    File.Delete(diretorioArquivo);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string PegarDiretorioLink(string link)
        {
            try
            {
                //pega a posição dos caracteres br/ (Final do link)
                var indexChar = link.IndexOf("br/");

                //diretorio que sera retornada
                string diretorioLink = "";

                //i começa em 3 pois ele vai pegar a primeira ocorrencia do br/ no caso o caracter 'b'
                //em 3 ele ja tira a / e os caracteres
                for (int i = indexChar + 3; i < link.Length; i++)
                {
                    diretorioLink += link[i];
                }

                return "wwwroot/" + diretorioLink;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
