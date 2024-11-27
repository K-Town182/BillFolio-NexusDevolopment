using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using BillFolio.Models;

namespace BillFolio.ViewModel
{
	public class SharedViewModel : INotifyPropertyChanged
	{
		private ObservableCollection<Bill> bills;
		public ObservableCollection<Bill> Bills
		{
			get => bills;
			set
			{
				bills = value;
				OnPropertyChanged();
			}
		}

		private DateTime lastPayDate;
		public DateTime LastPayDate
		{
			get => lastPayDate;
			set
			{
				lastPayDate = value;
				OnPropertyChanged();
			}
		}

		public SharedViewModel()
		{
			Bills = new ObservableCollection<Bill>();
			LastPayDate = DateTime.Today; // Default to today
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}