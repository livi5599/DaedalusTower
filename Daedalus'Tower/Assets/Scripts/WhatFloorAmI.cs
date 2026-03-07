using UnityEngine;

public class WhatFloorAmI : MonoBehaviour
{
    [SerializeField] int iamFloor;
    public int myFloor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myFloor = iamFloor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
