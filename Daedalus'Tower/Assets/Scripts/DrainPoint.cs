using UnityEngine;

public class DrainPoint : MonoBehaviour
{
    //Stores the object representing the water level
    public GameObject WaterLevel;
    //How far ray travels
    public float maxDist = 10f;
    //Water fill speed
    public float fillSpeed = 0.1f;
    //Water drain speed
    public float drainSpeed = 0.1f;
    //Ray line length
    float lineLength;
    //Base/default height of water
    public float waterBaseHeight; 
    public float maxWaterHeight;
    public bool isFlowing = false;

    public ParticleSystem waterFalling;

    LineRenderer line;

    void Start()
    {
        waterFalling.Stop();
        //Set waterBaseHeight
        waterBaseHeight = WaterLevel.transform.position.y;
        line = GetComponent<LineRenderer>();
    }

    void Update()
    {
        //Ray starting point
        Vector3 origin = transform.position; //sets origin to the position (middle) of the object this script is attached to (the sphere)
        //Ray direction
        Vector3 direction = -transform.up; //sets direction to the z-axis (better than .up, because if the sphere rotates, the ray will rotate with it)

        //Container that stores hit info
        RaycastHit hitInfo;

        //Naive line renderer positioning
        line.SetPosition(0,transform.position);
        line.SetPosition(1,transform.position);

        //Fire raycast
        if (Physics.Raycast(origin, direction, out hitInfo, maxDist)) //returns true if hit, false if miss
        {
            //Set the raycast line length
            lineLength = hitInfo.distance;
            line.SetPosition(1,transform.position - Vector3.up*lineLength);

            //If the ray collides with the "Water" object and the height of the object is not higher than 2.5...
            if (hitInfo.collider.CompareTag("Water") && hitInfo.collider.transform.position.y <= maxWaterHeight)
            {
                if(!isFlowing) {
                    isFlowing = true;
                    waterFalling.Play();
                }
                //Raise the "Water"
                hitInfo.collider.transform.position += Vector3.up * fillSpeed * Time.deltaTime;
                
                
            }
            else
            {
                waterFalling.Stop();
                isFlowing = false;
                //If the water level is higher than the base height...
                if (WaterLevel.transform.position.y > waterBaseHeight)
                {
                    //Drain the water
                    WaterLevel.transform.position -= Vector3.up * drainSpeed * Time.deltaTime;
                }
            }   
        }
        else
        {
            //No water collision, so set the raycast length to maxDist
            lineLength = maxDist;

            //If the water level is higher than the base height...
            if (WaterLevel.transform.position.y > waterBaseHeight)
            {
                //Drain the water
                WaterLevel.transform.position -= Vector3.up * drainSpeed * Time.deltaTime;
            }
        }
        //Set the raycast line color to blue to represent water
        Debug.DrawRay(origin, direction * lineLength, Color.blue);
    }
}

