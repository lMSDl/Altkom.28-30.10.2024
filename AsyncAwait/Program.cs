
namespace AsyncAwait;

public class Program
{
    public static async Task Main()
    {
        var task1 = AlphabetAsync();
        var task2 = NumbersAsync();
        List<Task> tasks = [task1, task2];

        while(tasks.Count != 0)
        {
            var task =  await Task.WhenAny(tasks);
            Console.WriteLine($"Zakończono dla {task.Id}");
            tasks.Remove(task);
        }
    }



static Task AlphabetAsync()
{
     return Task.Run(() => Alphabet());
}

static  Task NumbersAsync()
{
    return Task.Run(() => Numbers());
}


static void Numbers()
{
    for (int i = 0; i < 1000; i++)
    {
        Console.WriteLine(i);
    }
}

static void Alphabet()
{
    for (int i = 0; i < 100; i++)
    {
        for (char j = 'a'; j <= 'z'; j++)
        {
            Console.WriteLine($"{i}{j}");
        }
    }
}

}