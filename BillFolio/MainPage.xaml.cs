using System;
using Microsoft.Maui.Controls;
using BillFolio.Models;
using System.Collections.ObjectModel;
using BillFolio.ViewModels;
//using Javax.Security.Auth;

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

		private void OnEditBillClicked(object sender, EventArgs e)
		{
			if (sender is Button button && button.CommandParameter is Bill bill)
			{
				// populate entry fields with the selected bill details for editing
				BillNameEntry.Text = bill.Name;
				BillAmountEntry.Text = bill.Amount.ToString();
				DueDateEntry.Text = bill.DueDate.ToString("MM/dd/yyyy");
				FrequencyPicker.SelectedItem = bill.Frequency;
				TypePicker.SelectedItem = bill.Type;

				// remove bill option in edit screen
				var viewModel = (MainPageViewModel)BindingContext;
				viewModel.DeleteBill(bill); //remove from list until saved
			}
		}
		private void OnDeleteBillClicked(object sender, EventArgs e)
		{
			if (sender is Button button && button.CommandParameter is Bill bill)
			{
				var viewModel = (MainPageViewModel)BindingContext;
				viewModel.DeleteBill(bill); //Calls the DeleteBill method in ViewModel
			}
		}
		private void ClearEntryFields()
		{
			BillNameEntry.Text = string.Empty;
			BillAmountEntry.Text = string.Empty;
			DueDateEntry.Text = string.Empty;
			FrequencyPicker.SelectedItem = null;
			TypePicker.SelectedItem = null;
		}
        protected override void OnAppearing()
        {
            base.OnAppearing();
            var bills = DatabaseHelper.GetAllBills();
            ((MainPageViewModel)BindingContext).Bills = new ObservableCollection<Bill>(bills);
        }

    }
}
		
		
