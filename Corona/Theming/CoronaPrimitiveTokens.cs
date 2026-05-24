namespace Corona.Theming;

/// <summary>
/// Defines the raw, low-level primitive design tokens used to build semantic tokens.
/// </summary>
public sealed record CoronaPrimitiveTokens(
    CoronaColorPrimitives Colors,
    CoronaSpacingPrimitives Spacing,
    CoronaRadiusPrimitives Radius,
    CoronaShadowPrimitives Shadows,
    CoronaTypographyPrimitives Typography)
{
    /// <summary>
    /// Gets the default primitive token set.
    /// </summary>
    public static CoronaPrimitiveTokens Default { get; } = new(
        new CoronaColorPrimitives(),
        new CoronaSpacingPrimitives(),
        new CoronaRadiusPrimitives(),
        new CoronaShadowPrimitives(),
        new CoronaTypographyPrimitives());
}

/// <summary>
/// Raw color primitives.
/// </summary>
public sealed record CoronaColorPrimitives(

    // ===== BASE =====
    string White = "#FFFFFF",
    string Black = "#000000",

    // ===== GRAYS =====
    string Gray50 = "#F8FAFC",
    string Gray100 = "#F1F5F9",
    string Gray200 = "#E2E8F0",
    string Gray300 = "#CBD5E1",
    string Gray400 = "#94A3B8",
    string Gray500 = "#64748B",
    string Gray600 = "#475569",
    string Gray700 = "#334155",
    string Gray800 = "#1E293B",
    string Gray900 = "#0F172A",

    // ===== BRAND (PRIMARY) =====
    string Brand50 = "#EFF6FF",
    string Brand100 = "#DBEAFE",
    string Brand200 = "#BFDBFE",
    string Brand300 = "#93C5FD",
    string Brand400 = "#60A5FA",
    string Brand500 = "#3B82F6",
    string Brand600 = "#2563EB",
    string Brand700 = "#1D4ED8",
    string Brand800 = "#1E40AF",
    string Brand900 = "#1E3A8A",

    // ===== RED =====
    string Red50 = "#FEF2F2",
    string Red100 = "#FEE2E2",
    string Red200 = "#FECACA",
    string Red300 = "#FCA5A5",
    string Red400 = "#F87171",
    string Red500 = "#EF4444",
    string Red600 = "#DC2626",
    string Red700 = "#B91C1C",
    string Red800 = "#991B1B",
    string Red900 = "#7F1D1D",

    // ===== GREEN =====
    string Green50 = "#F0FDF4",
    string Green100 = "#DCFCE7",
    string Green200 = "#BBF7D0",
    string Green300 = "#86EFAC",
    string Green400 = "#4ADE80",
    string Green500 = "#22C55E",
    string Green600 = "#16A34A",
    string Green700 = "#15803D",
    string Green800 = "#166534",
    string Green900 = "#14532D",

    // ===== BLUE =====
    string Blue50 = "#EFF6FF",
    string Blue100 = "#DBEAFE",
    string Blue200 = "#BFDBFE",
    string Blue300 = "#93C5FD",
    string Blue400 = "#60A5FA",
    string Blue500 = "#3B82F6",
    string Blue600 = "#2563EB",
    string Blue700 = "#1D4ED8",
    string Blue800 = "#1E40AF",
    string Blue900 = "#1E3A8A",

    // ===== YELLOW / AMBER =====
    string Amber50 = "#FFFBEB",
    string Amber100 = "#FEF3C7",
    string Amber200 = "#FDE68A",
    string Amber300 = "#FCD34D",
    string Amber400 = "#FBBF24",
    string Amber500 = "#F59E0B",
    string Amber600 = "#D97706",
    string Amber700 = "#B45309",
    string Amber800 = "#92400E",
    string Amber900 = "#78350F",

    // ===== PURPLE =====
    string Purple50 = "#FAF5FF",
    string Purple100 = "#F3E8FF",
    string Purple200 = "#E9D5FF",
    string Purple300 = "#D8B4FE",
    string Purple400 = "#C084FC",
    string Purple500 = "#A855F7",
    string Purple600 = "#9333EA",
    string Purple700 = "#7E22CE",
    string Purple800 = "#6B21A8",
    string Purple900 = "#581C87",

    // ===== PINK =====
    string Pink50 = "#FDF2F8",
    string Pink100 = "#FCE7F3",
    string Pink200 = "#FBCFE8",
    string Pink300 = "#F9A8D4",
    string Pink400 = "#F472B6",
    string Pink500 = "#EC4899",
    string Pink600 = "#DB2777",
    string Pink700 = "#BE185D",
    string Pink800 = "#9D174D",
    string Pink900 = "#831843",

    // ===== TEAL =====
    string Teal50 = "#F0FDFA",
    string Teal100 = "#CCFBF1",
    string Teal200 = "#99F6E4",
    string Teal300 = "#5EEAD4",
    string Teal400 = "#2DD4BF",
    string Teal500 = "#14B8A6",
    string Teal600 = "#0D9488",
    string Teal700 = "#0F766E",
    string Teal800 = "#115E59",
    string Teal900 = "#134E4A",

    // ===== ORANGE =====
    string Orange50 = "#FFF7ED",
    string Orange100 = "#FFEDD5",
    string Orange200 = "#FED7AA",
    string Orange300 = "#FDBA74",
    string Orange400 = "#FB923C",
    string Orange500 = "#F97316",
    string Orange600 = "#EA580C",
    string Orange700 = "#C2410C",
    string Orange800 = "#9A3412",
    string Orange900 = "#7C2D12",

    // ===== CYAN =====
    string Cyan50 = "#ECFEFF",
    string Cyan100 = "#CFFAFE",
    string Cyan200 = "#A5F3FC",
    string Cyan300 = "#67E8F9",
    string Cyan400 = "#22D3EE",
    string Cyan500 = "#06B6D4",
    string Cyan600 = "#0891B2",
    string Cyan700 = "#0E7490",
    string Cyan800 = "#155E75",
    string Cyan900 = "#164E63",

    // ===== SEMANTIC =====
    string Success = "#22C55E",
    string SuccessDark = "#15803D",
    string Warning = "#F59E0B",
    string WarningDark = "#B45309",
    string Danger = "#EF4444",
    string DangerDark = "#B91C1C",
    string Info = "#0EA5E9",
    string InfoDark = "#0369A1",

    // ===== SURFACES =====
    string Surface = "#FFFFFF",
    string SurfaceAlt = "#F8FAFC",
    string SurfaceDark = "#1E293B",
    string SurfaceDarker = "#0F172A",

    // ===== OVERLAYS =====
    string OverlayLight = "rgba(0,0,0,0.05)",
    string OverlayMedium = "rgba(0,0,0,0.15)",
    string OverlayDark = "rgba(0,0,0,0.35)",
    string OverlayBlack = "rgba(0,0,0,0.60)",
    string OverlayWhite = "rgba(255,255,255,0.40)",

    // ===== TEXT =====
    string TextPrimary = "#0F172A",
    string TextSecondary = "#475569",
    string TextMuted = "#94A3B8",
    string TextInverted = "#FFFFFF"
);


/// <summary>
/// Raw spacing primitives.
/// </summary>
public sealed record CoronaSpacingPrimitives(
    string Xs = "0.25rem",
    string Sm = "0.5rem",
    string Md = "1rem",
    string Lg = "1.5rem",
    string Xl = "2rem");

/// <summary>
/// Raw border radius primitives.
/// </summary>
public sealed record CoronaRadiusPrimitives(
    string Sm = "0.375rem",
    string Md = "0.75rem",
    string Lg = "1rem");

/// <summary>
/// Raw shadow primitives.
/// </summary>
public sealed record CoronaShadowPrimitives(
    string Sm = "0 1px 2px rgba(15, 23, 42, 0.10)",
    string Md = "0 8px 18px rgba(15, 23, 42, 0.12)",
    string Lg = "0 14px 30px rgba(15, 23, 42, 0.16)");

/// <summary>
/// Raw typography primitives.
/// </summary>
public sealed record CoronaTypographyPrimitives(
    string FontFamily = "Inter, system-ui, -apple-system, Segoe UI, Roboto, Helvetica, Arial, sans-serif",
    string SizeSm = "0.875rem",
    string SizeMd = "1rem",
    string SizeLg = "1.125rem",
    string WeightMedium = "500",
    string WeightSemibold = "600");
