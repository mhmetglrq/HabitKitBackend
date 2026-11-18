public sealed record CreateHabitCommand(
    Guid UserId,
    string Name,
    string Icon,
    string ColorHex,
    string? Description);