public interface IHabitRepository
{
    Task<Habit?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default);

    Task<IReadOnlyList<Habit>> GetByUserAsync(Guid userId, bool includeArchived, CancellationToken ct = default);

    void Add(Habit habit);
    void Remove(Habit habit);
}