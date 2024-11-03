namespace BillFolio.Models
{
	public class Bill
	{
		public string Name { get; set; }          // Name of the bill
		public decimal Amount { get; set; }       // Amount due for the bill
		public DateTime DueDate { get; set; }     // Due date of the bill
		public bool IsRecurring { get; set; }     // Whether the bill is recurring
		public string Type { get; set; }          // Type of bill (e.g., Utility, Subscription, etc.)
	}
}

