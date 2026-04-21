using MauiAppMinhasCompras.Helpers;
using System.Globalization;

namespace MauiAppMinhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper? _db;

        public static SQLiteDatabaseHelper Db
        {
            get
            {
                if( _db == null)
                {
                    string caminho_do_arquivo = Path.Combine(
                        Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData),
                        "banco_sqlite_compras.db3"
                    );

                    _db = new SQLiteDatabaseHelper(caminho_do_arquivo);
                }

                return _db;
            }
        }

        public App()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            var window = new Window(new AppShell());

            // Ao iniciar, navegar para a tela de Login antes de mostrar a lista
            // Se quiser pular o login por enquanto, comente as próximas linhas
            window.Page.Dispatcher.Dispatch(async () =>
            {
                await window.Page.Navigation.PushAsync(new Views.LoginPage());
            });

            return window;
        }
    }
}