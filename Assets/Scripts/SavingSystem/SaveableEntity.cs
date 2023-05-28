using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public class SaveableEntity : MonoBehaviour
{
    [SerializeField] string uniqueIdentifier = "";

    public string GetUniqueIdentifier()
    {
        return uniqueIdentifier;
    }

    public JToken CaptureState()
    {
        JObject state = new JObject();
        IDictionary<string, JToken> stateDict = state;
        foreach (ISaveable saveable in GetComponents<ISaveable>())
        {
            JToken token = saveable.CaptureState();
            string component = saveable.GetType().ToString();
            //Debug.Log($"{name} Capture {component} = {token}");
            stateDict[saveable.GetType().ToString()] = token;
        }
        return state;
    }

    public void RestoreFromState(JToken s)
    {
        JObject state = s.ToObject<JObject>();
        IDictionary<string, JToken> stateDict = state;
        foreach (ISaveable saveable in GetComponents<ISaveable>())
        {
            string component = saveable.GetType().ToString();
            if (stateDict.ContainsKey(component))
            {

                //Debug.Log($"{name} Restore {component} =>{stateDict[component]}");
                saveable.RestoreFromState(stateDict[component]);
            }
        }
    }
}
