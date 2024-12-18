// See https://aka.ms/new-console-template for more information

using System.Diagnostics.Metrics;
using System;
using System.Threading;
using System.Xml.Serialization;
using Microsoft.VisualBasic;


List<Task> tasks = new List<Task>(50);
List<Task> noSyncParrallelTasks = new List<Task>(50);
List<Task> lockSyncParrallelTasks = new List<Task>(50);
List<Task> autoResetEventSyncParrallelTasks = new List<Task>(50);


for (int i = 1; i < 11; i++)
{
    tasks.Add(
        Task.Factory.StartNew(() =>
            {
                Worker.DoWorkNoSync("NO SYNC");
            })
    );
    
    noSyncParrallelTasks.Add(
        new Task(() =>
        {
            Worker.DoWorkNoSync("PARALLEL NO SYNC");   
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
    
    lockSyncParrallelTasks.Add(
        new Task(() =>
        {
            Worker.DoWorkLockSync("PARALLEL LOCK SYNC");   
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
    
    autoResetEventSyncParrallelTasks.Add(
        new Task(() =>
        {
            Worker.DoWorkAutoResetEventSync("PARALLEL AutoResetEvent SYNC");   
        })
    );
}

Task.WaitAll(tasks.ToArray());
Console.WriteLine("All tasks are done, starting parallel tasks");
Thread.Sleep(500);
await RunParrallelTasks();

async Task RunParrallelTasks()
{
    noSyncParrallelTasks.ForEach(t => t.Start());
    lockSyncParrallelTasks.ForEach(t => t.Start());
    autoResetEventSyncParrallelTasks.ForEach(t => t.Start());
    await Task.WhenAll(noSyncParrallelTasks);
    await Task.WhenAll(lockSyncParrallelTasks);
    await Task.WhenAll(autoResetEventSyncParrallelTasks);
}


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


