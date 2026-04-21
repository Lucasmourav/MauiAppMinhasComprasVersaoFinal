using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Usuario
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        // Armazena o hash da senha
        public string PasswordHash { get; set; } = string.Empty;
    }
}