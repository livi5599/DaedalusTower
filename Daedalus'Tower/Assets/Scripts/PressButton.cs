using UnityEngine;

public class InteractableButton : MonoBehaviour
{
    public Water water;
    private bool stopDrain = false;

    void Update()
    {
        if (stopDrain && water != null)
        {
            water.isFilling = true;
        }
    }

    public void PressButton()
    {
        stopDrain = !stopDrain;

        if (stopDrain)
        {
            Debug.Log("Button Pressed: Drain Stopped");
        }
        else
        {
            Debug.Log("Button Pressed: Drain Enabled");
            water.isFilling = false;
        }
    }
}