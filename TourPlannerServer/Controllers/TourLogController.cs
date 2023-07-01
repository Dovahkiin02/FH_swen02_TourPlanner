using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TourPlannerServer.Model;

namespace TourPlannerServer.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class TourLogController : ControllerBase {
        private readonly ApplicationContext _context;
        private readonly ILogger<TourLogController> _logger;

        public TourLogController(ApplicationContext context, ILogger<TourLogController> logger) {
            _context = context;
            _logger = logger;
        }

        // GET: api/TourLogs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourLog>>> GetTourLogs() {
            try {
                _logger.LogInformation("Fetching all tour logs.");
                return await _context.TourLog.ToListAsync();
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while fetching all tour logs.");
                return StatusCode(500, new { error = "An unexpected error occurred while retrieving tour logs." });
            }
        }

        // GET: api/TourLogs/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TourLog>> GetTourLog(Guid id) {
            try {
                _logger.LogInformation($"Fetching tour log with ID {id}.");
                var tourLog = await _context.TourLog.FindAsync(id);

                if (tourLog == null) {
                    _logger.LogWarning($"No tour log found with ID {id}.");
                    return NotFound(new { error = "A TourLog with this ID does not exist." });
                }

                return tourLog;
            } catch (Exception ex) {
                _logger.LogError(ex, $"An error occurred while fetching tour log with ID {id}.");
                return StatusCode(500, new { error = "An unexpected error occurred while retrieving the tour log." });
            }
        }

        [HttpPost("Upsert")]
        public async Task<ActionResult<TourLog>> UpsertTourLog(TourLogDto tourLogDto) {
            try {
                TourLog tourLog = new TourLog {
                    Id = tourLogDto.Id,
                    Date = tourLogDto.Date,
                    Difficulty = tourLogDto.Difficulty,
                    Duration = tourLogDto.Duration,
                    Rating = tourLogDto.Rating,
                    Comment = tourLogDto.Comment,
                    TourId = tourLogDto.TourId
                };

                var existingTourLog = await _context.TourLog.FindAsync(tourLogDto.Id);

                if (existingTourLog != null) {
                    _logger.LogInformation($"Updating tour log with ID {tourLogDto.Id}.");
                    _context.Entry(existingTourLog).CurrentValues.SetValues(tourLog);
                } else {
                    _logger.LogInformation($"Creating new tour log with ID {tourLogDto.Id}.");
                    _context.TourLog.Add(tourLog);
                }

                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetTourLog), new { id = tourLog.Id }, tourLog);
            } catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505") {
                _logger.LogError(ex, $"A TourLog with ID {tourLogDto.Id} already exists.");
                return Conflict(new { error = "A TourLog with this ID already exists." });

            } catch (DbUpdateException ex) when (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23503") {
                _logger.LogError(ex, $"The referenced Tour does not exist.");
                return BadRequest(new { error = "The referenced Tour does not exist." });

            } catch (Exception ex) {
                _logger.LogError(ex, $"An unexpected error occurred while upserting the tour log with ID {tourLogDto.Id}.");
                return StatusCode(500, new { error = "An unexpected error occurred while upserting the tour log." });
            }
        }

        // DELETE: api/TourLogs/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<TourLog>> DeleteTourLog(Guid id) {
            try {
                _logger.LogInformation($"Deleting tour log with ID {id}.");
                var tourLog = await _context.TourLog.FindAsync(id);
                if (tourLog == null) {
                    _logger.LogWarning($"No tour log found with ID {id} for deletion.");
                    return NotFound(new { error = "A TourLog with this ID does not exist." });
                }

                _context.TourLog.Remove(tourLog);
                await _context.SaveChangesAsync();
            } catch (Exception ex) {
                _logger.LogError(ex, $"An error occurred while deleting tour log with ID {id}.");
                return StatusCode(500, new { error = "An unexpected error occurred while deleting the tour log." });
            }

            return NoContent();
        }
    }
}
