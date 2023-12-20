using System;
using System.Threading;
using NAudio.Wave;  

class TimerApp
{
    static void Main()
    {
        Console.WriteLine("Введите время в секундах для таймера:");
        int seconds;
        while (!int.TryParse(Console.ReadLine(), out seconds) || seconds < 0)
        {
            Console.WriteLine("Пожалуйста, введите корректное число секунд:");
        }

        Console.WriteLine("Таймер установлен на {0} секунд.", seconds);
        Console.WriteLine("Для паузы/возобновления нажмите 'p', для выхода - 'q'.");
        Console.WriteLine("");

        int timeLine = Console.CursorTop - 1;
        int messageLine = Console.CursorTop;
        bool isPaused = false;
        string message = "";
        
        while (seconds > 0)
        {
            if (!isPaused)
            {
                Console.SetCursorPosition(0, timeLine);
                Console.WriteLine("Оставшееся время: {0} секунд   ", seconds);
                Thread.Sleep(1000);
                seconds--;
            }

            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.P)
                {
                    isPaused = !isPaused;
                    message = isPaused ? "Таймер на паузе.                " : "Таймер возобновлен.              ";
                }
                else if (key.Key == ConsoleKey.Q)
                {
                    break;
                }
            }

            Console.SetCursorPosition(0, messageLine);
            Console.WriteLine(message);
        }

        if (seconds == 0)
        {
            Console.SetCursorPosition(0, timeLine);
            Console.WriteLine("Время вышло!                     ");
            PlaySound("C:\\Users\\Admin\\source\\repos\\timer3\\zvuk\\vzryiv-vo-vremya-boya.wav");  
        }

        Console.WriteLine("Таймер остановлен.");
    }

    private static void PlaySound(string soundFilePath)
    {
        try
        {
            using (var audioFile = new AudioFileReader(soundFilePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Не удалось воспроизвести звук: " + ex.Message);
        }
    }
}