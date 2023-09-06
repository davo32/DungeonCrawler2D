using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testRotator : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward,90f);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward,-90f);
        }
        
    }
}
