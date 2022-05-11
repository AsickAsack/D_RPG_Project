using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
public class CameraWalk : MonoBehaviour
{
    public PlayableDirector playable;
    public Camera zzcamera;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        { 
        playable.gameObject.SetActive(true);
        playable.Play();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            playable.gameObject.SetActive(true);
            playable.Stop();
            Camera.main.gameObject.SetActive(false);
            zzcamera.gameObject.SetActive(true);

        }
    }
}
