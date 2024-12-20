using Microsoft.JSInterop;

namespace Kbs.Web.Components;

public class NotificationService
{
    private readonly IJSRuntime _jsRuntime;

    public NotificationService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task ShowSuccessNotification(string message)
    {
        await _jsRuntime.InvokeVoidAsync("notyf.success", message);
    }

    public async Task ShowErrorNotification(string message)
    {
        await _jsRuntime.InvokeVoidAsync("notyf.error", message);
    }

    public async Task<bool> ShowConfirmation(string message)
    {
        return await _jsRuntime.InvokeAsync<bool>("swalConfirmation", message);
    }
}