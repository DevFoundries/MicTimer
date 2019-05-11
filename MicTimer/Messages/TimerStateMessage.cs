namespace MicTimer.Messages
{
    public enum TimerState
    {
        Normal,
        Warn,
        Alert
    }

    public class TimerStateMessage
    {
        public TimerState TimerState { get; set; } = TimerState.Normal;
    }
}