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

        return tour;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTour(int id, Tour tour) {
        _logger.LogInformation($"Updating tour with ID {id}");

        if (id != tour.Id) {
            _logger.LogWarning($"Mismatched tour ID in PUT request. URL ID: {id}, Body ID: {tour.Id}");
            return BadRequest(new { error = "Mismatched tour ID in PUT request." });
        }

        _context.Entry(tour).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            if (!TourExists(id)) {
                _logger.LogWarning($"Failed to update tour: Tour with ID {id} not found");
                return NotFound(new { error = "Tour not found." });
            } else {
                _logger.LogError($"Concurrency exception when updating tour with ID {id}");
                return StatusCode(500, new { error = "An error occurred while updating the tour. Please try again." });
            }
        } catch (Exception ex) {
            _logger.LogError($"Error when updating tour with ID {id}: {ex.Message}");
            return StatusCode(500, new { error = "An unexpected error occurred. Please try again." });
        }

        _logger.LogInformation($"Successfully updated tour with ID {id}");
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Tour>> PostTour(Tour tour) {
        _logger.LogInformation("Creating a new tour");

        try {
            _context.Tour.Add(tour);
            await _context.SaveChangesAsync();
        } catch (Exception ex) {
            _logger.LogError($"Error when creating a new tour: {ex.Message}");
            return StatusCode(500, new { error = "An unexpected error occurred while creating the tour. Please try again." });
        }

        _logger.LogInformation($"Successfully created new tour with ID {tour.Id}");
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