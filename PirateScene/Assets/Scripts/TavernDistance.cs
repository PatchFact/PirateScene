using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TavernDistance : MonoBehaviour
{
    void Update()
    {
        GameObject tavern = GameObject.FindGameObjectWithTag("Tavern");
        Vector3 position = transform.position;

        Vector3 diff = tavern.transform.position - position;
        float currentDistance = diff.sqrMagnitude;

        AkSoundEngine.SetRTPCValue("tavern_distance", currentDistance, GameObject.Find("WwiseGlobal"));
    }
}
