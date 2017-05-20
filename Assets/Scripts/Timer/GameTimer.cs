using System.Timers;

public class GameTimer
{
    Timer timer;
    public bool timerPassed { get; set; }

    public void TimerElapsed(object source, ElapsedEventArgs e)
    {
        timer.Stop();
        timerPassed = true;
    }

    public void ResetAndStartTimer(float intervalSeconds)
    {
        ResetTimerNew(intervalSeconds);
        StartTimer();
    }

    public void StartTimer()
    {
        timer.Start();
    }

    public void ResetTimerNew(float intervalSeconds)
    {
        timer = new Timer();
        timer.Interval = intervalSeconds * 1000f;
        timer.Elapsed += new ElapsedEventHandler(TimerElapsed);
        timerPassed = false;
    }
}
