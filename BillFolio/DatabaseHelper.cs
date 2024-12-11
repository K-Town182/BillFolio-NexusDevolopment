﻿using SQLite;
using System.Collections.Generic;
using System.IO;
using Microsoft.Maui.Storage;
using BillFolio.Models;

public class DatabaseHelper
{
	private static SQLiteAsyncConnection db;

	// Method to initialize the database connection
	public static async Task InitializeAsync()
	{
		var dbPath = Path.Combine(FileSystem.AppDataDirectory, "bills.db");
		db = new SQLiteAsyncConnection(dbPath);
		await db.CreateTableAsync<Bill>();
	}

	// Private method to ensure db is initialized
	private static void EnsureInitialized()
	{
		if (db == null)
		{
			throw new InvalidOperationException("DatabaseHelper.InitializeAsync() must be called before using the database.");
		}
	}

	// Method to add a new bill
	public static Task<Bill> GetBillAsync(int id)
	{
		EnsureInitialized();
		return db.FindAsync<Bill>(id);
	}

	// Method to delete a bill by ID
	public static Task<int> DeleteBillAsync(int id)
	{
		EnsureInitialized();
		return db.DeleteAsync<Bill>(id);
	}

	// Method to update an existing bill
	public static Task<int> UpdateBillAsync(Bill bill)
	{
		EnsureInitialized();
		return db.UpdateAsync(bill);
	}
	//mehtod to save a bill
	public static Task<int> SaveBillAsync(Bill bill)
	{
		EnsureInitialized();
		if (bill.Id != 0)
		{
			return db.UpdateAsync(bill);
		}
		else
		{
			return db.InsertAsync(bill);
		}
	}

	// Method to retrieve all bills
	public static Task<List<Bill>> GetAllBillsAsync()
	{
		EnsureInitialized();
		return db.Table<Bill>().ToListAsync();
	}


	public static Task<List<Bill>> GetBillsForTwoWeeksAsync(DateTime startDate, DateTime endDate)
	{
		EnsureInitialized(); // Ensure the database is properly initialized
		return db.Table<Bill>().Where(bill => bill.DueDate >= startDate && bill.DueDate <= endDate).ToListAsync();
	}
}