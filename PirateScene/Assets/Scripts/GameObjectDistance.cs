using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectDistance : MonoBehaviour
{

    public bool inTavern = false;
    public bool underwater = false;

    void Start()
    {
        //turn on both filters at start of scene
        AkSoundEngine.SetRTPCValue("tavern_LPF", 1f, GameObject.Find("WwiseGlobal"));   
        AkSoundEngine.SetRTPCValue("water_LPF", 1f, GameObject.Find("WwiseGlobal"));  
    }

    void Update()
    {
        GameObject tavern = GameObject.Find("wwise_tavern_trigger");
        GameObject crab = GameObject.FindGameObjectWithTag("DancingCrab");
        
        Vector3 position = transform.position;

        float tavernDistance = CalculateDistance(position, tavern);
        float crabDistance = CalculateDistance(position, crab);
      
        AkSoundEngine.SetRTPCValue("crab_distance", crabDistance, GameObject.Find("WwiseGlobal"));

        Debug.LogWarning("Distance To Crab:" + crabDistance);

        Debug.LogWarning("Distance To Tavern:" + tavernDistance);

        if (!inTavern) { AkSoundEngine.SetRTPCValue("tavern_distance", tavernDistance, GameObject.Find("WwiseGlobal")); }

        if (inTavern)
        {
            AkSoundEngine.SetRTPCValue("tavern_distance", 0f, GameObject.Find("WwiseGlobal")); //set to loudest            
        }
    }

    float CalculateDistance(Vector3 playerPosition, GameObject target) 
    {
        Vector3 diff = target.transform.position - playerPosition;
        return diff.sqrMagnitude;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "tavern_check")
        {
            inTavern = true;
            AkSoundEngine.SetRTPCValue("tavern_LPF", 0f, GameObject.Find("WwiseGlobal")); //turn LPF off
        }

        if (other.tag == "water_check")
        {
            underwater = true;
            AkSoundEngine.SetRTPCValue("water_LPF", 0f, GameObject.Find("WwiseGlobal")); //turn LPF off
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "tavern_check")
        {
            inTavern = false;
            AkSoundEngine.SetRTPCValue("tavern_LPF", 1f, GameObject.Find("WwiseGlobal")); //turn LPF on
        }

        if (other.tag == "water_check")
        {
            underwater = false;
            AkSoundEngine.SetRTPCValue("water_LPF", 1f, GameObject.Find("WwiseGlobal")); //turn LPF on
        }
      
    }
}
