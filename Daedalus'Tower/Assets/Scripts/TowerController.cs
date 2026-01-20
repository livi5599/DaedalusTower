using UnityEngine;
using System.Collections;

public class TowerController : MonoBehaviour
{
    public GameObject[] towerSections;
    private bool[] sectionIsRotating;
    private int sectionIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sectionIsRotating = new bool[towerSections.Length];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !sectionIsRotating[sectionIndex])
        {
            StartCoroutine(RotateLeft(towerSections[sectionIndex], 90f, 2f, sectionIndex));
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !sectionIsRotating[sectionIndex])
        {
            StartCoroutine(RotateRight(towerSections[sectionIndex], 90f, 2f, sectionIndex));
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Debug.Log("Up arrow pressed");
            sectionIndex -= 1;
            Debug.Log("sectionIndex = " + sectionIndex);
            if (sectionIndex < 0)
            {
                sectionIndex = 2;
                Debug.Log("sectionIndex < 0, so sectionIndex = " + sectionIndex);
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Debug.Log("Down arrow pressed");
            sectionIndex += 1;
            Debug.Log("sectionIndex = " + sectionIndex);
            if (sectionIndex > 2)
            {
                sectionIndex = 0;
                Debug.Log("sectionIndex > 2, so sectionIndex = " + sectionIndex);
            }
        }
    }

    IEnumerator RotateLeft(GameObject towerSection, float angle, float duration, int localIndex)
    {
        float timeElapsed = 0;
        Quaternion currentRotation = towerSection.transform.rotation;
        Quaternion rotationDelta = Quaternion.Euler(0, angle, 0);
        Quaternion endRotation = currentRotation * rotationDelta;

        sectionIsRotating[localIndex] = true;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            float clampedT = Mathf.Clamp01(t);

            towerSection.transform.rotation = Quaternion.Slerp(currentRotation, endRotation, clampedT);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        towerSection.transform.rotation = endRotation;
        sectionIsRotating[localIndex] = false;
    }
    
    IEnumerator RotateRight(GameObject towerSection, float angle, float duration, int localIndex)
    {
        float timeElapsed = 0;
        Quaternion currentRotation = towerSection.transform.rotation;
        Quaternion rotationDelta = Quaternion.Euler(0, -angle, 0);
        Quaternion endRotation = currentRotation * rotationDelta;

        sectionIsRotating[localIndex] = true;

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration;
            float clampedT = Mathf.Clamp01(t);

            towerSection.transform.rotation = Quaternion.Slerp(currentRotation, endRotation, clampedT);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        towerSection.transform.rotation = endRotation;
        sectionIsRotating[localIndex] = false;
    }
}
