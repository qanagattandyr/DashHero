using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stopper : MonoBehaviour
{

   
    private void Update()
    {
        transform.Rotate(Vector3.forward, Time.deltaTime * 180);
    }
}
