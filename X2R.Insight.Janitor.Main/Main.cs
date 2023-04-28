using X2R.Insight.Janitor.Logic;

var logic = new Logic();
var cnn = logic.databaseConnection();

if (cnn == true)
{
    Console.WriteLine("Connecting established");
    Console.WriteLine("----------------------");
    logic.UseExecuteTimes(out var query);
}
else
{
    Console.WriteLine("Failed to connect to the database");
}

Console.ReadLine();






