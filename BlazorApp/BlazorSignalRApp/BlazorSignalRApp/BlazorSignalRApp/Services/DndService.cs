
public class DndService
{
    public bool IsDnd { get; private set; } = false;

    public event Action? OnDndChanged;

    public void SetDnd(bool isDnd)
    {
        IsDnd = isDnd;
        OnDndChanged?.Invoke();
    }
}
