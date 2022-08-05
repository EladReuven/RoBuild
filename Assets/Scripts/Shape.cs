using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shape : MonoBehaviour
{
    [SerializeField] Transform topPoint;
    [SerializeField] Transform bottomPoint;

    public Transform GetTopPoint()
    {
        return topPoint;
    }
    public Transform GetBottomPoint()
    {
        return bottomPoint;
    }
}
