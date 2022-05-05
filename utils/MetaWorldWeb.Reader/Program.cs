using MetaWorldWeb;
using System.Text.Json;

Console.WriteLine("Meta World Web Reader utility");
Console.WriteLine("-----------------------------");
Console.WriteLine();
Console.WriteLine("Use this utility to read and dump data interpreted by the Mtea World Web library.");

var uri = string.Empty;
var hashes = 0;
var clientIdentier = string.Empty;

if (args.Length > 0)
{
    uri = args[0];
}
if (args.Length > 1)
{
    hashes = int.Parse(args[1]);
}
if (args.Length > 2)
{
    clientIdentier = args[2];
}

if (string.IsNullOrWhiteSpace(uri))
{
    Console.Write("URL to visit: ");
    uri = Console.ReadLine();
}
if (hashes == 0)
{
    Console.Write("How many hashes to generate [1]: ");
    var hashesString = Console.ReadLine();
    if (!int.TryParse(hashesString, out hashes))
    {
        hashes = 1;
    }
}
if (string.IsNullOrWhiteSpace(clientIdentier))
{
    Console.Write("Optional client identifier to use: ");
    clientIdentier = Console.ReadLine();
}

if (string.IsNullOrWhiteSpace(uri))
{
    return;
}

var manager = MetaWorldWebFactory.CreateManager();
var result = await manager.GetMetaWorldDataAsync(uri, clientIdentier, hashes);

var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
var json = JsonSerializer.Serialize(result, jsonOptions);

Console.WriteLine(json);