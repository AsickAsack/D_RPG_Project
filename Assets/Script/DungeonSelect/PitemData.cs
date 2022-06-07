using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitemData : MonoBehaviour

{
    public static PitemData instance;

    private void Awake()
    {
        instance = this;
    }

    public List<Pitem> itemDB = new List<Pitem>();
}

