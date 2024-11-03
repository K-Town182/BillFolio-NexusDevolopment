
using System.Collections.ObjectModel;
using System.ComponentModel;
using BillFolio.Models;

namespace BillFolio.ViewModels
{
	public class MainPageViewModel : INotifyPropertyChanged
	{
		public ObservableCollection<Bill> Bills { get; set; } = new ObservableCollection<Bill>();

		public MainPageViewModel()
		{
			// Optional: Initialize with any default data if needed
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
