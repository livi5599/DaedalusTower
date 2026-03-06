using UnityEngine;

public class DoorwayController : MonoBehaviour
{
    [SerializeField] GameObject doorwayShimmer;
    [SerializeField] bool doorwayOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SomePublicMethod(float oneArgument)
    {
        if (!doorwayOn)
        {
            doorwayShimmer.SetActive(true);   
        }
        else
        {
            doorwayShimmer.SetActive(false);
        }
        doorwayOn = !doorwayOn;
    }
}
