using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDistance : MonoBehaviour
{
    public bool inTavern = false;
    public bool underwater = false;
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
        CheckInside(inTavern, other);
        CheckInside(underwater, other);
        CheckInside(inCave, other);
    }

    private void OnTriggerExit(Collider other)
    {
        CheckOutside(inTavern, other);
        CheckOutside(underwater, other);
        CheckOutside(inCave, other);
    }

    float CalculateDistance(Vector3 playerPosition, GameObject target) 
    {
        Vector3 diff = target.transform.position - playerPosition;
        return diff.sqrMagnitude;
    }

    void CheckInside(bool check, Collider other) 
    {
        if (checkFilterList.ContainsKey(other.tag)) 
        {
            AkSoundEngine.SetRTPCValue(checkFilterList[other.tag], 0f, GameObject.Find("WwiseGlobal"));
            check = true;
        }
        else 
        {
            check = false;
        }
    }

    void CheckOutside(bool check, Collider other) 
    {
        if (checkFilterList.ContainsKey(other.tag)) 
        {
            AkSoundEngine.SetRTPCValue(checkFilterList[other.tag], 1f, GameObject.Find("WwiseGlobal"));
            check = false;
        }
        else 
        {
            check = true;
        }
    }
}
