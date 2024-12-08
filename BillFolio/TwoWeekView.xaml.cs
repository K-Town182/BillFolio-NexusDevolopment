using BillFolio.ViewModel;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BillFolio
{
	public partial class TwoWeekView : ContentPage
	{
		private Dictionary<DateTime, Border> dateBorders = new Dictionary<DateTime, Border>();

		public TwoWeekView()
		{
			InitializeComponent();
			BindingContext = ViewModelLocator.SharedViewModel;
			var viewModel = BindingContext as SharedViewModel;
			if (viewModel != null)
			{
				viewModel.PropertyChanged += ViewModel_PropertyChanged;
			}
			PopulateCalendar();
		}

		private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(SharedViewModel.LastPayDate))
			{
				PopulateCalendar();
			}
		}

		private void PopulateCalendar()
		{
			
			var viewModel = BindingContext as SharedViewModel;
			if (viewModel == null) return;

			calendarGrid.Children.Clear();
			checklistLayout.Children.Clear();
			dateBorders.Clear();

			DateTime startDate = viewModel.LastPayDate;

			for (int i = 0; i < 14; i++)
			{
				DateTime date = startDate.AddDays(i);

				Label dayLabel = new Label
				{
					Text = date.ToString("MMM dd"),
					HorizontalOptions = LayoutOptions.Center,
					VerticalOptions = LayoutOptions.Center,
					TextColor = Colors.Black,
					FontSize = 14,
					Padding = new Thickness(5),
					HorizontalTextAlignment = TextAlignment.Center,
					VerticalTextAlignment = TextAlignment.Center
				};

				Color backgroundColor;
				if (date == DateTime.Today)
				{
					backgroundColor = Colors.LightBlue;
				}
				else if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
				{
					backgroundColor = Colors.White;
				}
				else
				{
					backgroundColor = Colors.LightGray;
				}

				Border border = new Border
				{
					Content = dayLabel,
					Stroke = Colors.Gray,
					StrokeThickness = 1,
					BackgroundColor = backgroundColor,
					Padding = new Thickness(2),
					HorizontalOptions = LayoutOptions.Fill,
					VerticalOptions = LayoutOptions.Fill
				};

				int row = i / 7;
				int column = i % 7;

				Grid.SetRow(border, row);
				Grid.SetColumn(border, column);
				calendarGrid.Children.Add(border);
				dateBorders[date] = border;

				CheckBox checkBox = new CheckBox
				{
					HorizontalOptions = LayoutOptions.Start
				};

				Label checkBoxLabel = new Label
				{
					Text = date.ToString("MMM dd") + " - " + date.DayOfWeek.ToString(),
					VerticalTextAlignment = TextAlignment.Center
				};

				StackLayout checklistItem = new StackLayout
				{
					Orientation = StackOrientation.Horizontal,
					Children = { checkBox, checkBoxLabel }
				};

				checkBox.CheckedChanged += (s, e) => OnChecklistItemCheckedChanged(s, e, date);

				checklistLayout.Children.Add(checklistItem);
			}
		}

		private void OnChecklistItemCheckedChanged(object sender, CheckedChangedEventArgs e, DateTime date)
		{
			var viewModel = BindingContext as SharedViewModel;
			if (viewModel == null) return;

			if (dateBorders.ContainsKey(date))
			{
				var billsForDate = viewModel.Bills.Where(bill => bill.DueDate.Date == date.Date).ToList();
				if (billsForDate.Any())
				{
					dateBorders[date].BackgroundColor = e.Value ? Colors.LightGreen : Colors.LightGray;
				}
				else
				{
					dateBorders[date].BackgroundColor = Colors.Gray;
				}
			}
		}
	}
}