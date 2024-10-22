using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform target;

    void Update()
    {
        if(target == null)
        {
            return;
        }
        this.transform.position = new Vector3(target.position.x , target.position.y , -10);
    }
}
