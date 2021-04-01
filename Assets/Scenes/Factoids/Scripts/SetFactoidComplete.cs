using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFactoidComplete : MonoBehaviour
{
    public int factoidIndex = 0;
    public bool completed = false;

    private void Awake () {
        if (completed == false) {
            // AppProgression.UpdateFactoidCompleted(factoidIndex);
            AppProgression.factoidsCompleted[factoidIndex] = true;
            AppProgression.SaveGame();
            completed = true;
        }
    }
}
