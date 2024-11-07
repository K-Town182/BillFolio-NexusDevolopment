using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;

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
            if (dateBorders.ContainsKey(date))
            {
                dateBorders[date].BackgroundColor = e.Value ? Colors.LightGreen : Colors.LightGray;
            }
        }
    }
}