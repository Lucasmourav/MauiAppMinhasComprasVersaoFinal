using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class CadastroUsuario : ContentPage
{
    public CadastroUsuario()
    {
        InitializeComponent();
    }

    private async void BtnSalvar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Lê valores dos campos
            string user = txt_username.Text?.Trim() ?? string.Empty;
            string pass = txt_password.Text ?? string.Empty;
            string pass2 = txt_password_confirm.Text ?? string.Empty;

            // Valida se o usuário foi informado
            if (string.IsNullOrEmpty(user))
            {
                await DisplayAlertAsync("Atenção", "Informe o usuário", "OK");
                return;
            }

            // Valida se as senhas conferem
            if (pass != pass2)
            {
                await DisplayAlertAsync("Atenção", "Senhas não conferem", "OK");
                return;
            }

            // Verifica se já existe um usuário com o mesmo nome
            var existing = await App.Db.GetUsuarioByUsername(user);
            if (existing != null)
            {
                await DisplayAlertAsync("Atenção", "Usuário já existe", "OK");
                return;
            }

            // Cria o objeto Usuario com o hash da senha e insere no DB
            Usuario u = new Usuario
            {
                Username = user,
                PasswordHash = Helpers.SQLiteDatabaseHelper.ComputeHash(pass)
            };

            await App.Db.InsertUsuario(u);

            // Confirmação e volta para a tela anterior
            await DisplayAlertAsync("Sucesso", "Usuário criado", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }
}
