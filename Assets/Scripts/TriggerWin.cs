using CameraFading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerWin : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            CameraFade.Out(() =>
            {
                SceneManager.LoadScene(2); // satan scene
            }
            , 2f);
        }
    }

}
