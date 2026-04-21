using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void BtnEntrar_Clicked(object sender, EventArgs e)
    {
        try
        {
            // Lê os valores dos campos de usuário e senha
            string user = txt_username.Text?.Trim() ?? string.Empty;
            string pass = txt_password.Text ?? string.Empty;

            // Chama o método de autenticação do helper de banco
            bool ok = await App.Db.Authenticate(user, pass);
            if (ok)
            {
                // Se autenticado com sucesso, navega para a lista de produtos
                await Navigation.PushAsync(new Views.ListaProdutos());
            }
            else
            {
                // Senha ou usuário inválido: mostra alerta
                await DisplayAlertAsync("Erro", "Usuário ou senha inválidos", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }

    private async void BtnCadastro_Clicked(object sender, EventArgs e)
    {
        // Navega para a página de cadastro de usuário
        await Navigation.PushAsync(new Views.CadastroUsuario());
    }
}
