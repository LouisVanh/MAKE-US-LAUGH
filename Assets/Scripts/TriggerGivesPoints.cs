using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGivesPoints : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) // I DO NOT CARE I ADDED THE BALL AS A PLAYER FUCK YOU
        {
            ClownMeter.Instance.ChangeMeter(5);
            // TODO CLOWN CHEER
        }
    }

}
