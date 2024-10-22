using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : Trap
{
    [SerializeField] private List<Transform> point = new List<Transform>();
    int idx = 0;
    int sz;

    private void Start()
    {
        dmg = 5;
        sz = point.Count;
    }

    private void Update()
    {
        path();
    }

    void path()
    {
        Transform dir = point[idx];
        if(Vector2.Distance(dir.position, this.transform.position) < 0.01f)
        {
            idx++;
            if(idx == sz)
            {
                idx = 0;
            }
        }
        transform.Translate((dir.position - this.transform.position) * Time.deltaTime);  

    }

}
