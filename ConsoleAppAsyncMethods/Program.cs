// See https://aka.ms/new-console-template for more information

await FibAsync();

async Task FibAsync() {
    await Task.Run(() => Fib(5));
    await Task.Run(() => Fib(6));
    await Task.Run(() => Fib(7));
}

int Fib(int x) {
    if (x == 0)
    {
        return 0;
    };
    
    int prev = 0;
    int next = 1;
    for (int i = 1; i < x; i++)
    {
        int sum = prev + next;
        prev = next;
        next = sum;
    }
    
    Console.WriteLine($"Fibonacci number in position {x} is {next}");
    return next;
}