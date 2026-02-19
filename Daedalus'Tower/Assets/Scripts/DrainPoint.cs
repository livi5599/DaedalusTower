using Unity.VisualScripting;
using UnityEngine;

public class DrainPoint : MonoBehaviour
{
    //Stores the object representing the water level
    //How far ray travels
    public float maxDist = 10f;
    //Water fill speed
    //Water drain speed
    //Ray line length
    float lineLength;
    //Base/default height of water
    public bool isFlowing = false;

    public DrainPoint lastHole;

    public ParticleSystem waterFalling;

    [SerializeField] LineRenderer line;
    public Water hittingWater;
    public bool isFillingWaterSource = false;

    void Start()
    {
        waterFalling.Stop();
        //Set waterBaseHeight
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

        isFlowing = false;


        //Fire raycast
        if (Physics.Raycast(origin, direction, out hitInfo, maxDist)) //returns true if hit, false if miss
        {
            Debug.Log("Hit " + hitInfo.transform.name);
            //Set the raycast line length
            lineLength = hitInfo.distance;
            line.SetPosition(1,hitInfo.point);
            if (hitInfo.collider.CompareTag("Hole"))
            {
                isFlowing = true;
                lastHole = hitInfo.transform.GetComponentInChildren<DrainPoint>();
                lastHole.enabled = true;
            }
            else if (lastHole)
            {
                lastHole.enabled = false;
                lastHole = null;
            }
            
            
            //If the ray collides with the "Water" object and the height of the object is not higher than 2.5...
            if (hitInfo.collider.CompareTag("Water"))// && hitInfo.collider.transform.position.y <= maxWaterHeight)
            {
                isFlowing = true;
                hittingWater = hitInfo.transform.GetComponentInChildren<Water>();
                hittingWater.isFilling = true;
                isFillingWaterSource = true;
                //Raise the "Water"   
            }
        }
        else
        {
            //No water collision, so set the raycast length to maxDist
            lineLength = maxDist;
            //If the water level is higher than the base height...
            
        }

        //Set the raycast line color to blue to represent water
        Debug.DrawRay(origin, direction * lineLength, Color.blue);
        if(isFlowing) 
        {
            Debug.Log("Called Play");
            if (!waterFalling.isPlaying)
            {
                waterFalling.Play();
            }
        }
        else
        {
            Debug.Log("Called Stop");
            waterFalling.Stop();
            //waterFalling.Clear();
            if (isFillingWaterSource)
            {
                Debug.Log("No more Water.");
                hittingWater.isFilling = false;
                hittingWater = null;
                isFillingWaterSource = false;   
            }
        }

    }

    void OnDisable()
    {
        line.SetPosition(0,transform.position);
        line.SetPosition(1,transform.position);
        waterFalling.Stop();
         if (isFillingWaterSource)
            {
                Debug.Log("No more Water.");
                hittingWater.isFilling = false;
                hittingWater = null;
                isFillingWaterSource = false;   
            }
    }
}

