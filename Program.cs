public class Program
{
    public static Semaphore? Semaphore;
    public static int sem1 = 1;
    static void Menu()
    {
        Console.WriteLine(" 0. Exit");
        Console.WriteLine(" 1. Create new thread");
        Console.WriteLine(" 2. Click Thread (Created)");
        Console.WriteLine(" 3. Click Thread (Waiting)");
        Console.WriteLine(" 4. Show All Operation");
    }
    static void Main(string[] args)
    {
        List<Thread> CreatedThreads = new List<Thread>();
        List<Thread> WaitingThreads = new List<Thread>();
        Semaphore = new Semaphore(sem1, 4);
        int choice = -2;
        while (choice != 0)
        {
            Menu();
            Console.Write("Enter the choice: ");
            choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    {
                        Thread thread = new Thread(() =>
                        {
                            Thread.Sleep(300);
                            Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} working...");
                        });
                        CreatedThreads.Add(thread);
                        thread.Start();
                        Thread.Sleep(1000);
                        break;
                    }
                case 2:
                    {
                        List<Thread> threadsCopy = new List<Thread>(CreatedThreads);
                        foreach (Thread thread in CreatedThreads)
                        {
                            Console.WriteLine($"Thread {thread.ManagedThreadId}");
                        }
                        Console.Write("Select Thread (Only number enter): ");
                        int selected_thred = Convert.ToInt32(Console.ReadLine());
                        foreach (Thread thread in threadsCopy)
                        {
                            int currentThreadId = thread.ManagedThreadId;
                            if (selected_thred == currentThreadId)
                            {
                                CreatedThreads.Remove(thread);
                                WaitingThreads.Add(thread);
                                Console.WriteLine("\n                 Waiting...");
                                Thread.Sleep(500);
                                break;
                            }
                            currentThreadId = 0;
                        }

                        break;
                    }
                case 3:
                    {
                        List<Thread> threadsCopy2 = new List<Thread>(WaitingThreads);

                        foreach (Thread thread in WaitingThreads)
                        {
                            Console.WriteLine($"Thread {thread.ManagedThreadId}");
                        }
                        Console.Write("Select Thread (Only number enter): ");
                        int selected_thred = Convert.ToInt32(Console.ReadLine());
                        foreach (Thread thread in threadsCopy2)
                        {
                            int currentThreadId = thread.ManagedThreadId;
                            if (selected_thred == currentThreadId)
                            {
                                WaitingThreads.Remove(thread);
                                ReleaseSemaphore();
                                break;
                            }
                            currentThreadId = 0;
                        }

                        break;
                    }
                case 4:
                    {

                        Console.WriteLine("Created Threads");

                        foreach (Thread thread in CreatedThreads)
                        {
                            Console.WriteLine($"Thread {thread.ManagedThreadId}");
                            Thread.Sleep(200);
                        }
                        Console.WriteLine("------------------------\n");
                        Console.WriteLine("Waiting Threads");
                        foreach (Thread thread in WaitingThreads)
                        {
                            Console.WriteLine($"Thread {thread.ManagedThreadId}");
                            Thread.Sleep(200);
                        }
                        Console.WriteLine("------------------------");
                        break;
                    }
            }
        }

    }

    static void ReleaseSemaphore()
    {
        if (sem1 < 4)
        {
            Semaphore.Release();
            sem1++;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Semaphore worked");
            Console.ResetColor();
        }
    }
}