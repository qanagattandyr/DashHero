using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class Fruits : MonoBehaviour
{
    [Header("Inspector")]
    public float speed;
    public ParticleSystem explosion;
    public GameObject aura;
    public float hitRange = 1f;
    public float lifeTime;
    public float outerRadius;
    public float innerRadius;
   
    [Header("Dynamic")]
  
    public bool isKilled = false;
    public float del;
    public void diePlease()
    {
        if(isKilled)
        {
            GameObject go = Instantiate<GameObject>(explosion.gameObject);
            go.transform.position = transform.position;
            go.GetComponent<ParticleSystem>().Play();
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
            GameManager.S.lifeDecrease();
        }
    }

    private void Awake()
    {
        del = (outerRadius - innerRadius) / lifeTime;
        Movement.S.fruitList.Add(gameObject);
    }
    private void OnDestroy()
    {
        Movement.S.fruitList.Remove(gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        outerRadius -= Time.deltaTime * del;
        aura.transform.localScale = Vector3.one * outerRadius;

        if(outerRadius <= innerRadius)
        {
            diePlease();
        }
    }
}
