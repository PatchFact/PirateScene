using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWaterCheck : MonoBehaviour
{
    bool isUnderwater = false;

    // Update is called once per frame
    void Update()
    {
        if (isUnderwater)
        {
            AkSoundEngine.SetRTPCValue("underwater", 100f, GameObject.Find("WwiseGlobal"));
        }
        else 
        {
            AkSoundEngine.SetRTPCValue("underwater", 0f, GameObject.Find("WwiseGlobal"));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Water")
        {
            isUnderwater = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            isUnderwater = false;
        }
    }
}
