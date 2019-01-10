using System;
using System.IO;
using Hl7.Fhir.Model;
using Hl7.Fhir.Rest;
using Hl7.Fhir.Serialization;

namespace TransactionClient
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var endpoint = new Uri("http://localhost/Spark/fhir");
            var client = new FhirClient(endpoint);
            string json = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "transaction.json"));
            var bundle = new FhirJsonParser().Parse<Bundle>(json);

            var resultBundle = client.Transaction(bundle);
            Console.ReadLine();
        }
    }
}