using UnityEngine;
using UnityEngine.Events;

public class Activate : MonoBehaviour
{
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
        //Ensures object isn't active forever
        active = false;

        if (Input.GetButtonDown("Jump"))
        {
            PowerOn();
        }
    }


}
