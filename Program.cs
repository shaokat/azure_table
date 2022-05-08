
using Microsoft.Azure.Cosmos.Table;
using static demo.CustomerService;

namespace demo
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Table storage sample");

            var storageConnectionString = " ";
            var tableName = "employee";

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
            CloudTable table = tableClient.GetTableReference(tableName);


            CustomerEntity customer = new CustomerEntity("Musk", "Elon")
            {
                Email = "em@tesla.com",
                PhoneNumber = "1234567"
            };
           
            EmployeeEnitiy[] emp =
            {
                new EmployeeEnitiy("Training", "Emp1") { Email = "emp1@gmail.com", PhoneNumber = "111224" },
                new EmployeeEnitiy("Training", "Emp2") { Email = "emp2@gmail.com", PhoneNumber = "222222" },
            };
            
            //CustomerService.MergeUser(table, customer).Wait();

            //QueryUser(table,"Elon","Musk").Wait();

            // UpdateUser(table,"Elon","Musk",customer).Wait();

            EmployeeService.getEmployees(table, "Training").Wait();

        }

       

    }
}
  
