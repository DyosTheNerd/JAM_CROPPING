using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPicker : MonoBehaviour
{
    private double accumlatedWeights;
    private System.Random random = new System.Random();

    [SerializeField] private ObjectReference[] objectReferences;

    private void Awake()
    {
        CalculateWeights();
    }

    public GameObject GetRandomObject()
    {
        if (objectReferences.Length == 0)
        {
            Debug.LogWarning("List of Objects is empty.");
            return null;
        }

        return objectReferences[GetRandomIndex()].prefab;
    }

    private int GetRandomIndex()
    {
        double r = random.NextDouble() * accumlatedWeights;

        Debug.Log("RANDOM IS: " + r + " | " + accumlatedWeights);

        for (int i = 0; i < objectReferences.Length; i++)
            if (objectReferences[i]._weight >= r)
                return i;

        return 0;
    }

    private void CalculateWeights()
    {
        accumlatedWeights = 0f;

        foreach (ObjectReference obj in objectReferences)
        {
            accumlatedWeights += obj.Chance;
            obj._weight = accumlatedWeights;
        }
    }
}

[Serializable]
public class ObjectReference
{
    public GameObject prefab;
    [Range(0f, 100f)] public float Chance = 100f;
    [HideInInspector] internal double _weight;
}