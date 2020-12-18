using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControll : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 position;
    Camera main;
    void Start()
    {
        main = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Input.mousePosition;
            pos = main.ScreenToWorldPoint(pos);
            print(pos);
            position = new Vector3(pos.x, pos.y, 0);
        
            transform.position = position;  
        }
    }
}
