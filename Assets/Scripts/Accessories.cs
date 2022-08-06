using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessories : MonoBehaviour
{
    public List<GameObject> eyesSerializedIsh;
    public List<GameObject> legsSerializedIsh;
    public static List<GameObject> eyes;
    public static List<GameObject> legs;

    private void Awake()
    {
        //eyes = new List<GameObject>();
        eyes = eyesSerializedIsh;
        //legs = new List<GameObject>();
        legs = legsSerializedIsh;
    }

    public static GameObject RandomEyes()
    {
        int rand = Random.Range(0, 2);
        return eyes[rand];
    }
    public static GameObject RandomLegs()
    {
        int rand = Random.Range(0, 2);
        return legs[rand];
    }
}
