using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveModule
{
    public void OnInit()
    {
        
    }

    public void OnDestroy()
    {
        TryReleaseLastObject();
    }

    public IInteractable Object { get; private set; }

    public void SetCurrentInteractiveObject(IInteractable interactable)
    {
        TryReleaseLastObject();
        if (interactable == null)
            return;
        Object = interactable;
        interactable.OnInteractiveEnter();
    }

    public bool TryTriggerObject()
    {
        if (Object == null)
            return false;

        Object.OnInteractiveAction();
        TryReleaseLastObject();
        return true;
    }

    private void TryReleaseLastObject()
    {
        if (Object != null)
        {
            Object.OnInteractiveExit();
            Object = null;
        }
    }
}
