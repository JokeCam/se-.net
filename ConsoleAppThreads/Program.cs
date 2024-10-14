// See https://aka.ms/new-console-template for more information

using System.Diagnostics.Metrics;
using System;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.VisualBasic;


List<Task> tasks = new List<Task>(50);

for (int i = 1; i < 11; i++)
{
    tasks.Add(
        Task.Factory.StartNew(() =>
            {
                Worker.DoWorkNoSync("NO SYNC");
            })
    );
}

Task.WaitAll(tasks.ToArray());

for (int i = 1; i < 11; i++)
{
    tasks.Add(
        Task.Factory.StartNew(() =>
        {
            Worker.DoWorkLockSync("LOCK SYNC");
        })
    );
}

Task.WaitAll(tasks.ToArray());

for (int i = 1; i < 11; i++)
{
    tasks.Add(
        Task.Factory.StartNew(() =>
        {
            Worker.DoWorkAutoResetEventSync("AutoResetEvent SYNC");
        })
    );
}

Task.WaitAll(tasks.ToArray());

public class Worker
{
    private static Random _random = new Random();
    private static int _counter = 0;
    static object locker = new();
    static AutoResetEvent waitHandler = new AutoResetEvent(true);
    public static void DoWorkNoSync(object msg)
    {
        Thread.Sleep(_random.Next(1, 4) * 300);
        for (var i = 0; i < 3; i++)
        {
            Console.WriteLine($"Message: {msg}; Counter: {_counter}; Inner iteration number: {i}");
        }
        _counter++;
    }

    public static void DoWorkLockSync(object msg)
    {
        lock (locker)
        {
            Thread.Sleep(_random.Next(1, 4) * 300);
            for (var i = 0; i < 3; i++)
            {
                Console.WriteLine($"Message: {msg}; Counter: {_counter}; Inner iteration number: {i}");
            }
            _counter++;
        }
    }

    public static void DoWorkAutoResetEventSync(object msg)
    {
        waitHandler.WaitOne();
        Thread.Sleep(_random.Next(1, 4) * 300);
        for (var i = 0; i < 3; i++)
        {
            Console.WriteLine($"Message: {msg}; Counter: {_counter}; Inner iteration number: {i}");
        }
        _counter++;
        waitHandler.Set();
    }
}


