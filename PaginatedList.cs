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
