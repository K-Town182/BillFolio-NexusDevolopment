using Microsoft.Maui.Controls;

namespace BillFolio
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("TwoWeekView", typeof(TwoWeekView));
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);

            // Redirect to TwoWeekView on startup
            if (args.Current.Location.OriginalString == "//")
            {
                Shell.Current.GoToAsync("//TwoWeekView");
            }
        }
    }
}