using DotNet_Lab1;
using System.ComponentModel;


var myDictionary = new MyDictionary<string, string>();

myDictionary.CollectionCleared += col => Console.WriteLine("CollectionCleared");
myDictionary.CollectionCopied += (col, _)=> Console.WriteLine("CollectionCopied");
myDictionary.ElementAdded += (col, _) => Console.WriteLine("ElementAdded");
myDictionary.ElementRemoved += (col, _) => Console.WriteLine("ElementRemoved");
myDictionary.ElementChanged += (col, _, _) => Console.WriteLine("ElementChanged");

myDictionary.Add("2", "2");
PrintAllElements(".Add(\"2\", \"2\")", myDictionary);

myDictionary.Add(new KeyValuePair<string, string>("3", "3"));
PrintAllElements(".Add(new KeyValuePair<string, string>(\"3\", \"3\"))", myDictionary);

myDictionary.Insert("1", "1", 0);
PrintAllElements(".Insert(\"1\", \"1\", 0)", myDictionary);

myDictionary.Insert("5", "5", 3);
PrintAllElements(".Insert(\"5\", \"5\", 3)", myDictionary);

myDictionary.Insert("4", "4", 3);
PrintAllElements(".Insert(\"4\", \"4\", 3)", myDictionary);

Console.WriteLine($"Count - {myDictionary.Count}\n");

myDictionary.Remove("3");
PrintAllElements(".Remove(\"3\")", myDictionary);

myDictionary.Remove(new KeyValuePair<string, string>("2", "1"));
PrintAllElements(".Remove(new KeyValuePair<string, string>(\"2\", \"1\"))", myDictionary);

string? stringResult;
var boolResult = myDictionary.TryGetValue("5", out stringResult);
Console.WriteLine($".TryGetValue(\"5\", out stringResult): {boolResult}, Value = {stringResult}\n");

bool isContainsKey = myDictionary.ContainsKey("3");
Console.WriteLine($".ContainsKey(\"3\"): {isContainsKey}\n");

bool isContainsKeyValue = myDictionary.Contains(new KeyValuePair<string, string>("4", "4"));
Console.WriteLine($".ContainsKey(\"3\"): {isContainsKey}\n");

myDictionary["1"] = "2";
PrintAllElements("[\"1\"] = \"2\";", myDictionary);

Console.WriteLine($"[\"4\"]: {myDictionary["4"]}\n");

try
{
    Console.WriteLine("[\"999\"]");
    var test = myDictionary["999"];
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message + '\n');
}

try
{
    Console.WriteLine(".Add(\"2\", \"2\");");
    myDictionary.Add("2", "2");
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message + '\n');
}

myDictionary.Clear();
PrintAllElements(".Clear()", myDictionary);


void PrintAllElements(string operation, MyDictionary<string, string> myDictionary)
{
    Console.WriteLine($"{operation}:");
    foreach (var item in myDictionary)
    {
        Console.WriteLine($"{item.Key} - {item.Value}");
    }
    Console.WriteLine();
}