namespace BillFolio
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			InitializeDatabaseAsync();
			MainPage = new AppShell();
		}

        protected override void OnStart()
        {
            base.OnStart();
			
			InitializeDatabaseAsync();
        }

		private async void InitializeDatabaseAsync()
		{
			await DatabaseHelper.InitializeAsync();
		}
    }
}
