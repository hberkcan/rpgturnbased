using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;

public interface ISaveable
{
    public JToken CaptureState();

    public void RestoreFromState(JToken state);
}
