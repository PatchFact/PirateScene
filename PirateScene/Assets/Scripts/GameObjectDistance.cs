using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDistance : MonoBehaviour
{
    void Update()
    {
        GameObject tavern = GameObject.FindGameObjectWithTag("Tavern");
        GameObject crab = GameObject.FindGameObjectWithTag("DancingCrab");
        
        Vector3 position = transform.position;

        float tavernDistance = CalculateDistance(position, tavern);
        float crabDistance = CalculateDistance(position, crab);

        AkSoundEngine.SetRTPCValue("tavern_distance", tavernDistance, GameObject.Find("WwiseGlobal"));
        AkSoundEngine.SetRTPCValue("crab_distance", crabDistance, GameObject.Find("WwiseGlobal"));
    }

    float CalculateDistance(Vector3 playerPosition, GameObject target) 
    {
        Vector3 diff = target.transform.position - playerPosition;
        return diff.sqrMagnitude;
    }
}
