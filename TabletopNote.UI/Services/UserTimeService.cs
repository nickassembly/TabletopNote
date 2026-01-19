using Microsoft.JSInterop;

namespace TabletopNote.UI.Clients
{
    public class UserTimeService
    {
        private readonly IJSRuntime _js;

        public UserTimeService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<TimeZoneInfo> GetUserTimeZoneAsync()
        {
            var tz = await _js.InvokeAsync<string>(
                "eval", "Intl.DateTimeFormat().resolvedOptions().timeZone"
            );
            return TimeZoneInfo.FindSystemTimeZoneById(tz);
        }

        public DateTime ConvertUtcToUserTime(DateTime utc, TimeZoneInfo tz)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utc, tz);
        }

        public DateTime ConvertUserTimeToUtc(DateTime userLocalTime, TimeZoneInfo tz)
        {
            var unspecified = DateTime.SpecifyKind(userLocalTime, DateTimeKind.Unspecified);

            return TimeZoneInfo.ConvertTimeToUtc(unspecified, tz);
        }

    }


}
