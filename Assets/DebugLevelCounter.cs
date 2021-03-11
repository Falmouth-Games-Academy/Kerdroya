using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLevelCounter : MonoBehaviour
{
    public bool Predennak,
        CapeCornwall,
        Morwenstow,
        Lansallos,
        Collifordlake,
        TheBlouth,
        Padstow,
        ParkHead,
        StAgnesHead,
        Boscatle,
        Tehidy,
        ClarrickWood;

    public void Start()
    {
        Predennak = AppProgression.levelCompleted[0];
        CapeCornwall = AppProgression.levelCompleted[1];
        Morwenstow = AppProgression.levelCompleted[2];
        Lansallos = AppProgression.levelCompleted[3];
        Collifordlake = AppProgression.levelCompleted[4];
        TheBlouth = AppProgression.levelCompleted[5];
        Padstow = AppProgression.levelCompleted[6];
        ParkHead = AppProgression.levelCompleted[7];
        StAgnesHead = AppProgression.levelCompleted[8];
        Boscatle = AppProgression.levelCompleted[9];
        Tehidy = AppProgression.levelCompleted[10];
        ClarrickWood = AppProgression.levelCompleted[11];
    }

    private void Update()
    {
        AppProgression.levelCompleted[0] = Predennak;
        AppProgression.levelCompleted[1] = CapeCornwall;
        AppProgression.levelCompleted[2] = Morwenstow;
        AppProgression.levelCompleted[3] = Lansallos;
        AppProgression.levelCompleted[4] = Collifordlake;
        AppProgression.levelCompleted[5] = TheBlouth;
        AppProgression.levelCompleted[6] = Padstow;
        AppProgression.levelCompleted[7] = ParkHead;
        AppProgression.levelCompleted[8] = StAgnesHead;
        AppProgression.levelCompleted[9] = Boscatle;
        AppProgression.levelCompleted[10] = Tehidy;
        AppProgression.levelCompleted[11] = ClarrickWood;
    }
}
