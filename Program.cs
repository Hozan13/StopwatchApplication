using System;
using System.Threading;

namespace StopwatchApplication
{
    internal class Program
    {
        static bool isRunning = false;
        static bool isApplicationRunning = true;
        static int seconds = 0;
        static Thread timerThread;

        static void Main(string[] args)
        {
            Console.WriteLine("Stopwatch Application");
            Console.WriteLine("Commands: 'start', 'stop', 'reset', 'exit'");
            Console.WriteLine("Elapsed Time: 0 seconds");

            timerThread = new Thread(UpdateTimer);
            timerThread.IsBackground = true;
            timerThread.Start();

            while (true)
            {
                Console.Write("Enter a command: ");
                string command = Console.ReadLine()?.ToLower();

                if (command == "start")
                {
                    if (!isRunning)
                    {
                        isRunning = true;
                    }
                    else
                    {
                        Console.WriteLine("The stopwatch is already running.");
                    }
                }
                else if (command == "stop")
                {
                    isRunning = false;
                }
                else if (command == "reset")
                {
                    isRunning = false;
                    seconds = 0;
                    UpdateElapsedTime(seconds);
                    Console.WriteLine("The stopwatch has been reset.");
                }
                else if (command == "exit")
                {
                    Console.WriteLine("Exiting...");
                    isRunning = false;
                    isApplicationRunning = false;
                    timerThread.Join();
                    Console.WriteLine("Application has exited successfully.");
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid command. Available commands: 'start', 'stop', 'reset', 'exit'.");
                }
            }
        }

        static void UpdateTimer()
        {
            while (isApplicationRunning)
            {
                if (isRunning)
                {
                    Thread.Sleep(1000);
                    seconds++;
                    UpdateElapsedTime(seconds);
                }
                else
                {
                    Thread.Sleep(200);
                }
            }
        }

        static void UpdateElapsedTime(int seconds)
        {
            if (!isApplicationRunning) return;

            int cursorLeft = Console.CursorLeft;
            int cursorTop = Console.CursorTop;

            Console.SetCursorPosition(0, 2);
            Console.Write($"Elapsed Time: {seconds} seconds   ");

            Console.SetCursorPosition(cursorLeft, cursorTop);
        }
    }
}
