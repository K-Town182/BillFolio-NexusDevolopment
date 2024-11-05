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
			// Validate input
			if (string.IsNullOrWhiteSpace(BillNameEntry.Text) ||
				!decimal.TryParse(BillAmountEntry.Text, out decimal amount) ||
				!DateTime.TryParse(DueDateEntry.Text, out DateTime dueDate) ||
				FrequencyPicker.SelectedItem == null ||
				TypePicker.SelectedItem == null)
			{
				//Show an alert to user about invalid input
				DisplayAlert("Error", "Please provide valid inputs for all fields.", "OK");
				return;
			}

			// Create a new Bill object from user input
			var bill = new Bill
			{
				Name = BillNameEntry.Text,
				Amount = decimal.Parse(BillAmountEntry.Text),
				DueDate = DateTime.Parse(DueDateEntry.Text),
				Frequency = (BillFrequency)FrequencyPicker.SelectedItem, //Get selected frequency
				Type = (BillType)TypePicker.SelectedItem, // Get selected type

		};

			// Get the ViewModel from the BindingContext and add the bill
			var viewModel = (MainPageViewModel)BindingContext;
			viewModel.AddBill(bill);

			// Clear the entry fields for new input
			BillNameEntry.Text = string.Empty;
			BillAmountEntry.Text = string.Empty;
			DueDateEntry.Text = string.Empty;
			FrequencyPicker.SelectedItem = null;
			TypePicker.SelectedItem = null;

		}
	}
}