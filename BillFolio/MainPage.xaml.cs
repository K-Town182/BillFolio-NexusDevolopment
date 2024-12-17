using System;
using Microsoft.Maui.Controls;
using BillFolio.Models;
using BillFolio.ViewModels;
using System.Collections.ObjectModel;

namespace BillFolio
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
			BindingContext = new MainPageViewModel();
		}

		private async void OnAddBillClicked(object sender, EventArgs e)
		{
			// Validate input
			if (string.IsNullOrWhiteSpace(BillNameEntry.Text) ||
				!decimal.TryParse(BillAmountEntry.Text, out decimal amount) ||
				!DateTime.TryParse(DueDateEntry.Text, out DateTime dueDate) ||
				FrequencyPicker.SelectedItem == null ||
				TypePicker.SelectedItem == null)
			{
				// Show an alert to user about invalid input
				await DisplayAlert("Error", "Please provide valid inputs for all fields.", "OK");
				return;
			}

			// Create a new Bill object from user input
			var bill = new Bill
			{
				Name = BillNameEntry.Text,
				Amount = decimal.Parse(BillAmountEntry.Text),
				DueDate = DateTime.Parse(DueDateEntry.Text),
				Frequency = (BillFrequency)FrequencyPicker.SelectedItem,
				Type = (BillType)TypePicker.SelectedItem,
			};

			// Save the bill to the database
			await DatabaseHelper.SaveBillAsync(bill);

			// Get the ViewModel from the BindingContext and add the bill
			var viewModel = (MainPageViewModel)BindingContext;
			viewModel.AddBill(bill);

			// Clear the entry fields for new input
			ClearEntryFields();
		}

		private void OnEditBillClicked(object sender, EventArgs e)
		{
			if (sender is Button button && button.CommandParameter is Bill bill)
			{
				// Populate entry fields with the selected bill details for editing
				BillNameEntry.Text = bill.Name;
				BillAmountEntry.Text = bill.Amount.ToString();
				DueDateEntry.Text = bill.DueDate.ToString("MM/dd/yyyy");
				FrequencyPicker.SelectedItem = bill.Frequency;
				TypePicker.SelectedItem = bill.Type;

				// Store the bill being edited in a temporary variable
				var viewModel = (MainPageViewModel)BindingContext;
				viewModel.CurrentEditingBill = bill;
			}
		}

		private async void OnDeleteBillClicked(object sender, EventArgs e)
		{
			if (sender is Button button && button.CommandParameter is Bill bill)
			{
				var viewModel = (MainPageViewModel)BindingContext;
				await viewModel.DeleteBillAsync(bill); // Calls the DeleteBillAsync method in ViewModel
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

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			var bills = await DatabaseHelper.GetAllBillsAsync();
			((MainPageViewModel)BindingContext).Bills = new ObservableCollection<Bill>(bills);
		}
	}
}