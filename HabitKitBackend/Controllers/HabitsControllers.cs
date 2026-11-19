using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/habits")]
public sealed class HabitsController(ICurrentUser currentUser) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<HabitDto>>> GetAll(
        [FromServices] GetHabitsHandler handler,
        [FromQuery] bool includeArchived,
        CancellationToken ct)
    {
        var habits = await handler.HandleAsync(new GetHabitsQuery(currentUser.UserId, includeArchived), ct);
        return Ok(habits);
    }

    [HttpPost]
    public async Task<ActionResult<HabitDto>> Create(
        [FromServices] CreateHabitHandler handler,
        [FromBody] CreateHabitRequest request,
        CancellationToken ct)
    {
        var command = new CreateHabitCommand(currentUser.UserId,
            request.Name,
            request.Icon,
            request.ColorHex,
            request.Description);

        var habit = await handler.HandleAsync(command, ct);
        return Created($"/api/habits/{habit.Id}", habit);
    }
}