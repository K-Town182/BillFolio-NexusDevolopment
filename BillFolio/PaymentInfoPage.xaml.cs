using BillFolio.ViewModel;
using Microsoft.Maui.Controls;
using System;

namespace BillFolio
{
	public partial class PaymentInfoPage : ContentPage
	{
		public PaymentInfoPage()
		{
			InitializeComponent();
			BindingContext = ViewModelLocator.SharedViewModel;
		}

		private void OnCalculatePayPeriodClicked(object sender, EventArgs e)
		{
			var viewModel = BindingContext as SharedViewModel;
			if (viewModel == null) return;

			DateTime lastPayDate = LastPayDatePicker.Date;
			viewModel.LastPayDate = lastPayDate;

			if (PayFrequencyPicker.SelectedItem == null)
			{
				DisplayAlert("Error", "Please select a pay frequency.", "OK");
				return;
			}

			string payFrequency = PayFrequencyPicker.SelectedItem as string;

			DateTime payPeriodStart = lastPayDate;
			DateTime payPeriodEnd = lastPayDate;
			DateTime nextPayDate = lastPayDate;

			switch (payFrequency)
			{
				case "Weekly":
					payPeriodEnd = payPeriodStart.AddDays(6);
					nextPayDate = payPeriodEnd.AddDays(1);
					break;

				case "Biweekly":
					payPeriodEnd = payPeriodStart.AddDays(13);
					nextPayDate = payPeriodEnd.AddDays(1);
					break;

				case "Monthly":
					payPeriodEnd = payPeriodStart.AddMonths(1).AddDays(-1);
					nextPayDate = payPeriodEnd.AddDays(1);
					break;

				default:
					break;
			}

			PayPeriodLabel.Text = $"Next Pay Period: {payPeriodStart:MM/dd/yyyy} - {payPeriodEnd:MM/dd/yyyy}";
			NextPayDateLabel.Text = $"Next Pay Date: {nextPayDate:MM/dd/yyyy}";
		}
	}
}