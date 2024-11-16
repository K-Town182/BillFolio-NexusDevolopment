using SQLite;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Storage;
using BillFolio.Models;

public class DatabaseHelper
{
    private static SQLiteConnection db;

    // Method to initialize the database connection
    public static void Initialize()
    {
        db = new SQLiteConnection(Constants.DatabasePath);
        db.CreateTable<Bill>();
    }

    // Private method to ensure db is initialized
    private static void EnsureInitialized()
    {
        if (db == null)
        {
            throw new InvalidOperationException("DatabaseHelper.Initialize() must be called before using the database.");
        }
    }

    // Method to add a new bill
    public static void AddBill(Bill bill)
    {
        EnsureInitialized();
        db.Insert(bill);
    }

    // Method to retrieve a bill by ID
    public static Bill GetBill(int id)
    {
        EnsureInitialized();
        return db.Find<Bill>(id);
    }

    // Method to delete a bill by ID
    public static void DeleteBill(int id)
    {
        EnsureInitialized();
        db.Delete<Bill>(id);
    }

    // Method to update an existing bill
    public static void UpdateBill(Bill bill)
    {
        EnsureInitialized();
        db.Update(bill);
    }

    // Method to retrieve all bills
    public static List<Bill> GetAllBills()
    {
        EnsureInitialized();
        return db.Table<Bill>().ToList();
    }

    public static List<Bill> GetBillsForTwoWeeks(DateTime startDate, DateTime endDate)
    {
        EnsureInitialized(); // Ensure the database is properly initialized
        return db.Table<Bill>().Where(bill => bill.DueDate >= startDate && bill.DueDate <= endDate).ToList();
    }

}
