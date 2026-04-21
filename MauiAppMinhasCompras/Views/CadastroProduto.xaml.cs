using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class CadastroProduto : ContentPage
{
	public CadastroProduto()
	{
		InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
		try
		{
            // Pega o produto que pode ter sido passado como BindingContext (edição)
			Produto? produto_anexado = BindingContext as Produto;

			Produto p = new()
			{
				Descricao = txt_descricao.Text,
				Preco = Convert.ToDouble(txt_preco.Text),
				Quantidade = Convert.ToDouble(txt_quantidade.Text)
			};

			if(produto_anexado == null)
			{
                // Se não há produto anexado, insere um novo no banco
				await App.Db.Insert(p);
            } else
			{
				p.Id = produto_anexado.Id;
                // Se existe produto anexado, atualiza o registro no banco
				await App.Db.Update(p);
			}			
			
			await DisplayAlertAsync("Sucesso!", "Dados Gravados!", "OK");
			await Navigation.PopAsync();
		}
		catch (Exception ex)
		{
			await DisplayAlertAsync("Ops", ex.Message, "OK");
		}
    }
}