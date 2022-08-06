using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WwiseController : MonoBehaviour
{
    public bool inTavern = false;
    public bool inCave = false;
    public bool underwater = false;

    Dictionary<string, string> checkFilterList = new Dictionary<string, string>();
    
    void Start()
    {
        //add filter names corresponding to checks to dictionary
        checkFilterList.Add("tavern_check","tavern_LPF");
        // checkFilterList.Add("cave_check","cave_LPF"); // Didn't end up needing these lmao

        //turn on filters at start of scene
        foreach (var check in checkFilterList)
        {
            AkSoundEngine.SetRTPCValue(check.Value, 1f, GameObject.Find("WwiseGlobal"));
        }
    }

    void Update()
    {
        Vector3 position = transform.position;

        GameObject tavern = GameObject.Find("wwise_tavern_trigger");
        GameObject crab = GameObject.FindGameObjectWithTag("DancingCrab");
        
        float tavernDistance = CalculateDistance(position, tavern);
        float crabDistance = CalculateDistance(position, crab);
      
        AkSoundEngine.SetRTPCValue("crab_distance", crabDistance, GameObject.Find("WwiseGlobal"));

        if (inTavern)
        {
            AkSoundEngine.SetRTPCValue("tavern_distance", 0f, GameObject.Find("WwiseGlobal")); //set to loudest            
        }
        else 
        {
            AkSoundEngine.SetRTPCValue("tavern_distance", tavernDistance, GameObject.Find("WwiseGlobal"));
        }

        // Debug.LogWarning("Distance To Crab:" + crabDistance);
        // Debug.LogWarning("Distance To Tavern:" + tavernDistance);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "cave_check") 
        {
            
        }

        if (other.tag == "water_check") 
        {
            underwater = true;
            Debug.Log("underwater");
            AkSoundEngine.PostEvent("underwater", gameObject);
        }

        SetLPF(inTavern, other, false);
        // SetLPF(inCave, other, false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "water_check") 
        {
            underwater = false;
            Debug.Log("not underwater");
            AkSoundEngine.PostEvent("above_water", gameObject);
        }

        SetLPF(inTavern, other, true);
        // SetLPF(inCave, other, true);
    }

    float CalculateDistance(Vector3 playerPosition, GameObject target) 
    {
        Vector3 diff = target.transform.position - playerPosition;
        return diff.sqrMagnitude;
    }

    void SetLPF(bool check, Collider other, bool on_off) 
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
}
