using UnityEngine;

public class Gem1 : MonoBehaviour
{
    //Tracks if the gem is active
    public bool isActive = false;
    //Used to store the renderer attached to this gem
    Renderer renderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Sets the renderer
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Ensures gem is not active unless being hit by ray
        isActive = false;

        if (isActive) //if the gem is active...
        {
            //Emit the color
            renderer.material.EnableKeyword("_EMISSION");
            Color glowColor = Color.cyan * 2.0f;
            renderer.material.SetColor("_EmissionColor", glowColor);
        }
        else //otherwise...
        {
            //Stop emitting
            renderer.material.DisableKeyword("_EMISSION");
        }    
    }
}
