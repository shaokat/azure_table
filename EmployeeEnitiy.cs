using Microsoft.Azure.Cosmos.Table;


namespace demo;

public class EmployeeEnitiy: TableEntity
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public EmployeeEnitiy(string deptName, string empName)
    {
        PartitionKey = deptName;
        RowKey = empName;
    }

    public EmployeeEnitiy()
    {
       
    }
}