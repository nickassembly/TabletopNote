using MudBlazor;

namespace TabletopNote.UI.Components.Layout
{
    public static class AppTheme
    {
        public static MudTheme Dark = new MudTheme
        {
            PaletteDark = new PaletteDark
            {
                Background = "#121212",
                Surface = "#1E1E1E",
                AppbarBackground = "#1F1F1F",
                DrawerBackground = "#1B1B1B",

                Primary = "#7E57C2",
                Secondary = "#26A69A",

                TextPrimary = "#E0E0E0",
                TextSecondary = "#A0A0A0"
            },

            LayoutProperties = new LayoutProperties
            {
                DefaultBorderRadius = "8px"
            }
        };
    }
}
