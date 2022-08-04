using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDistance : MonoBehaviour
{
    public bool inTavern = false;
    public bool inCave = false;

    Dictionary<string, string> checkFilterList = new Dictionary<string, string>();
    
    void Start()
    {
        //add filter names corresponding to checks to dictionary
        checkFilterList.Add("tavern_check","tavern_LPF");
        checkFilterList.Add("water_check","water_LPF");
        checkFilterList.Add("cave_check","cave_LPF");

        //turn on filters at start of scene
        AkSoundEngine.SetRTPCValue("tavern_LPF", 1f, GameObject.Find("WwiseGlobal"));   
        AkSoundEngine.SetRTPCValue("water_LPF", 1f, GameObject.Find("WwiseGlobal"));  
        AkSoundEngine.SetRTPCValue("cave_LPF", 1f, GameObject.Find("WwiseGlobal"));
    }

    void Update()
    {
        Vector3 position = transform.position;

        GameObject tavern = GameObject.Find("wwise_tavern_trigger");
        GameObject crab = GameObject.FindGameObjectWithTag("DancingCrab");
        
        float tavernDistance = CalculateDistance(position, tavern);
        float crabDistance = CalculateDistance(position, crab);
      
        AkSoundEngine.SetRTPCValue("crab_distance", crabDistance, GameObject.Find("WwiseGlobal"));

        if (!inTavern) { AkSoundEngine.SetRTPCValue("tavern_distance", tavernDistance, GameObject.Find("WwiseGlobal")); }

        if (inTavern)
        {
            AkSoundEngine.SetRTPCValue("tavern_distance", 0f, GameObject.Find("WwiseGlobal")); //set to loudest            
        }

        // Debug.LogWarning("Distance To Crab:" + crabDistance);

        // Debug.LogWarning("Distance To Tavern:" + tavernDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckInside(inTavern, other, false);
        CheckInside(inCave, other, false);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckInside(inTavern, other, true);
        CheckInside(inCave, other, true);
    }

    float CalculateDistance(Vector3 playerPosition, GameObject target) 
    {
        Vector3 diff = target.transform.position - playerPosition;
        return diff.sqrMagnitude;
    }

    void CheckInside(bool check, Collider other, bool on_off) 
    {
        float rtcpVal = on_off ? 1f : 0f;

        if (checkFilterList.ContainsKey(other.tag)) 
        {
            AkSoundEngine.SetRTPCValue(checkFilterList[other.tag], rtcpVal, GameObject.Find("WwiseGlobal"));
            check = !on_off;
        }
        else 
        {
            check = on_off;
        }
    }

    //*Old Implementation

    // void CheckOutside(bool check, Collider other) 
    // {
    //     if (checkFilterList.ContainsKey(other.tag)) 
    //     {
    //         AkSoundEngine.SetRTPCValue(checkFilterList[other.tag], 1f, GameObject.Find("WwiseGlobal"));
    //         check = false;
    //     }
    //     else 
    //     {
    //         check = true;
    //     }
    // }
}
