namespace PortalSystem.Client.Shared
{

    // Enum for GradeLevel
    public enum GradeLevel
    {
        Kindergarten,
        FirstGrade,
        SecondGrade,
        ThirdGrade,
        FourthGrade,
        FifthGrade,
        SixthGrade,
        SeventhGrade,
        EighthGrade,
        NinthGrade,
        TenthGrade,
        EleventhGrade,
        TwelfthGrade
    }

    // Class model
    public class ClassModel
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Timing Timing { get; set; } = new Timing(); // Initialize Timing object
        public GradeLevel GradeLevelEnum { get; set; }
        public int MaxClassSize { get; set; }
    }

    // Class for Timing
    public class Timing
    {
        public int[] DayOfWeek { get; set; } = new int[0]; // Initialize as an empty array
        public TimeInHour StartTime { get; set; } = new TimeInHour(); // Initialize StartTime
        public TimeInHour EndTime { get; set; } = new TimeInHour(); // Initialize EndTime
    }

    // Class for TimeInHour
    public class TimeInHour
    {
        public int Hour { get; set; }
        public int Minute { get; set; }
        public int Sec { get; set; }
    }
}
