namespace CustomerBackend
{
    public enum RepeatPeriod { None, Daily, Weekly, Monthly, Yearly }
    public class TimePeriod
    {
        public DateTime Start { get; set; }
        public TimeSpan Duration { get; set; }
        public RepeatPeriod Repetition { get; set; }
        
        public TimePeriod(DateTime startDate, TimeSpan duration, RepeatPeriod repetition)
        {
            Start = startDate;
            Duration = duration;
            Repetition = repetition;
        }
    }
}
