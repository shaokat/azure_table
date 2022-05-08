using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;
using WindowsAzure.Table.Extensions;

namespace demo;

public static class EmployeeService
{
    public static async Task MergeEmployee(CloudTable table, EmployeeEnitiy[] customers)
    {
        // //each operation entity must have same partition key.
        TableBatchOperation batchOperation = new();
        foreach (var customerEntity in customers) 
            batchOperation.InsertOrMerge(customerEntity);

        // Execute the operation.
        TableBatchResult result = await table.ExecuteBatchAsync(batchOperation);

        Console.WriteLine("Added Employee.");
    }

    public static async Task getEmployees(CloudTable table, string partitionKey)
    {
        TableQuery<EmployeeEnitiy> query = new TableQuery<EmployeeEnitiy>().Where(
            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey));
                
        var emps = table.ExecuteQuery(query);
        
        foreach (EmployeeEnitiy  emp in emps)
        {
                Console.WriteLine("Item: {0} as {1} items in stock", emp.Email, emp.PhoneNumber);
        }
        
    }
}