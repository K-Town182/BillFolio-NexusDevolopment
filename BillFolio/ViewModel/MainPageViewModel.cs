
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using BillFolio.Models;

namespace BillFolio.ViewModels
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Bill> Bills { get; set; } = new ObservableCollection<Bill>();
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

		public MainPageViewModel()
		{
			// Initialize with any default data if needed
			Frequencies = new ObservableCollection<BillFrequency>((BillFrequency[])Enum.GetValues(typeof(BillFrequency)));
			Types = new ObservableCollection<BillType>((BillType[])Enum.GetValues(typeof(BillType)));

		}

		public void AddBill(Bill bill)
		{
			Bills.Add(bill);
			OnPropertyChanged(nameof(Bills)); // Notify the UI of the property change
		}

		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
