using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnvironmentInfluence //interface to implement on all drops which affect the environmentScore
{
    public float influence { get; set; }

}
