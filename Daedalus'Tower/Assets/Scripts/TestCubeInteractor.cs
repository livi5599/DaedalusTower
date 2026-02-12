using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public string Prompt => "Interact";

    public void Interact(GameObject interactor)
    {
        Debug.Log($"Interacted with {name}");
    }
}
