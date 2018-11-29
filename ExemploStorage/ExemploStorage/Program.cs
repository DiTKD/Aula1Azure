using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;

namespace ExemploStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            //CloudStorageAccount account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storage13net;AccountKey=AhDlxuw3BZ3XhhFvgJUNmvVDBoiUZFXwrZoty/hLCiXdFzR2B/wXVnzlbS/dnRIt7oC/jn8mcvNYoaDluvercg==;EndpointSuffix=core.windows.net");
            CloudStorageAccount account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storagediogo13;AccountKey=SBrsPwCIUU8Oauhb9AvE8K1Fc4qsj8J5pFOHZABaxo9zVcD+VQLVwN7eqovA8RTzrzHejpEw5zBLqHr9JxeMVQ==;EndpointSuffix=core.windows.net");
            var tableClient = account.CreateCloudTableClient();
            var table = tableClient.GetTableReference("rm331835");
            table.CreateIfNotExistsAsync().Wait();

            var entity = new Aluno("33183531", "Taboão da Serra");
            entity.Nome = "Diogo";
            entity.Email = "DiogoCrisostomos@gmail.com";


            table.ExecuteAsync(TableOperation.Insert(entity));

            Console.WriteLine($"{table.Uri}");

            Console.Read();

            Console.WriteLine("Hello World!");
        }

        public void CriarBlob()
        {
            //CloudStorageAccount account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storage13net;AccountKey=AhDlxuw3BZ3XhhFvgJUNmvVDBoiUZFXwrZoty/hLCiXdFzR2B/wXVnzlbS/dnRIt7oC/jn8mcvNYoaDluvercg==;EndpointSuffix=core.windows.net");
            CloudStorageAccount account = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=storagediogo13;AccountKey=SBrsPwCIUU8Oauhb9AvE8K1Fc4qsj8J5pFOHZABaxo9zVcD+VQLVwN7eqovA8RTzrzHejpEw5zBLqHr9JxeMVQ==;EndpointSuffix=core.windows.net");
            var blobClient = account.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference("331835");
            container.CreateIfNotExistsAsync().Wait();
            var blob = container.GetBlockBlobReference("diogo3.txt");
            blob.UploadTextAsync("Aula Azure Meu blob");
            var sas = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write,
                SharedAccessExpiryTime = DateTime.Now.AddYears(5)
            });


            Console.WriteLine($"{blob.Uri}{sas}");

            Console.Read();

            Console.WriteLine("Hello World!");
        }
    }
}
