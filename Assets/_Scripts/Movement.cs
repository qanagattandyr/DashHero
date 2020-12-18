using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    [Header("Inspector")]
    public bool joyStickStyle;
    public float dashPower;
    public float hitRange;
    public GameObject stopperPrefab;
    public LineRenderer line;
    public List<Image> X;

    [Header("Dynamic")]
    public List<GameObject> fruitList;
    public bool aimingMode = false;
    public bool flyingMode = false;
    public static Movement S;
    public List<Fruits> cuttingFruit;
    Vector3 position;
    Vector3 startPosition;
    Vector3 aimingDirection;
    Camera main;
    GameObject stopper;
    public int misses = 0;
    void Start()
    {
        main = Camera.main;
        S = this;
        cuttingFruit = new List<Fruits>();
        //QualitySettings.vSyncCount = 1;
        //Application.targetFrameRate = 15;
        misses = 0;
    }




    ///<summary>
    ///Returns true if line between A and B cuts through point T with radius R
    /// </summary>
    public bool ifCuts(Vector3 A, Vector3 B, Vector3 T, float R)
    {
        Vector3 targetAim = T - A;

        float x1 = targetAim.x,
                y1 = targetAim.y,
                x2 = B.x - A.x,
                y2 = B.y - A.y;
        float coef = (y1 * y2 + x1 * x2) / (x2 * x2 + y2 * y2);
        float x_inter = x2 * coef;
        float y_inter = y2 * coef;


        float distance = Mathf.Sqrt(Mathf.Pow(x1 - x_inter, 2) + Mathf.Pow(y1 - y_inter, 2));

        bool hitX = Mathf.Max(B.x, A.x) - T.x > R
                 && Mathf.Min(B.x, A.x) - T.x < R;
        bool hitY = Mathf.Max(B.y, A.y) - T.y > R
                 && Mathf.Min(B.y, A.y) - T.y < R;
        bool hit = (hitX || hitY) && distance < R;

        return hit;
    }

    void aimDown()
    {
        startPosition = position;
        stopper = Instantiate<GameObject>(stopperPrefab);
        line.positionCount = 2;

    }

    void aimOn()
    {
        aimingMode = true;
        if (joyStickStyle)
        {
            aimingDirection = startPosition - position;
            stopper.transform.position = transform.position + aimingDirection;
        }
        else
        {
            aimingDirection = position - transform.position;
            stopper.transform.position = position;
        }

        float angle = Mathf.Atan2(aimingDirection.y, aimingDirection.x);
        this.transform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg - 90);

        line.SetPosition(0, transform.position);
        line.SetPosition(1, stopper.transform.position);
    }

    void aimUp()
    {
        aimingMode = false;
        flyingMode = true;
        Destroy(stopper);

        line.positionCount = 0;

        foreach (GameObject frt in fruitList)
        {
            if (ifCuts(transform.position, stopper.transform.position, frt.transform.position, hitRange))
            {
                cuttingFruit.Add(frt.GetComponent<Fruits>());
            }
        }
        if(cuttingFruit.Count == 0)
        {
            X[misses].enabled = true;
            misses++;
            if(misses > 2)
            {
                GameManager.S.GameOver();
                print("MISSED");
            }
        }
        if(cuttingFruit.Count > 1)
        {
            for(int i = 0; i < cuttingFruit.Count - 1; i++)
            {
                if(misses > 0)
                {
                    misses--;
                    X[misses].enabled = false;
                }

            }
        }
        transform.position = position;
    }
    void controller()
    {
        //MousePosition
        Vector3 pos = Input.mousePosition;
        pos = main.ScreenToWorldPoint(pos);
        position = new Vector3(pos.x, pos.y, 0);
        if (Input.GetMouseButtonDown(0))
        {
            aimDown();
        }
        if (Input.GetMouseButton(0))
        {
            aimOn();
        }
        if (Input.GetMouseButtonUp(0))
        {
            aimUp();
        }

        //SlowMotion
        if (Input.GetMouseButton(1))
        {
            Time.timeScale = 0.1f;
        }
        if (Input.GetMouseButtonUp(1))
        {
            Time.timeScale = 1f;
        }
    }
    
    
    
    void Update()
    {
        //pc controller
        if (!GameManager.S.gameOver)
        {
            controller();
        }
        for (int i = cuttingFruit.Count - 1; i >= 0; i-- )
        {
            if(!cuttingFruit[i].isKilled)
            {
                cuttingFruit[i].isKilled = true;
                cuttingFruit[i].diePlease();
                cuttingFruit.Remove(cuttingFruit[i]);
            }
        }

    }
}
