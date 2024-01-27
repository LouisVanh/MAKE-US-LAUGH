using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{



    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        //TEST CUBE TO SEE IF MIC IS WORKING
        transform.localScale = new Vector3(MicInput.MicLoudness + 1, MicInput.MicLoudness + 1, MicInput.MicLoudness + 1);
        ClownMeter.Instance.ChangeMeter(MicInput.MicLoudness/20);

    }
}