using UnityEngine;

public class ClawController : MonoBehaviour
{
    [SerializeField] private ClawForcer _forcer;

    public bool IsOpened { get; private set; }

    private void Awake()
    {
        OpenHooks();
    }

    public void OpenHooks()
    {
        if(IsOpened)
        {
            return;
        }

        _forcer.MoveDown(onComplete: () => IsOpened = true);
    }

    public void CloseHooks()
    {
        if (!IsOpened)
        {
            return;
        }

        _forcer.MoveUp(onComplete: () => IsOpened = false);
    }
}
