using System;

namespace BillFolio.Models
{
	public enum BillFrequency
	{
		OneTime, // Represents a one-time bill
		Monthly, // Represents a bill that occurs monthly
		Yearly // Represents a bill that occurs yearly
	}

	public enum BillType
	{
		Utility, // Represents utility bills (e.g., water, electricity)
		Subscription, // Represents subscription-based bills (e.g., streaming services)
		Other //Represents other types of bills
	}

	// Class to represent a bill

	public class Bill
	{
		public string Name { get; set; }          // Name of the bill
		public decimal Amount { get; set; }       // Amount due for the bill
		public DateTime DueDate { get; set; }     // Due date of the bill
		public BillFrequency Frequency { get; set; } // Fequency of the bill
		public BillType Type { get; set; }          // Type of bill (e.g., Utility, Subscription, etc.)
		public bool IsPaid { get; set; }            // To check if the bill is paid or not. ( Added 11/11 Johnathon)
    }
}

