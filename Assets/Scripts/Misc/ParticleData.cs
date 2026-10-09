using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleData", menuName = "ScriptableObjects/ParticleData")]
public class ParticleData : ScriptableObject
{
    public GameObject prefab;
    public enum PriorityLevel
    {
        All,
        Decreased,
        OnlyNecessary
    }
    public PriorityLevel priorityLevel;
}