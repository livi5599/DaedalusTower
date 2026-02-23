using UnityEngine;

public class Activate : MonoBehaviour
{
    //Tracks if object is active or not
    public bool active;

    //activates/powers on the object
    public void PowerOn()
    {
        active = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Ensures object isn't active forever
        active = false;
    }
}
