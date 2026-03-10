using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using UnityEngine;
using UnityEngine.Timeline;

public class EConductor : MonoBehaviour
{
    //Maximum distance the ray can travel
    public float maxDist = 10f;
    //Tracks if the gem is active
    public bool isActive = false;
    //Tracks if non-reflective object is active
    Activate active;

    //Toggles to turn different gem faces on/off
    [SerializeField] public bool FFActive;
    [SerializeField] public bool BFActive;
    [SerializeField] public bool RFActive;
    [SerializeField] public bool LFActive;

    //Stores the point where the ray hit the gem
    public Vector3 incomingHitPoint;
    //Stores the possible hit points/nodes of the gem
    public Transform[] nodes;
    //Used to store the renderer attached to this gem
    public Renderer renderer;

    //Caches the renderers for efficiency
    private Dictionary<Transform, LineRenderer> worldLines = new Dictionary<Transform, LineRenderer>();
    private Dictionary<Transform, LineRenderer> mmLines = new Dictionary<Transform, LineRenderer>();

    //look through gem/hierarchy to find which nodes have a minimap beam inside
    void Start()
    {
        renderer = GetComponentInChildren<Renderer>();

        //one-time search
        foreach (Transform node in nodes)
        {
            //If the node has already been processed, skip
            if (worldLines.ContainsKey(node)) continue;
            //Store world node line
            worldLines.Add(node, node.GetComponent<LineRenderer>());

            //Store minimap line (need to search children of current node)
            LineRenderer mmBeam = null;

            //Finds component anywhere in the node's hierarchy
            LineRenderer[] allLines = node.GetComponentsInChildren<LineRenderer>(true); //true means to search through every child, even if the object/parent is inactive
            foreach (LineRenderer l in allLines)
            {
                //if the line is part of the minimap beam/layer...
                if (l.gameObject.name == "mmBeam" || l.gameObject.layer == LayerMask.NameToLayer("MiniMap"))
                {
                    //set the reflected beam to be that minimap line
                    mmBeam = l;
                    break;
                }
            }
            //if mmLines doesn't contain the node...
            if (!mmLines.ContainsKey(node))
            {
                //add the node to mmLines
                mmLines.Add(node, mmBeam);
            }
        }
    }

    void Update()
    {
        if (isActive) //if the gem is active...
        {
            //Emit the color
            renderer.material.EnableKeyword("_EMISSION");
            Color glowColor = Color.cyan * 2.0f;
            renderer.material.SetColor("_EmissionColor", glowColor);

            //Get the node that was hit and send it to reflectRay so the correct nodes will reflect the rays
            Transform hitNode = findClosestNode();
            reflectRay(hitNode);
        }
        else //otherwise...
        {
            //Stop emitting
            renderer.material.DisableKeyword("_EMISSION");
            //Disable all lines
            ResetAllLines();
        }

        //Ensures gem is not active unless being hit by ray
        isActive = false; 
    }

    //Disables both world and minimap lines
    void ResetAllLines()
    {
        foreach (Transform node in nodes)
        {
            if (worldLines[node] != null) worldLines[node].enabled = false;
            if (mmLines[node] != null) mmLines[node].enabled = false;
        }
    }

    //Reflects the ray from the proper nodes of the gem
    void reflectRay(Transform hitNode)
    {
        //Container that stores hit info
        RaycastHit hitInfo;

        foreach (Transform node in nodes) //For each node in nodes...
        {
            LineRenderer nodeLine = worldLines[node];
            LineRenderer mmLine = mmLines[node];

            //Tracks if face should be firing or not
            bool fire = false;

            //if the current node is the hit node...
            if (node == hitNode)
            {
                //ensures reflected node doesn't reflect from the face that was hit
                if (nodeLine != null) nodeLine.enabled = false;
                if (mmLine != null) mmLine.enabled = false;
                continue;
            }

            //Checks if the current face should fire
            if ((node == nodes[0] && FFActive) || (node == nodes[1] && BFActive) || (node == nodes[2] && RFActive) || (node == nodes[3] && LFActive))
            {
                fire = true;
            }

            if (fire) //if the ray should be fired...
            {
                //Enable the node line
                if (nodeLine != null) nodeLine.enabled = true;
                if (mmLine != null) mmLine.enabled = true;

                //Get the origin from where the ray will be reflected
                Vector3 origin = node.position;
                //Sets the direction of the ray so it is opposite the node origin
                Vector3 direction = (node.position - transform.position);

                //Keeps the ray horizontal
                direction.y = 0;

                direction = direction.normalized; //using "normalized" makes the vector a unit vector

                //Fire the ray
                if (Physics.Raycast(origin, direction, out hitInfo, maxDist)) //returns true if hit, false if miss
                {
                    //Update the lines
                    UpdateLine(nodeLine, mmLine, origin, hitInfo.point);

                    if (hitInfo.collider.CompareTag("Gem")) //If the ray hit a gem...
                    {
                        //Store the hit gem
                        EConductor hitGem = hitInfo.collider.GetComponent<EConductor>();

                        if (hitGem != null)
                        {
                            hitGem.isActive = true;
                            hitGem.incomingHitPoint = hitInfo.point;
                        }
                    }
                    else if (hitInfo.collider.CompareTag("Conductor"))
                    {
                        activate(hitInfo);
                    }
                }
                else //Ray hit nothing
                {
                    UpdateLine(nodeLine, mmLine, origin, origin + (direction * maxDist));
                }
            }
            else //node is not fired, so ensure both world and minimap lines are disabled
            {
                if (nodeLine != null) nodeLine.enabled = false;
                if (mmLine != null) mmLine.enabled = false;
            }
        }
    }

    //Helper that keeps both world and minimap lines in sync
    void UpdateLine(LineRenderer world, LineRenderer mm, Vector3 start, Vector3 end)
    {
        //Update the world line
        if (world != null)
        {
            world.SetPosition(0, start);
            world.SetPosition(1, end);
        }
        //Update the minimap line
        if (mm != null)
        {
            mm.SetPosition(0, start);
            mm.SetPosition(1, end);
        }
    }

    //Finds the closest node to the point the ray hit
    Transform findClosestNode()
    {
        //Don't know what the closest node is yet, so set to null
        Transform closestNode = null;
        //Don't know what the shortest distance is yet, so set to infinity
        float shortestDist = Mathf.Infinity;

        foreach (Transform node in nodes) //For each node in nodes...
        {
            //Get the distance from the ray hit point and the node position
            float distance = Vector3.Distance(incomingHitPoint, node.position);

            if (distance < shortestDist) //if the distance is less than the current shortest distance...
            {
                //Update the shortest distance and closest node
                shortestDist = distance;
                closestNode = node;
            }
        }
        //Return the updated closest node
        return closestNode;
    }

    void activate(RaycastHit hitInfo)
    {
        //get Activate script on object that was hit
        Activate target = hitInfo.collider.GetComponent<Activate>();

        if (target != null) //if the target is valid...
        {
            //Increment the beam count on the object   
            target.beamCount++;
        }   
    }
}
