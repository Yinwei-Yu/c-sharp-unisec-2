using System;

public class AlarmClock
{
    public event EventHandler Tick;

    public event EventHandler Alarm;

    private readonly int _alarmTime;

    public AlarmClock(int alarmTime)
    {
        _alarmTime = alarmTime;
    }

    public void Start()
    {
        Console.WriteLine("闹钟已启动...");
        for (int i = 1; i <= _alarmTime; i++)
        {
            System.Threading.Thread.Sleep(1000);
            OnTick();
        }
        OnAlarm();
    }

    protected virtual void OnTick()
    {
        Tick?.Invoke(this, EventArgs.Empty);
    }

    protected virtual void OnAlarm()
    {
        Alarm?.Invoke(this, EventArgs.Empty);
    }
}

internal class Program
{
    private static void Main()
    {
        var alarmClock = new AlarmClock(5); // 5秒后响铃

        alarmClock.Tick += (sender, e) =>
            Console.WriteLine($"第{DateTime.Now.Second % 60}秒 - 嘀嗒...");

        alarmClock.Alarm += (sender, e) =>
            Console.WriteLine("时间到！闹钟响铃！");

        alarmClock.Start();
    }
}