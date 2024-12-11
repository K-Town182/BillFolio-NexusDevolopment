namespace BillFolio
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();
			DatabaseHelper.InitializeAsync();
			MainPage = new AppShell();
		}

        protected override void OnStart()
        {
            base.OnStart();
			
			DatabaseHelper.InitializeAsync();
        }
    }
}
