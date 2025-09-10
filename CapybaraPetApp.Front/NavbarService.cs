namespace CapybaraPetApp.Front;

public class NavbarService
{
    public event Action? OnChange;
    
    private bool _isOpen = true;

    public bool IsOpen
    {
        get => _isOpen;
        private set
        {
            _isOpen = value;
            NotifyStateChanged();
        }
    }

    public void Toggle() => IsOpen = !IsOpen;
    private void NotifyStateChanged() => OnChange?.Invoke();
}