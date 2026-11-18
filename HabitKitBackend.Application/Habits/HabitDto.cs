
public sealed record HabitDto(
    Guid Id,
    string Name,
    string? Description,
    string Icon,
    string ColorHex,
    bool IsArchived,
    DateTime CreatedAt);

public static class HabitMappings
{
    public static HabitDto ToDto(this Habit habit) => new(
        habit.Id,
        habit.Name,
        habit.Description,
        habit.Icon,
        habit.ColorHex,
        habit.IsArchived,
        habit.CreatedAt);
}