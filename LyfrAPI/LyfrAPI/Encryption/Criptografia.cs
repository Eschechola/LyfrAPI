using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APILyfr.Encryption
{
    public class Criptografia
    {
        //Vetor de Bytes para Criptografia
        private static byte[] bIV = { 0x50, 0x08, 0xF1, 0xDD, 0xDE, 0x3C, 0xF2, 0x18, 0x44, 0x74, 0x19, 0x2C, 0x53, 0x49, 0xAB, 0xBC };

        private const string ChaveEncriptacao =
        "Q3JpcHRvZ3JhZmlhcyBjb20gUmluamRhZWwgLyBBRVM=";

        public string Encrypt(string texto)
        {
            try
            {
                //Se a String não estiver vazia, então executa essa tarefa
                if (!string.IsNullOrEmpty(texto))
                {
                    //Criar instancia de vetores de Bytes
                    byte[] bkey = Convert.FromBase64String(ChaveEncriptacao);

                    byte[] bText = new UTF8Encoding().GetBytes(texto);

                    //Classe de criptografia Rijndael
                    Rijndael rijndael = new RijndaelManaged();

                    //Delimitar o tamanho das chaves
                    rijndael.KeySize = 256;

                    //Criar  espaço para guardar o valor encriptado
                    MemoryStream memoryStream = new MemoryStream();

                    //Instancia o Encriptador 
                    CryptoStream crypto = new CryptoStream(memoryStream, rijndael.CreateEncryptor(bkey, bIV), CryptoStreamMode.Write);

                    //Escrita dos dados criptografados no espaço de memória
                    crypto.Write(bText, 0, bText.Length);

                    //Despejar a memória
                    crypto.FlushFinalBlock();
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return "Não foi possível criptografar! Tente novamente.";
            }
        }

        public string Decrypt(string text)
        {
            try
            {
                // Se a string não está vazia, executa a criptografia           
                if (!string.IsNullOrEmpty(text))
                {
                    // Cria instancias de vetores de bytes com as chaves                
                    byte[] bKey = Convert.FromBase64String(ChaveEncriptacao);
                    byte[] bText = Convert.FromBase64String(text);

                    // Instancia a classe de criptografia Rijndael                
                    Rijndael rijndael = new RijndaelManaged();

                    // Define o tamanho da chave "256 = 8 * 32"                
                    // Lembre-se: chaves possíves:                
                    // 128 (16 caracteres), 192 (24 caracteres) e 256 (32 caracteres)                
                    rijndael.KeySize = 256;

                    // Cria o espaço de memória para guardar o valor DEScriptografado:               
                    MemoryStream mStream = new MemoryStream();

                    // Instancia o Decriptador                 
                    CryptoStream decryptor = new CryptoStream(mStream, rijndael.CreateDecryptor(bKey, bIV), CryptoStreamMode.Write);

                    // Faz a escrita dos dados criptografados no espaço de memória   
                    decryptor.Write(bText, 0, bText.Length);
                    // Despeja toda a memória.                
                    decryptor.FlushFinalBlock();
                    // Instancia a classe de codificação para que a string venha de forma correta         
                    UTF8Encoding utf8 = new UTF8Encoding();
                    // Com o vetor de bytes da memória, gera a string descritografada em UTF8       
                    return utf8.GetString(mStream.ToArray());
                }
                else
                {
                    // Se a string for vazia retorna nulo                
                    return null;
                }
            }
            catch (Exception)
            {
                // Se algum erro ocorrer, dispara a exceção            
                return "Não foi possível descriptografar! Tente novamente.";
            }
        }
    }
}
