using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolve : MonoBehaviour
{
    public float fade;
    private Material mat;
    public bool dissolving = false;
    // Start is called before the first frame update
    void Start()
    {
        fade = 1;
        mat = gameObject.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Jump"))
            dissolving = !dissolving;
        
        if (dissolving)
            fade += Time.deltaTime;
        else
            fade -= Time.deltaTime;
        fade = Mathf.Clamp(fade, 0, 1);
        mat.SetFloat("_Fade", fade);
    }
}
