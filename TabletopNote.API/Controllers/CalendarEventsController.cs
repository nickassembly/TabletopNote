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

        // TODO - update calendar event
        [HttpPut]
        // PUT - /campaigns/{campaignId}/events/{calendarEventId}
        public async Task<ActionResult> UpdateCampaignDocument(
            [FromRoute] int campaignId,
            [FromRoute] int calendarEventId,
            [FromBody] CampaignEventUpdateDto eventToUpdate)
        {
            throw new NotImplementedException();
        }

        // TODO - delete calendar event
        [HttpDelete]
        // Delete - /campaigns/{campaignId}/events/{calendarEventId}
        public async Task<ActionResult> DeleteCampaignDocument(
            [FromRoute] int campaignId,
            [FromRoute] int calendarEventId)
        {
            throw new NotImplementedException();
        }

    }
}
