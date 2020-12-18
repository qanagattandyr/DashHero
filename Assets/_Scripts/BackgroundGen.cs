using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGen : MonoBehaviour
{
    // Start is called before the first frame update
    public int width;
    public int height;
    public float wid;
    public float hei;

    public GameObject gridPrefab;
    void Start()
    {
        for(int i = -width; i < width; i++)
        {
            for(int j = -height; j < height; j++)
            {
                GameObject grid = Instantiate<GameObject>(gridPrefab);
                grid.transform.position = new Vector2(wid *i , hei* j);
                grid.transform.SetParent(this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
