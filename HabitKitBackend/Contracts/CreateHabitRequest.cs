public sealed record CreateHabitRequest(
    String Name,
    String Icon,
    String ColorHex,
    String? Description
);