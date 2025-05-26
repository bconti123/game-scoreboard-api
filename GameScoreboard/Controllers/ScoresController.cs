using GameScoreboard.Models;
using GameScoreboard.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameScoreboard.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ScoresController : ControllerBase
{
    private readonly ScoreService _scoreService;

    public ScoresController(ScoreService scoreService)
    {
        _scoreService = scoreService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Score>>> Get() =>
        await _scoreService.GetAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<Score>> Get(string id)
    {
        var score = await _scoreService.GetAsync(id);

        if (score is null) return NotFound();

        return score;
    }

    [HttpPost]
    public async Task<IActionResult> Post(Score newScore)
    {
        await _scoreService.CreateAsync(newScore);
        return CreatedAtAction(nameof(Get), new { id = newScore.Id }, newScore);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(string id, Score updatedScore)
    {
        var existing = await _scoreService.GetAsync(id);
        if (existing is null) return NotFound();

        updatedScore.Id = id;
        await _scoreService.UpdateAsync(id, updatedScore);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var score = await _scoreService.GetAsync(id);
        if (score is null) return NotFound();

        await _scoreService.DeleteAsync(id);
        return NoContent();
    }
}
