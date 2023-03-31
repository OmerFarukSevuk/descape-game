public interface IInfo<T>
{
    public T InfoPanel { get; }

    public void OpenPanel();
    public void OffPanel();
}

public interface IInteractable
{
    public void Interact();
}