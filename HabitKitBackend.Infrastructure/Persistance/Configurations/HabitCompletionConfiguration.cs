using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal sealed class HabitCompletionConfiguration : IEntityTypeConfiguration<HabitCompletion>
{
    public void Configure(EntityTypeBuilder<HabitCompletion> builder)
    {
        builder.ToTable("HabitCompletions");
        builder.HasKey(hc => hc.Id);
        builder.Property(hc => hc.Id).ValueGeneratedNever();
        builder.Property(hc => hc.HabitId).IsRequired();
        builder.HasIndex(hc => new { hc.Date, hc.HabitId }).IsUnique();
    }
}