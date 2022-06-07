using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunAnim : MonoBehaviour
{

    public bool EndCinema = false;

    public void RunWith()
    {
        EndCinema = true;
        Debug.Log(EndCinema);
    }
}
