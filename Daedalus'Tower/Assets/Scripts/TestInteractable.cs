using UnityEngine;

public class TestInteractable : MonoBehaviour
{
    [SerializeField] GameObject[] waterLevels;
    public string prompt = "Go UP";
    public void Interact()
    {
        for (int i = 0; i < waterLevels.Length; i++)
        {
            waterLevels[i].SetActive(true);
        }
        Debug.Log($"Interacted with {name}");
    }
}
