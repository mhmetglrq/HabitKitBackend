public class HabitCompletion : BaseEntity
{
    public Guid HabitId { get; private set; }

    public DateOnly Date { get; private set; }

    private HabitCompletion() { }

    internal HabitCompletion(Guid habitId, DateOnly date)
    {
        HabitId = habitId;
        Date = date;
    }
}