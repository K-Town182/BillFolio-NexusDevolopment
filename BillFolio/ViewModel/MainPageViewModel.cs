using System.Windows.Input;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using BillFolio.Models;
using BillFolio.ViewModel;

namespace BillFolio.ViewModels
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		private SharedViewModel sharedViewModel;

		public ObservableCollection<Bill> Bills
		{
			get => sharedViewModel.Bills;
			set
			{
				sharedViewModel.Bills = value;
				OnPropertyChanged(nameof(Bills));
			}
		}

		// Properties for enum Values
		public ObservableCollection<BillFrequency> Frequencies { get; set; }
		public ObservableCollection<BillType> Types { get; set; }

		// Properties for selected values
		private BillFrequency _selectedFrequency;
		private BillType _selectedType;

		public BillFrequency SelectedFrequency
		{
			get => _selectedFrequency;
			set
			{
				if (_selectedFrequency != value)
				{
					_selectedFrequency = value;
					OnPropertyChanged(nameof(SelectedFrequency));
				}
			}
		}

		public BillType SelectedType
		{
			get => _selectedType;
			set
			{
				if (_selectedType != value)
				{
					_selectedType = value;
					OnPropertyChanged(nameof(SelectedType));
				}
			}
		}

		// Commands for editing and deleting a bill
		public ICommand EditCommand { get; private set; }
		public ICommand DeleteCommand { get; private set; }

		// Property to store the bill being edited
		private Bill _currentEditingBill;
		public Bill CurrentEditingBill
		{
			get => _currentEditingBill;
			set
			{
				if (_currentEditingBill != value)
				{
					_currentEditingBill = value;
					OnPropertyChanged(nameof(CurrentEditingBill));
				}
			}
		}

		public MainPageViewModel()
		{
			sharedViewModel = ViewModelLocator.SharedViewModel;

			Frequencies = new ObservableCollection<BillFrequency>((BillFrequency[])Enum.GetValues(typeof(BillFrequency)));
			Types = new ObservableCollection<BillType>((BillType[])Enum.GetValues(typeof(BillType)));

			// Initialize the commands
			EditCommand = new Command<Bill>(OnEditBill);
			DeleteCommand = new Command<Bill>(async (bill) => await DeleteBillAsync(bill));
		}

		private void OnEditBill(Bill bill)
		{
			CurrentEditingBill = bill;
			// Notify the view to update the entry fields if necessary
			OnPropertyChanged(nameof(CurrentEditingBill));
		}

		public void AddBill(Bill bill)
		{
			Bills.Add(bill);
			OnPropertyChanged(nameof(Bills)); // Notify the UI of the property change
		}

		public async Task DeleteBillAsync(Bill bill)
		{
			if (Bills.Contains(bill))
			{
				Bills.Remove(bill);
				OnPropertyChanged(nameof(Bills)); // Notify the UI of the property change
				var result = await DatabaseHelper.DeleteBillAsync(bill.Id); // Remove the bill from the database
				Console.WriteLine($"DeleteBillAsync: UI updated, result: {result}");
			}
		}

		public void DeleteBill(Bill bill)
		{
			if (Bills.Contains(bill))
			{
				Bills.Remove(bill);
				OnPropertyChanged(nameof(Bills)); // Notify the UI of the property change
			}
		}

		public async Task EditBillAsync(Bill bill)
		{
			var existingBill = Bills.FirstOrDefault(b => b.Id == bill.Id);
			if (existingBill != null)
			{
				existingBill.Name = bill.Name;
				existingBill.Amount = bill.Amount;
				existingBill.DueDate = bill.DueDate;
				existingBill.Frequency = bill.Frequency;
				existingBill.Type = bill.Type;
				existingBill.IsPaid = bill.IsPaid;

				OnPropertyChanged(nameof(Bills)); // Notify the UI of the property change
				var result = await DatabaseHelper.UpdateBillAsync(existingBill); // Update the bill in the database
				Console.WriteLine($"EditBillAsync: Updated bill with ID {bill.Id}, result: {result}");
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}