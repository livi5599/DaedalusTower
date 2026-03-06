using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public string Prompt => "Interact";
    [SerializeField] GameObject[] waterLevels;
    public void Interact(GameObject interactor)
    {
        for (int i = 0; i < waterLevels.Length; i++)
        {
            waterLevels[i].SetActive(true);
        }
        Debug.Log($"Interacted with {name}");
    }
}
