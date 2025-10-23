using MudBlazor;

namespace Boxes.Frontend.Theme;

public static class CorporateTheme
{
    // Tonos “cartón”
    private const string Brown600 = "#6D4C41";

    private const string Brown800 = "#5D4037";
    private const string Brown900 = "#3E2723";
    private const string KraftBeige = "#F3EEE6";
    private const string OffWhite = "#FFFCF6";
    private const string Sand = "#D6CBBE";
    private const string Caramel = "#B58B68";

    public static readonly MudTheme Theme = new()
    {
        // 🌞 CLARO
        PaletteLight = new PaletteLight
        {
            Primary = Brown600,
            Secondary = Caramel,
            Tertiary = Sand,
            Background = OffWhite,
            Surface = KraftBeige,
            AppbarBackground = Brown800,
            AppbarText = "#FFFFFF",
            DrawerBackground = KraftBeige,
            DrawerText = Brown900,
            TextPrimary = Brown900,
            TextSecondary = Brown800,
            Divider = "#E2D8CA",
            TableLines = "#E9E2D8",
            Success = "#6BAA75",
            Warning = "#E0A106",
            Error = "#C44230",
            Info = "#7C9CBF",
        },

        // 🌚 OSCURO
        PaletteDark = new PaletteDark
        {
            Primary = Caramel,
            Secondary = Sand,
            Tertiary = KraftBeige,
            Background = Brown900,
            Surface = Brown800,
            AppbarBackground = Brown900,
            AppbarText = "#F5F0E6",
            DrawerBackground = Brown800,
            DrawerText = "#F5F0E6",
            TextPrimary = "#F5F0E6",
            TextSecondary = "#E6DCCF",
            Divider = "#5A463D",
            TableLines = "#5A463D",
            Success = "#8FCF9B",
            Warning = "#F3C34F",
            Error = "#E57368",
            Info = "#9CB6D6",
        },

        LayoutProperties = new LayoutProperties
        {
            DefaultBorderRadius = "10px"
        },

        // 👇 TIPOGRAFÍA: la omitimos para evitar conflictos entre v6/v7
        // Typography = ...
    };
}