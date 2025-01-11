//using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing.Matching;
using Microsoft.AspNetCore.Mvc;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("allowAll", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();

    }

    );
}
);

builder.Services.AddAntiforgery();
var app = builder.Build();




//apply cors
app.UseCors("allowAll");


//create a list for demo 
List<Person> persons = new List<Person>();

app.MapGet("/", () => "Hello World!");


//get form first 

app.MapPost("/getform", ([FromForm] string name, [FromForm] string email, [FromForm] int age) =>
{

    Person person = new Person()
    {
        Name = name,
        Email = email,
        Age = age
    };

    Console.WriteLine("person created");
    //add to list 
    persons.Add(person);



    return Results.Ok(new
    {
        Name = name,
        Email = email,
        Age = age
    });
}).DisableAntiforgery();

app.MapGet("/showall", () =>
{
    var allRecords = persons;

    return Results.Ok(allRecords);

});

app.Run();


public class Person
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";

    public int Age { get; set; } = 0;

    public Person() { }
}
