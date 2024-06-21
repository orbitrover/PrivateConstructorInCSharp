using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main()
    {
        List<Employee> employees = new List<Employee>()
        {
            new Employee(1, "Orbit Rover"),
            new Employee(2, "Nion Tesla"),
            new Employee(3, "Riky Rat"),
            new Employee(4, "Famous Fox"),
            new Employee(5, "Rare Rabbit"),
            new Employee(6, "Tough Tiger"),
            new Employee(7, "Loud Lion"),
            new Employee(8, "Ginni Giraffe"),
            new Employee(9, "Happy Hippo"),
        };
        
        // Here we cannot create an instance of a class that has a private constructor.
        // If you uncomment the following statement, it will generate
        // an error because the constructor is inaccessible:
        // PaginatedList<Employee> pagination = new PaginatedList<Employee>(employees.AsQueryable(), 2, 5); // Error 
        
        // However, we can use this class as a static class if it contains any static methods.
        var pages = await PaginatedList<Employee>.CreateAsync(employees.AsQueryable(), 1, 5);
        
        foreach (var employee in pages)
            Console.WriteLine($"Id: {employee.Id} & Name: {employee.Name}");
    }
}
