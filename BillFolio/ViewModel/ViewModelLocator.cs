using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillFolio.ViewModel
{
	public static class ViewModelLocator
	{
		private static SharedViewModel _sharedViewModel;
		public static SharedViewModel SharedViewModel => _sharedViewModel ??= new SharedViewModel();
	}
}
