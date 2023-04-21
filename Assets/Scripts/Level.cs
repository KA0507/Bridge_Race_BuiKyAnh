using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Stage[] stages;
    
    public Stage[] Stages { get { return stages; } }
}
