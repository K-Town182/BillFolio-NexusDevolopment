using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Xaml;

namespace BillFolio
{

	public partial class PaymentInfoPage : ContentPage
	{
		
		public PaymentInfoPage ()
		{
			InitializeComponent ();
			

		}

		//event handler for calculating the pay period
		private void OnCalculatePayPeriodClicked(object sender, EventArgs e)
		{

			//get the selected last pay date
			DateTime lastPayDate = LastPayDatePicker.Date;

			//check if the pay frequency is selected
			if (PayFrequencyPicker.SelectedItem == null)
			{
				DisplayAlert("Error", "Please select a pay frequency.", "OK");
				return;
			}


			//get the selected pay frequency
			string payFrequency = PayFrequencyPicker.SelectedItem as string;

			//initialize the start and end dates for the pay period
			DateTime payPeriodStart = lastPayDate;
			DateTime payPeriodEnd = lastPayDate;
			DateTime nextPayDate = lastPayDate;

			//calculate the pay period based on the selected frequency
			switch (payFrequency)
			{
				case "Weekly":
					payPeriodEnd = payPeriodStart.AddDays(6); // Weekly, 6 days after last pay date
					nextPayDate = payPeriodEnd.AddDays(1); //for weekly, the next pay date is the end date
					break;

				case "Biweekly":
					payPeriodEnd = payPeriodStart.AddDays(13); // Biweekly, 13 days after last pay date
					nextPayDate = payPeriodEnd.AddDays(1); // For biweekly, the next pay date is the end date
					break;
				
				case "Monthly":
					payPeriodEnd = payPeriodStart.AddMonths(1).AddDays(-1); // Monthly, 1 month later
					nextPayDate = payPeriodEnd.AddDays(1); // For monthly, the next pay date is the end date
					break;

				default:
					break;


				
			}

			// Display the pay period range and next pay date
			PayPeriodLabel.Text = $"Next Pay Period: {payPeriodStart:MM/dd/yyyy} - {payPeriodEnd:MM/dd/yyyy}";
			NextPayDateLabel.Text = $"Next Pay Date: {nextPayDate:MM/dd/yyyy}";
		}
	}

}

/* Will be adding user settings here
 * with notifications
 * and light/darkmode
 * maybe also analytics
 */