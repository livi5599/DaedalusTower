using UnityEngine;

public class ESource : MonoBehaviour
{
    //Maximum distance the ray can travel
    public float maxDist = 10f;
    //Ray line length
    float lineLength;
    //Used to make ray visible
    LineRenderer line;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Container that stores hit info
        RaycastHit hitInfo;

        //Ray starting point
        Vector3 origin = transform.position; //sets origin to the position (middle) of the source of the ray
        //Ray direction
        Vector3 direction = transform.up;

        //Fire the ray
        if (Physics.Raycast(origin, direction, out hitInfo, maxDist)) //returns true if hit, false if miss
        {
            line.SetPosition(0, origin); //Start of line
            line.SetPosition(1, hitInfo.point); //End of line

            //Sets ray line length
            lineLength = hitInfo.distance;

            //If the ray hit a gem...
            if (hitInfo.collider.CompareTag("Gem"))
            {
                //Store the hit gem
                Gem1 hitGem = hitInfo.collider.GetComponent<Gem1>();

                if (hitGem != null)
                {
                    hitGem.isActive = true;
                }
            }
        }
        else //ray hit nothing
        {
            line.SetPosition(0, origin);
            line.SetPosition(1, origin + (direction * maxDist));
        }
    }
}
