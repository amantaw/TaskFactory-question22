using System;
using System.Threading.Tasks;

public class Program
{
    // Step 1: Method which returns a random integer between 1 to 10000 using Func and Lambda expression
    public static Func<int> GetRandomNumber = () => new Random().Next(1, 10001);

    // Step 2: Method which takes Func<int> and returns a string
    public static Func<Func<int>, string> GenerateNumberString = (randomNumberFunc) =>
    {
        int number = randomNumberFunc();
        return $"The Generated Number is: {number}";
    };

    public static void Main()
    {
        // Step 3: Task Factory to chain the methods using ContinueWith
        Task.Factory.StartNew(() =>
        {
            // Step 4: First Task: Generate Random Number
            return GetRandomNumber();
        })
        .ContinueWith(previousTask =>
        {
            // Step 5: Second Task: Generate the output string from the first task's result
            return GenerateNumberString(() => previousTask.Result);
        })
        .ContinueWith(finalTask =>
        {
            // Step 5: Print the final result
            Console.WriteLine(finalTask.Result);
        });

        // Ensure the program waits for tasks to complete before exiting
        Console.ReadLine();
    }
}

