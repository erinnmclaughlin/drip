using Microsoft.JSInterop;

namespace Drip.Services;

/// <summary>
/// Service for managing the theme of the application.
/// </summary>
/// <param name="jsRuntime">The JavaScript runtime.</param>
public sealed class ThemeService(IJSRuntime jsRuntime)
{
    private readonly IJSRuntime _jsRuntime = jsRuntime;

    /// <summary>
    /// Event handler for when the theme changes.
    /// </summary>
    public event Action<string>? OnThemeChanged;

    /// <summary>
    /// Gets the current theme of the application.
    /// </summary>
    /// <returns>The current theme of the application.</returns>
    public async Task<string> GetThemeAsync()
    {
        return await _jsRuntime.InvokeAsync<string>("getCurrentTheme");
    }

    /// <summary>
    /// Sets the theme of the application.
    /// </summary>
    /// <param name="theme">The theme to set.</param>
    public async Task SetThemeAsync(string theme)
    {
        await _jsRuntime.InvokeVoidAsync("setTheme", theme);
        OnThemeChanged?.Invoke(theme);
    }

    /// <summary>
    /// Toggles the theme of the application.
    /// </summary>
    public async Task ToggleThemeAsync()
    {
        await _jsRuntime.InvokeVoidAsync("toggleTheme");
    }

    /// <summary>
    /// Checks if the application is in dark mode.
    /// </summary>
    /// <returns>True if the application is in dark mode, false otherwise.</returns>
    public async Task<bool> IsDarkModeAsync()
    {
        return await _jsRuntime.InvokeAsync<bool>("isDarkMode");
    }

    /// <summary>
    /// Checks if the application is in light mode.
    /// </summary>
    /// <returns>True if the application is in light mode, false otherwise.</returns>
    public async Task<bool> IsLightModeAsync()
    {
        return await _jsRuntime.InvokeAsync<bool>("isLightMode");
    }
}
