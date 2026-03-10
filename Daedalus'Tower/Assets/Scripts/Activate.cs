using UnityEngine;
using UnityEngine.Events;

public class Activate : MonoBehaviour
{
    //current number of beams colliding with the object
    public int beamCount;
    //number of beams required to power the object
    [SerializeField] private int requiredBeams = 2;

    //Tracks if object is active or not
    public bool active;

    [SerializeField] UnityEvent eventToFire;

    //activates/powers on the object
    public void PowerOn()
    {
        active = true;
        eventToFire.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        //Ensures beamCount doesn't increase forever
        beamCount = 0;
    }

    //Called once per frame after Update
    void LateUpdate()
    {
        //if the number of colliding beams equals the required beam count, and the object isn't already active
        if (beamCount >= requiredBeams && !active)
        {
            //Power on the object
            PowerOn();
        }
    }
}