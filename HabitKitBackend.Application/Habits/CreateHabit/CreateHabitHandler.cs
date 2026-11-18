public sealed class CreateHabitHandler(IHabitRepository habits, IUnitOfWork unitOfWork)
{
    public async Task<HabitDto> HandleAsync(CreateHabitCommand command, CancellationToken ct = default)
    {
        var habit = Habit.Create(
            command.UserId,
            command.Name,
            command.Description,
            command.Icon,
            command.ColorHex
            );

        habits.Add(habit);
        await unitOfWork.SaveChangesAsync(ct);
        return habit.ToDto();
    }
}