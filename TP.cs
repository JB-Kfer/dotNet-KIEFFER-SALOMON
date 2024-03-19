using System;
using Newtonsoft.Json;

public class Person
{
    public string Nom { get; set; }
    public int Age { get; set; }

    public Person(string nom, int age)
    {
        Nom = nom;
        Age = age;
    }
}

public class TP
{
    public static void tp()
    {
        Person person = new Person("Alice", 30);

        // Serializing the person object into a JSON string with indented formatting
        string json = JsonConvert.SerializeObject(person, Formatting.Indented);
        Console.WriteLine(json);
    }
}
