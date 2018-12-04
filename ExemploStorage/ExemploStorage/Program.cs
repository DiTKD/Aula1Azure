using System;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
using StackExchange.Redis;

namespace ExemploStorage
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue();


            Console.Read();

            Console.WriteLine("Hello World!");
        }

        public static void CriarBlob()
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

        public static void CriarTable()
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
        }

        public static void Redis()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("trezenetredis.redis.cache.windows.net:6380,password=ftXkXFGfHf9Pf4sIJ8LVIgSQTAmU7I38nnRf96qmipE=,ssl=True,abortConnect=False");
            var db = redis.GetDatabase();

            Console.WriteLine("Digite Seu RM");
            var rm = Console.ReadLine();

            Console.WriteLine("Digite Seu Nome");
            var nome = Console.ReadLine();


            db.StringSet(rm, nome);

            Console.WriteLine($"chave: {rm}, valor: {db.StringGet(rm)}");

            Console.Read();
        }

        public static void Chart()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("trezenetredis.redis.cache.windows.net:6380,password=ftXkXFGfHf9Pf4sIJ8LVIgSQTAmU7I38nnRf96qmipE=,ssl=True,abortConnect=False");
            var db = redis.GetDatabase();

            Console.WriteLine("Digite Seu Nome");
            var nome = Console.ReadLine();
            db.Publish("13net", $"{nome} entrou na sala");

            var pubsub = redis.GetSubscriber();
            pubsub.Subscribe("13net", (chanel, message) => 
                Console.WriteLine(message.ToString()));
            while(true)
            {
                var msg = Console.ReadLine();
                db.Publish("13net", msg);
            }

            
            Console.Read();
        }

        public static void Queue()
        {
            var connection = "DefaultEndpointsProtocol=https;AccountName=trezenet;AccountKey=AXwUsljgM169Q3c9IvcunCdagOXypuVtbaSs/mMmPCrPuMADu9rW7BYeEAE/B5Qm5v9sM956wpbBjpJYoj/8UQ==;EndpointSuffix=core.windows.net";

            CloudStorageAccount account = CloudStorageAccount.Parse(connection);
            var queueClient = account.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("rm331835_");
            queue.CreateIfNotExistsAsync().Wait();

            Console.WriteLine("Digite algo");
            var msg = Console.ReadLine();

            queue.AddMessageAsync(new Microsoft.WindowsAzure.Storage.Queue.CloudQueueMessage(msg), new TimeSpan(1000), null, null, null);


            var messages = queue.GetMessagesAsync(10).Result;
            foreach (var m in messages)
            {
                Console.WriteLine($"Mensagem: {m.AsString}");
                queue.DeleteMessageAsync(m);
            }

            Console.ReadLine();
        }
    }
}
