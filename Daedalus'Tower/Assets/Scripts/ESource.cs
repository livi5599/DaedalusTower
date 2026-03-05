using UnityEngine;

public class ESource : MonoBehaviour
{
    //Maximum distance the ray can travel
    public float maxDist = 1000f;
    //Ray line length
    float lineLength;
    //Tracks beam 
    [SerializeField] LineRenderer line;
    //Tracks beam in minimap
    [SerializeField] LineRenderer mmBeam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = GetComponent<LineRenderer>();

        //Only search for mmBeam if not already assigned in Inspector
        if (mmBeam == null)
        {
            //Looks for child named "mmBeam" to prevent grabbing the "line" line renderer
            Transform child = transform.Find("mmBeam");
            if (child != null)
            {
                mmBeam = child.GetComponent<LineRenderer>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Container that stores hit info
        RaycastHit hitInfo;

        //Ray starting point
        Vector3 origin = transform.position; //sets origin to the position (middle) of the source of the ray
        //Ray direction
        Vector3 direction = transform.forward;

        //Fire the ray
        if (Physics.Raycast(origin, direction, out hitInfo, maxDist)) //returns true if hit, false if miss
        {
            line.SetPosition(0, origin); //Start of line
            line.SetPosition(1, hitInfo.point); //End of line

            mmBeam.SetPosition(0, origin); //Start of minimap beam line
            mmBeam.SetPosition(1, hitInfo.point); //End of minimap beam line

            //Sets ray line length
            lineLength = hitInfo.distance;

            //If the ray hit a gem...
            if (hitInfo.collider.CompareTag("Gem"))
            {
                //Store the hit gem
                EConductor hitGem = hitInfo.collider.GetComponent<EConductor>();

                if (hitGem != null)
                {
                    hitGem.isActive = true;
                    hitGem.incomingHitPoint = hitInfo.point;
                }
            }
        }
        else //ray hit nothing
        {
            line.SetPosition(0, origin);
            line.SetPosition(1, origin + (direction * maxDist));
            mmBeam.SetPosition(0, origin);
            mmBeam.SetPosition(1, origin + (direction * maxDist));
        }
    }
}
