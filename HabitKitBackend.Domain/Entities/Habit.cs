public class Habit : BaseEntity
{
    public const int MaxLengthName = 100;

    public Guid UserId { get; private set; }

    public string Name { get; private set; } = string.Empty;

    public string? Description { get; private set; }

    public string Icon { get; private set; } = string.Empty;

    public string ColorHex { get; private set; } = string.Empty;

    public bool IsArchived { get; private set; } = false;

    private readonly List<HabitCompletion> _completions = [];
    public IReadOnlyCollection<HabitCompletion> Completions => _completions.AsReadOnly();

    private Habit() { }

    public static Habit Create(Guid userId, string name, string? description, string icon, string colorHex)
    {
        var habit = new Habit
        {
            UserId = userId,
        };

        habit.setDetails(name, description, icon, colorHex);
        return habit;
    }

    public void Update(string name, string? description, string icon, string colorHex)
    {
        setDetails(name, description, icon, colorHex);
        MarkUpdated();
    }

    public void Archive()
    {
        IsArchived = true;
        MarkUpdated();
    }

    public void Unarchive()
    {
        IsArchived = false;
        MarkUpdated();
    }

    public bool toogleCompletion(DateOnly date, DateOnly today)
    {
        if (IsArchived)
            throw new DomainException("Cannot toggle completion for an archived habit.");
        if (date > today)
            throw new DomainException("Cannot toggle completion for a future date.");

        var existingCompletion = _completions.FirstOrDefault(c => c.Date == date);
        if (existingCompletion != null)
        {
            _completions.Remove(existingCompletion);
            MarkUpdated();
            return false;
        }
        _completions.Add(new HabitCompletion(Id, date));
        MarkUpdated();

        return true;
    }

    private void setDetails(string name, string? description, string icon, string colorHex)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Habit name cannot be empty.");

        if (name.Length > MaxLengthName)
            throw new DomainException($"Habit name cannot exceed {MaxLengthName} characters.");

        if (string.IsNullOrWhiteSpace(icon))
            throw new DomainException("Icon cannot be empty.");

        if (!IsValidHexColor(colorHex))
            throw new DomainException("ColorHex must be a valid hex color code.");

        Name = name.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        Icon = icon;
        ColorHex = colorHex.ToUpperInvariant();

        MarkUpdated();
    }

    private static bool IsValidHexColor(string value) =>
        value is { Length: 7 } && value[0] == '#' && value[1..].All(Uri.IsHexDigit);
}