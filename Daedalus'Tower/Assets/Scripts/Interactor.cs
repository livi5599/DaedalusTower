using UnityEngine;
using TMPro;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // drag your PlayerCamera here
    [SerializeField] private TMP_Text promptText;       // optional UI text
    [SerializeField] private float range = 3f;
    [SerializeField] private LayerMask interactMask = ~0;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private IInteractable current;

    void Update()
    {
        current = null;

        if (cameraTransform != null &&
            Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, range, interactMask))
        {
            current = hit.collider.GetComponentInParent<IInteractable>();
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
                promptText.text = $"Press E to {current.Prompt}";
            }
        }

        if (current != null && Input.GetKeyDown(interactKey))
        {
            current.Interact(gameObject);
        }
    }
}

