using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    protected ScoreManager() { }

    public int Lives = 3;
    public int Level = 1;
    public int HitPoints = 3;
    public int DefaultHitPoints = 3;
    public int MaxHitPoints = 5;

    public void Reset()
    {
        Lives = 3;
        Level = 1;
        HitPoints = 3;
    }
}