using Microsoft.EntityFrameworkCore;

internal sealed class HabitRepository(AppDbContext context) : IHabitRepository
{
    public void Add(Habit habit)
        => context.Habits.Add(habit);

    public Task<Habit?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default)
        => context.Habits.Include(h => h.Completions).FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId, ct);
    public async Task<IReadOnlyList<Habit>> GetByUserAsync(Guid userId, bool includeArchived, CancellationToken ct = default)
    {
        var query = context.Habits
                         .AsNoTracking()
                         .Where(h => h.UserId == userId);
        if (!includeArchived)
            query = query.Where(h => !h.IsArchived);

        return await query.OrderBy(h => h.CreatedAt).ToListAsync(ct);
    }

    public void Remove(Habit habit) => context.Habits.Remove(habit);
}