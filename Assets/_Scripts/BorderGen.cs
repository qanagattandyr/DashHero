using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderGen : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject borderPrefab;
    public float camHeight;
    public float camWidth;
    void Start()
    {
        camHeight = Camera.main.orthographicSize ;
        camWidth = camHeight * Camera.main.aspect ;

        List<GameObject> borders = new List<GameObject>();
        for(int i = 0; i < 4; i++)
        {
            borders.Add(Instantiate<GameObject>(borderPrefab));
            borders[i].transform.SetParent(this.transform);
        }

        borders[0].transform.position = Vector3.up * camHeight;
        borders[0].transform.localScale = new Vector3(camWidth * 2, 0.5f, 1) * 100/64;

        borders[1].transform.position = Vector3.right * camWidth;
        borders[1].transform.localScale = new Vector3(0.5f, camHeight * 2, 1) * 100/64;

        borders[2].transform.position = Vector3.down * camHeight;
        borders[2].transform.localScale = new Vector3(camWidth * 2, 0.5f, 1) * 100/64;

        borders[3].transform.position = Vector3.left * camWidth;
        borders[3].transform.localScale = new Vector3(0.5f, camHeight * 2, 1) * 100/64;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
