
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
		public ObservableCollection<BillFrequency> Frequencies { get; set; } //Add Frequencies property
		public ObservableCollection<BillType> Types { get; set; } //Add Type property


		//Properties for selected values
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

		// Command for deleting a bill
		public ICommand DeleteCommand { get; private set; }

		public MainPageViewModel()
		{
			sharedViewModel = ViewModelLocator.SharedViewModel;

			Frequencies = new ObservableCollection<BillFrequency>((BillFrequency[])Enum.GetValues(typeof(BillFrequency)));
			Types = new ObservableCollection<BillType>((BillType[])Enum.GetValues(typeof(BillType)));

			//Initialize the DeleteCommand
			DeleteCommand = new Command<Bill>(DeleteBill);
		}

		public void AddBill(Bill bill)
		{
			Bills.Add(bill);
			OnPropertyChanged(nameof(Bills)); // Notify the UI of the property change
		}

		public void DeleteBill(Bill bill)
		{
			if (Bills.Contains(bill))
			{
				Bills.Remove(bill);
				OnPropertyChanged(nameof(Bills)); // Notify the UI of the property change
			}
		}

		public void EditBill(Bill bill)
		{
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	
	}
}
