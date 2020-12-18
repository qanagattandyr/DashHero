using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitGenerator : MonoBehaviour
{
    // Start is called before the first frame update
    public float spawnOffset;
    public List<GameObject> fruitPrefabs;
    public float ratio;

    private float camHeight;
    private float camWidth;

    private float prevSpawnTime;
    void Start()
    {
        camHeight = Camera.main.orthographicSize;
        camWidth = camHeight * Camera.main.aspect;

        prevSpawnTime = 0;
    }
    void fruitGen(GameObject fruit)
    {
        GameObject f = Instantiate<GameObject>(fruit);
        f.transform.SetParent(this.transform);
        float x = Random.Range(-camWidth + spawnOffset, camWidth - spawnOffset);
        float y = Random.Range(-camHeight + spawnOffset, camHeight - spawnOffset);
        f.transform.localPosition = new Vector3(x, y);

    }
    // Update is called once per frame
    void Update()
    {

        if(Time.time - prevSpawnTime > 1.0f / ratio)
        {
            prevSpawnTime = Time.time;
            fruitGen(fruitPrefabs[0]);
        }
    }
}
