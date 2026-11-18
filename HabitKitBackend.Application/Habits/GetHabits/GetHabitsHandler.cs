public sealed class GetHabitsHandler(IHabitRepository habitRepository)
{
    public async Task<IReadOnlyList<HabitDto>> HandleAsync(GetHabitsQuery query, CancellationToken ct = default)
    {
        var habits = await habitRepository.GetByUserAsync(query.UserId, query.IncludeArchived, ct);
        return habits.Select(h => h.ToDto()).ToList();
    }
}