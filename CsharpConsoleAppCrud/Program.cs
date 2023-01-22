using System;
using Npgsql;

namespace CsharpConsoleAppCrud
{
    internal class Program
    {
        static void Main(string[] args)
        {


            // Connection String
            string connString = "Host=localhost;Username=postgres;Password=root;Database=CsharpConsoleAppCrud";

            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                while (true)
                {
                    Console.WriteLine("1. Insert a new student");
                    Console.WriteLine("2. View all students");
                    Console.WriteLine("3. Update a student's grade");
                    Console.WriteLine("4. Delete a student");
                    Console.WriteLine("5. Exit");
                    Console.Write("Enter your choice: ");

                    int choice = Convert.ToInt32(Console.ReadLine());

                    if (choice == 1)
                    {
                        Console.Write("Enter name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter age: ");
                        int age = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter grade: ");
                        string grade = Console.ReadLine();
                        Console.Write("Enter address: ");
                        string address = Console.ReadLine();

                        var insertCommand = new NpgsqlCommand("INSERT INTO students (name, age, grade, address) VALUES (@name, @age, @grade, @address)", conn);
                        insertCommand.Parameters.AddWithValue("name", name);
                        insertCommand.Parameters.AddWithValue("age", age);
                        insertCommand.Parameters.AddWithValue("grade", grade);
                        insertCommand.Parameters.AddWithValue("address", address);
                        insertCommand.ExecuteNonQuery();
                        Console.WriteLine("Student added successfully!");
                    }
                    else if (choice == 2)
                    {
                        var selectCommand = new NpgsqlCommand("SELECT * FROM students", conn);
                        using (var reader = selectCommand.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Console.WriteLine("ID: " + reader["id"] + ", Name: " + reader["name"] + ", Age: " + reader["age"] + ", Grade: " + reader["grade"] + ", Address: " + reader["address"]);
                            }
                        }
                    }
                    else if (choice == 3)
                    {
                        Console.Write("Enter student name: ");
                        string name = Console.ReadLine();
                        Console.Write("Enter new grade: ");
                        string newGrade = Console.ReadLine();

                        var updateCommand = new NpgsqlCommand("UPDATE students SET grade = @newGrade WHERE name = @name", conn);
                        updateCommand.Parameters.AddWithValue("newGrade", newGrade);
                        updateCommand.Parameters.AddWithValue("name", name);
                        updateCommand.ExecuteNonQuery();
                        Console.WriteLine("Student grade updated successfully!");
                    }
                    else if (choice == 4)
                    {
                        Console.Write("Enter student name: ");
                        string name = Console.ReadLine();

                        var deleteCommand = new NpgsqlCommand("DELETE FROM students WHERE name = @name", conn);
                        deleteCommand.Parameters.AddWithValue("name", name);
                        deleteCommand.ExecuteNonQuery();
                        Console.WriteLine("Student deleted successfully!");
                    }
                    else if (choice == 5)
                    {
                        break;
                    }
                }
            }
        }
    }
}
