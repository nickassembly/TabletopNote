using Microsoft.AspNetCore.Mvc;
using TabletopNote.API.Dtos;
using TabletopNote.Data;
using TabletopNote.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace TabletopNote.API.Controllers
{
    [ApiController]
    [Route("campaigns/{campaignId}/events")]
    public class CalendarEventsController : ControllerBase
    {
        private readonly TabletopNoteDbContext _context;

        public CalendarEventsController(TabletopNoteDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        // GET - /campaigns/{campaignId}/events
        public async Task<ActionResult<List<CalendarEventDto>>> GetAllCalendarEvent([FromRoute] int campaignId)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            if (!campaignExists)
                return NotFound($"Campaign {campaignId} does not exist.");

            var calendarEvents = await _context.CalendarEvents.Where(cd => cd.CampaignId == campaignId).ToListAsync();

            var calendarEventDtos = new List<CalendarEventDto>();

            foreach (var calendarEvent in calendarEvents)
            {
                calendarEventDtos.Add(new CalendarEventDto
                {
                    CalendarEventId = calendarEvent.CalendarEventId,
                    EventName = calendarEvent.EventName,
                    EventDescription = calendarEvent.EventDescription,
                    EventStartDate = calendarEvent.EventStartDate,
                    EventEndDate = calendarEvent.EventEndDate,
                    IsGMVisibleOnly = calendarEvent.IsGMVisibleOnly
                });
            }
            return calendarEventDtos;
        }

        [HttpGet("{calendarEventId}")]
        // GET - /campaigns/{campaignId}/events/{calendarEventId}
        public async Task<ActionResult<CampaignDocumentDto>> GetReferenceDocumentById(
            [FromRoute] int campaignId,
            [FromRoute] int calendarEventId)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            var eventExists = await _context.CalendarEvents.AnyAsync(e => e.CalendarEventId == calendarEventId);

            var calendarEvent = await _context.CalendarEvents
                .FirstOrDefaultAsync(e => e.CalendarEventId == calendarEventId && e.CampaignId == campaignId);

            if (calendarEvent is null)
                return NotFound($"Event {calendarEventId} for Campaign {campaignId} not found.");

            return Ok(calendarEvent);
        }

        [HttpPost]
        // POST - /campaigns/{campaignId}/events
        public async Task<ActionResult> AddCalendarEvent(
            [FromRoute] int campaignId,
            [FromBody] CalendarEventAddDto newCalendarEvent)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);

            if (!campaignExists)
                return NotFound($"Campaign {campaignId} does not exist.");

            if (newCalendarEvent is null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var calendarEventToAdd = new CalendarEventEntity
            {
                CampaignId = campaignId,
                EventName = newCalendarEvent.EventName,
                EventDescription = newCalendarEvent.EventDescription,
                EventStartDate = newCalendarEvent.EventStartDate,
                EventEndDate = newCalendarEvent.EventEndDate,
                IsGMVisibleOnly = newCalendarEvent.IsGMVisibleOnly
            };

            _context.CalendarEvents.Add(calendarEventToAdd);

            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(AddCalendarEvent),
                new { campaignId, id = calendarEventToAdd.CalendarEventId },
                null
            );
        }

        [HttpPut("{calendarEventId}")]
        // PUT - /campaigns/{campaignId}/events/{calendarEventId}
        public async Task<ActionResult> UpdateCalendarEvent(
             [FromRoute] int campaignId,
             [FromRoute] int calendarEventId,
             [FromBody] CalendarEventUpdateDto eventToUpdate)
        {
            if (eventToUpdate is null) return BadRequest("Request body is required.");
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            var eventExists = await _context.CalendarEvents.AnyAsync(e => e.CalendarEventId == calendarEventId);

            var calendarEvent = await _context.CalendarEvents
                .FirstOrDefaultAsync(e => e.CalendarEventId == calendarEventId && e.CampaignId == campaignId);

            if (calendarEvent is null)
                return NotFound($"Event {calendarEventId} for Campaign {campaignId} not found.");

            calendarEvent.EventName = eventToUpdate.EventName;
            calendarEvent.EventDescription = eventToUpdate.EventDescription;
            calendarEvent.EventStartDate = eventToUpdate.EventStartDate;
            calendarEvent.EventEndDate = eventToUpdate.EventEndDate;
            calendarEvent.IsGMVisibleOnly = eventToUpdate.IsGMVisibleOnly;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{calendarEventId}")]
        // DELETE - /campaigns/{campaignId}/events/{calendarEventId}
        public async Task<ActionResult> DeleteCalendarEvent(
              [FromRoute] int campaignId,
              [FromRoute] int calendarEventId)
        {
            var campaignExists = await _context.Campaigns.AnyAsync(c => c.CampaignId == campaignId);
            var eventExists = await _context.CalendarEvents.AnyAsync(e => e.CalendarEventId == calendarEventId);

            var calendarEvent = await _context.CalendarEvents
                .FirstOrDefaultAsync(e => e.CalendarEventId == calendarEventId && e.CampaignId == campaignId);

            if (calendarEvent is null)
                return NotFound($"Event {calendarEventId} for Campaign {campaignId} not found.");

            _context.CalendarEvents.Remove(calendarEvent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
