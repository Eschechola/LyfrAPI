namespace APILyfr.Models.Security
{
    public class LoginToken
    {
        public string Usuario { get; set; }
        public string Senha { get; set; }
        // 'M' - Mobile | 'W' - Web | 'A' - Adm
        public string TipoUsuario { get; set; }
    }
}
