using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自动删除物体
public class DestroyObj : MonoBehaviour
{
    public float timer;
    void Start()
    {
        Destroy(gameObject, timer);
    }
}
