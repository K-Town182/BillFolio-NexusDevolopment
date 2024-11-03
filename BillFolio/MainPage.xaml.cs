using System;
using Microsoft.Maui.Controls;
using BillFolio.Models;
using BillFolio.ViewModels;

namespace BillFolio
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

		private void OnAddBillClicked(object sender, EventArgs e)
		{
			// Create a new Bill object from user input
			var bill = new Bill
			{
				Name = BillNameEntry.Text,
				Amount = decimal.Parse(BillAmountEntry.Text),
				DueDate = DateTime.Parse(DueDateEntry.Text)
			};

			// Get the ViewModel from the BindingContext and add the bill
			var viewModel = (MainPageViewModel)BindingContext;
			viewModel.AddBill(bill);

			// Clear the entry fields for new input
			BillNameEntry.Text = string.Empty;
			BillAmountEntry.Text = string.Empty;
			DueDateEntry.Text = string.Empty;
		}
	}
}