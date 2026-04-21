using MauiAppMinhasCompras.Models;
using SQLite;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace MauiAppMinhasCompras.Helpers
{
    public class SQLiteDatabaseHelper
    {
        readonly SQLiteAsyncConnection conexao;

        public SQLiteDatabaseHelper(string caminho_pro_db3)
        {
            conexao = new SQLiteAsyncConnection(caminho_pro_db3);
            conexao.CreateTableAsync<Produto>().Wait();
            conexao.CreateTableAsync<Usuario>().Wait();
        }

        public Task<int> Insert(Produto p)
        {
            return conexao.InsertAsync(p);
        }

        public Task<List<Produto>> Update(Produto p)
        {
            string sql = "Update Produto SET Descricao=?, Quantidade=?, Preco=? " +
                         "WHERE Id=?";

            return conexao.QueryAsync<Produto>(
                sql, p.Descricao, p.Quantidade, p.Preco, p.Id
            );
        }

        public Task<int> Delete(int id)
        {
            return conexao.Table<Produto>().DeleteAsync(i => i.Id == id);
        }

        public Task<List<Produto>> GetAll()
        {
            return conexao.Table<Produto>().ToListAsync();
        }
        public Task<List<Produto>> Search(string q)
        {
            string sql = $"SELECT * FROM Produto WHERE Descricao LIKE '%{q}%' ";
            return conexao.QueryAsync<Produto>(sql);
        }

        // ===== Métodos para usuários =====
        public Task<int> InsertUsuario(Usuario u)
        {
            return conexao.InsertAsync(u);
        }

        public Task<Usuario?> GetUsuarioByUsername(string username)
        {
            return conexao.Table<Usuario>()
                   .Where(x => x.Username == username)
                   .FirstOrDefaultAsync();
        }

        public async Task<bool> Authenticate(string username, string password)
        {
            var u = await GetUsuarioByUsername(username);
            if (u == null) return false;

            string hash = ComputeHash(password);
            return u.PasswordHash == hash;
        }

        public static string ComputeHash(string input)
        {
            if (input == null) input = string.Empty;
            using SHA256 sha = SHA256.Create();
            byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes) sb.Append(b.ToString("x2"));
            return sb.ToString();
        }
    } // Fecha classe
} // Fecha namespace
