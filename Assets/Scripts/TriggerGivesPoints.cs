using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGivesPoints : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            ClownMeter.Instance.ChangeMeter(5);
        }
    }

}
