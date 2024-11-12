using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using BillFolio.Models;
using BillFolio.ViewModel;

namespace BillFolio
{
    public partial class TwoWeekView : ContentPage
    {
        private Dictionary<DateTime, Border> dateBorders = new Dictionary<DateTime, Border>();

        public TwoWeekView()
        {
            InitializeComponent();
            PopulateCalendar();
        }

        private void PopulateCalendar()
        {
            calendarGrid.Children.Clear();
            checklistLayout.Children.Clear();
            dateBorders.Clear();

            DateTime today = DateTime.Today;
            int daysUntilSunday = (int)today.DayOfWeek;
            DateTime startDate = today.AddDays(-daysUntilSunday);

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

                Border border = new Border
                {
                    Content = dayLabel,
                    Stroke = Colors.Gray,
                    StrokeThickness = 1,
                    BackgroundColor = Colors.LightGray,
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
                    Text = date.ToString("MMM dd"),
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
            //Comment out lines 90-93, and un-comment out lines 96-118 to try to work on/with other code.
            if (dateBorders.ContainsKey(date))
            {
                dateBorders[date].BackgroundColor = e.Value ? Colors.LightGreen : Colors.LightGray;
            }
            
            
            /*
            // Assuming `Bills` is a collection of Bill objects in your class  
            var billsForDate = Bills.Where(Bill => Bill.DueDate.Date == date.Date).ToList();  // Find bills that are due on the selected date

            if (billsForDate.Any())  // Check if there are any bills for the given date
            {
                // If there are bills for the selected date, change the background color based on the checkbox value (checked/unchecked)
                if (dateBorders.ContainsKey(date))  // Ensure that the date exists in the dictionary of borders
                {
                    // If the checkbox is checked, use LightGreen (indicating the date has bills that need attention)
                    // If unchecked, use LightGray (indicating the date doesn't have bills or user unchecks)
                    dateBorders[date].BackgroundColor = e.Value ? Colors.LightGreen : Colors.LightGray;
                }
            }
            else
            {
                // If no bills are due on the selected date, set the background color to neutral Gray
                if (dateBorders.ContainsKey(date))
                {
                    dateBorders[date].BackgroundColor = Colors.Gray;  // No bills on this date, neutral color
                }
            }
           */
        }
    }
}