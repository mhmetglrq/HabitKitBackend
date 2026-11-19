using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class HabitConfiguration : IEntityTypeConfiguration<Habit>
{
    public void Configure(EntityTypeBuilder<Habit> builder)
    {
        builder.ToTable("Habits");
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Id).ValueGeneratedNever();
        builder.Property(h => h.Name).IsRequired().HasMaxLength(Habit.MaxLengthName);
        builder.Property(h => h.Description).HasMaxLength(500);
        builder.Property(h => h.Icon).IsRequired().HasMaxLength(100);
        builder.Property(h => h.ColorHex).IsRequired().HasMaxLength(7);

        builder.HasIndex(h => h.UserId);

        builder.HasMany(h => h.Completions)
               .WithOne()
               .HasForeignKey(c => c.HabitId)
               .OnDelete(DeleteBehavior.Cascade);
        builder.Navigation(h => h.Completions)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}

