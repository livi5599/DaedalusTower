using UnityEngine;

public class Gem1 : MonoBehaviour
{
    //Tracks if the gem is active
    // public bool isActive = false;
    // //Stores the point where the ray hit the gem
    // public Vector3 incomingHitPoint;
    // //Stores the possible hit points/nodes of the gem
    // public Transform[] nodes;
    // //Used to store the renderer attached to this gem
    // Renderer renderer;

    // // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
    //     //Sets the renderer
    //     renderer = GetComponent<Renderer>();
    //     //Sets line renderer
    //     line = GetComponent<LineRenderer>();
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     //Ensures gem is not active unless being hit by ray
    //     isActive = false;

    //     if (isActive) //if the gem is active...
    //     {
    //         //Emit the color
    //         renderer.material.EnableKeyword("_EMISSION");
    //         Color glowColor = Color.cyan * 2.0f;
    //         renderer.material.SetColor("_EmissionColor", glowColor);

    //         //Get the node that was hit and send it to reflectRay so the correct nodes will reflect the rays
    //         Transform hitNode = findClosestNode();
    //         reflectRay(hitNode);
    //     }
    //     else //otherwise...
    //     {
    //         //Stop emitting
    //         renderer.material.DisableKeyword("_EMISSION");
    //     }    
    // }

    // //Finds the closest node to the point the ray hit
    // Transform findClosestNode()
    // {
    //     //Don't know what the closest node is yet, so set to null
    //     Transform closestNode = null;
    //     //Don't know what the shortest distance is yet, so set to infinity
    //     float shortestDist = Mathf.Infinity;

    //     foreach (Transform node in nodes) //For each node in nodes...
    //     {
    //         //Get the distance from the ray hit point and the node position
    //         float distance = Vector3.Distance(incomingHitPoint, node.position);

    //         if (distance < shortestDist) //if the distance is less than the current shortest distance...
    //         {
    //             //Update the shortest distance and closest node
    //             shortestDist = distance;
    //             closestNode = node;
    //             return closestNode;
    //         }
    //     }
    // }

    // //Reflects the ray from the proper nodes of the gem
    // void reflectRay(Transform hitNode)
    // {
    //     //Container that stores hit info
    //     RaycastHit hitInfo;

    //     foreach (Transform node in nodes) //For each node in nodes...
    //     {
    //         if (node != hitNode) //If the node is not the node that was hit...
    //         {
    //             //Get the origin from where the ray will be reflected
    //             Vector3 origin = node.position;
    //             //Sets the direction of the ray so it is opposite the node origin
    //             Vector3 direction = (node.position - transform.position).normalized; //using "normalized" makes the vector a unit vector

    //             //Fire the ray
    //             if (Physics.Raycast(origin, direction, out hitInfo, maxDist)) //returns true if hit, false if miss
    //             {
    //                 //Start of line
    //                 line.SetPosition(0, origin);
    //                 //End of line
    //                 line.SetPosition(1, hitInfo.point);

    //                 if (hitInfo.collider.CompareTag("Gem")) //If the ray hit a gem...
    //                 {
    //                     //Store the hit gem
    //                     Gem1 hitGem = hitInfo.collider.GetComponent<Gem1>();

    //                     if (hitGem != null)
    //                     {
    //                         hitGem.isActive = true;
    //                         hitGem.incomingHitPoint = hitInfo.point;
    //                     }
    //                 }
    //             }
    //             else //Ray hit nothing
    //             {
    //                 line.SetPosition(0, origin);
    //                 line.SetPosition(1, origin + (direction * maxDist));
    //             } 
    //         }
    //     }
    // }
}
