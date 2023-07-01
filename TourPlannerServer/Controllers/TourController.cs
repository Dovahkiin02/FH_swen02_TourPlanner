using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TourPlannerServer.Model;
using System.Linq;

namespace TourPlannerServer.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TourController : ControllerBase {

    private readonly ApplicationContext _context;
    private readonly ILogger<TourController> _logger;

    public TourController(ApplicationContext context, ILogger<TourController> logger) {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Tour>>> GetTours() {
        _logger.LogInformation("Fetching all tours");
        return await _context.Tour.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Tour>> GetTour(int id) {
        _logger.LogInformation($"Fetching tour with ID {id}");
        var tour = await _context.Tour.FindAsync(id);

        if (tour == null) {
            return NotFound();
        }

        return Ok(tour);
    }

    [HttpGet("{id}/TourLogs")]
    public async Task<ActionResult<IEnumerable<TourLog>>> GetTourLogs(int id) {
        _logger.LogInformation($"Fetching tourLogs for Tour with ID {id}");
        var tourLogs = await _context.TourLog.Where(tourLog => tourLog.TourId == id).ToListAsync();

        if (tourLogs == null || !tourLogs.Any()) {
            return NotFound();
        }

        return Ok(tourLogs);
    }

    [HttpPost("Upsert")]
    public async Task<ActionResult<Tour>> UpsertTour(Tour tour) {
        _logger.LogInformation($"Upserting tour with ID {tour.Id}");
        _logger.LogInformation($"test");

        try {
            // Look for an existing tour with the same ID
            var existingTour = await _context.Tour.FindAsync(tour.Id);

            // If the tour exists, update it; otherwise, create a new one
            if (existingTour != null) {
                _logger.LogInformation($"Updating existing tour with ID {tour.Id}");
                _context.Entry(existingTour).CurrentValues.SetValues(tour);
            } else {
                _logger.LogInformation($"Creating a new tour with ID {tour.Id}");
                _context.Tour.Add(tour);
            }

            // Save the changes to the database
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            _logger.LogError($"Concurrency exception when upserting tour with ID {tour.Id}");
            return StatusCode(500, new { error = "An error occurred while upserting the tour. Please try again." });
        } catch (Exception ex) {
            _logger.LogError($"Error when upserting tour with ID {tour.Id}: {ex.Message}");
            return StatusCode(500, new { error = "An unexpected error occurred. Please try again." });
        }

        _logger.LogInformation($"Successfully upserted tour with ID {tour.Id}");
        return CreatedAtAction("GetTour", new { id = tour.Id }, tour);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTour(int id) {
        _logger.LogInformation($"Deleting tour with ID {id}");

        var tour = await _context.Tour.FindAsync(id);
        if (tour == null) {
            _logger.LogWarning($"Failed to delete tour: Tour with ID {id} not found");
            return NotFound(new { error = "Tour not found." });
        }

        try {
            _context.Tour.Remove(tour);
            await _context.SaveChangesAsync();
        } catch (Exception ex) {
            _logger.LogError($"Error when deleting tour with ID {id}: {ex.Message}");
            return StatusCode(500, new { error = "An unexpected error occurred while deleting the tour. Please try again." });
        }

        _logger.LogInformation($"Successfully deleted tour with ID {id}");
        return NoContent();
    }

    private bool TourExists(int id) {
        return _context.Tour.Any(e => e.Id == id);
    }
}