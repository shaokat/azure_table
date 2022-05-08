using System.Net;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Table;


namespace demo
{
    public  class CustomerService
    {
        public static async Task MergeUser(CloudTable table, CustomerEntity customer) {
            TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(customer);

            // Execute the operation.
            TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
            CustomerEntity insertedCustomer = result.Result as CustomerEntity;

            Console.WriteLine("Added user.");
        }

        public static async Task<CustomerEntity?> QueryUser(CloudTable table, string firstName, string lastName) {
            TableOperation retrieveOperation = TableOperation.Retrieve<CustomerEntity>(lastName, firstName);
            
            TableResult result = await table.ExecuteAsync(retrieveOperation);
            CustomerEntity customer = result.Result as CustomerEntity;
            
            /*if (customer != null)
            {
                Console.WriteLine("Fetched \t{0}\t{1}\t{2}\t{3}", 
                    customer.PartitionKey, customer.RowKey, customer.Email, customer.PhoneNumber);
            }
            else
            {
                Console.WriteLine("No customer found with that name");
            }
*/
            return customer;
        }

        public static async Task RemoveUser(CloudTable table, string firstName, string lastName)
        {
            var entity = new DynamicTableEntity(lastName,firstName) { ETag = "*" };
            TableOperation deleteOperation = TableOperation.Delete(entity);
            TableResult result = await table.ExecuteAsync(deleteOperation);
            
            Console.WriteLine("User deleted successfully.");
        }
        
        public static async Task<CustomerEntity> UpdateUser(CloudTable table, string firstName, string lastName, CustomerEntity customer)
        {
            var updateEntity = await QueryUser(table, firstName, lastName);
            CustomerEntity updatedUser = null;
            if (updateEntity != null)
            {
                updateEntity.Email = customer.Email;
                updateEntity.PhoneNumber = customer.PhoneNumber;
                var operation = TableOperation.InsertOrReplace(updateEntity);
                 TableResult result =  await table.ExecuteAsync(operation);
                 updatedUser = result.Result as CustomerEntity;

            }

            Console.WriteLine("user updated");
            return updatedUser;

        }
        
    }
}

