namespace MauiAppMinhasCompras.Views;

public partial class CompraPage : ContentPage
{
    double total = 0;

    public CompraPage(double valorTotal)
    {
        InitializeComponent();
        total = valorTotal;
        lbl_total.Text = total.ToString("C");
        lbl_desconto.Text = "R$ 0,00";
        lbl_total_final.Text = total.ToString("C");
    }

    private void Picker_forma_SelectedIndexChanged(object sender, EventArgs e)
    {
        int idx = picker_forma.SelectedIndex;
        if (idx == -1) return;

        string forma = picker_forma.Items[idx];

        if (forma == "Pix")
        {
            double desconto = total * 0.10; // 10%
            double final = total - desconto;
            lbl_desconto.Text = desconto.ToString("C");
            lbl_total_final.Text = final.ToString("C");
        }
        else
        {
            lbl_desconto.Text = "R$ 0,00";
            lbl_total_final.Text = total.ToString("C");
        }
    }

    private async void BtnPagar_Clicked(object sender, EventArgs e)
    {
        try
        {
            string forma = picker_forma.SelectedIndex >= 0 ? picker_forma.Items[picker_forma.SelectedIndex] : string.Empty;
            if (string.IsNullOrEmpty(forma))
            {
                await DisplayAlertAsync("Atenção", "Escolha a forma de pagamento", "OK");
                return;
            }

            double final = System.Globalization.CultureInfo.CurrentCulture.Name == "pt-BR" ?
                double.Parse(lbl_total_final.Text.Replace("R$", "").Trim()) :
                double.Parse(lbl_total_final.Text, System.Globalization.NumberStyles.Currency);

            // Aqui apenas simula o pagamento
            await DisplayAlertAsync("Pagamento", $"Pagamento de {lbl_total_final.Text} via {forma} realizado com sucesso.", "OK");

            // Após pagar, volta para a lista ou limpa o carrinho
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Ops", ex.Message, "OK");
        }
    }
}
