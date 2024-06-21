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
