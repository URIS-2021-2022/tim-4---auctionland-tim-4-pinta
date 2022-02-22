namespace Licitacija.Models
{
    /// <summary>
    /// Predstavlja model za autentifikaciju
    /// </summary>
    public class Principal
    {
        /// <summary>
        /// Korisničko ime
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Korisnička lozinka
        /// </summary>
        public string Password { get; set; }
    }
}
