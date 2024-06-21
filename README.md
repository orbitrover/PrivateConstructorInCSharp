# Private Constructor In C#
Private Constructor in C# with example

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
            // PaginatedList<Employee> pagination = new PaginatedList<Employee>(employees.AsQueryable(), 2, 5);
            // However, we can use this class as a static class if it contains any static methods.
            var pages = await PaginatedList<Employee>.CreateAsync(employees.AsQueryable(), 1, 5);
            
            foreach (var employee in pages)
                Console.WriteLine($"Id: {employee.Id} & Name: {employee.Name}");
        }
    }
    
    public class Employee
    {
    	public int Id { get; set; }
    	public string Name { get; set; }
    	public Employee()
    	{
    	}
    	public Employee(int id, string name)
    	{
    		Id = id;
    		Name = name;
    	}
    }

    public class PaginatedList<T> : List<T>
    {
      private PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize = 20)
      {
          PageIndex = pageIndex;
          TotalPages = (int)Math.Ceiling(count / (double)pageSize);
          this.AddRange(items);
      }
      
      public bool HasPreviousPage => PageIndex > 1;
      public bool HasNextPage => PageIndex < TotalPages;
      public int PageIndex { get; private set; }
      public int TotalPages { get; private set; }
      
      /// <summary>
      /// Creates a PaginatedList constructor privately and initiates the property values.
      /// </summary>
      public static async Task<PaginatedList<T>> CreateAsync(
          IQueryable<T> source, int pageIndex, int pageSize = 20)
      {
          // Accepts source as IQueryable to take rows from the database.
          var count = await source.CountAsync();
          var items = await source.Skip(
              (pageIndex - 1) * pageSize)
              .Take(pageSize).ToListAsync();
          // Returns an object of PaginatedList that contains entities as Enumerable and page count, index, and size.
          return new PaginatedList<T>(items, count, pageIndex, pageSize);
      }
      
      /// <summary>
      /// Creates a PaginatedList constructor privately and initiates the property values.
      /// </summary>
      public static async Task<PaginatedList<T>> CreateAsync(
          IEnumerable<T> source, int pageIndex, int pageSize = 20)
      {
          // Accepts source as IEnumerable to take rows from in-memory data.
          var count = source.Count();
          var items = source.Skip(
              (pageIndex - 1) * pageSize)
              .Take(pageSize).ToList();
          // Returns an object of PaginatedList that contains entities as Enumerable and page count, index, and size.
          return new PaginatedList<T>(items, count, pageIndex, pageSize);
      }
    }
