using UnityEngine;

public class Water : MonoBehaviour
{

    public float maxWaterHeight;
    public float waterBaseHeight;
    public bool isFilling = false; 
    public float drainSpeed = 0.1f;
    
    public float fillSpeed = 0.1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waterBaseHeight = transform.position.y;
        maxWaterHeight = waterBaseHeight + 5;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFilling)
        {
            if(transform.position.y < maxWaterHeight)
            {
                transform.position += Vector3.up * fillSpeed * Time.deltaTime;
            }
        } 
        else
        {
            if(transform.position.y > waterBaseHeight)
            {
                transform.position -= Vector3.up * drainSpeed * Time.deltaTime;
            }
        }
        
    }
}
