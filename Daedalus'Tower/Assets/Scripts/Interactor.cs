using UnityEngine;
using TMPro;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // drag your PlayerCamera here
    [SerializeField] private TMP_Text promptText;       // optional UI text
    [SerializeField] private float range = 3f;
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private KeyCode interactKey = KeyCode.E;



    public TestInteractable current;

    void Update()
    {
        
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, range, interactMask))            
        {
            Debug.DrawLine(cameraTransform.position, cameraTransform.position + cameraTransform.forward * range);
            
            current = hit.collider.GetComponent<TestInteractable>();
            print("Current object is: " + current.name);
        }
        else
        {
            current = null;
        }

        if (promptText != null)
        {
            if (current == null)
            {
                promptText.enabled = false;
                promptText.text = "";
            }
            else
            {
                promptText.enabled = true;
                promptText.text = $"Press E to {current.prompt}";
            }
        }

        if (current != null && Input.GetKeyDown(interactKey))
        {
            current.Interact();
        }
    }
}

