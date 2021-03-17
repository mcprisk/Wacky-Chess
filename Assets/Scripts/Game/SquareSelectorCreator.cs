using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareSelectorCreator : MonoBehaviour
{
    [SerializeField] private Material freeSquareMaterial;
    [SerializeField] private Material opponentSquareMaterial;
    [SerializeField] private GameObject selectorPrefab;
    private List<GameObject> instantiatedSelectors = new List<GameObject>();

    public void ShowSelection(Dictionary<Tuple<Vector3, Quaternion>, bool> squareData)
    {
        ClearSelections();
        foreach (var data in squareData)
        {
            GameObject selector = Instantiate(selectorPrefab, data.Key.Item1, data.Key.Item2);
            instantiatedSelectors.Add(selector);

            foreach (var setter in selector.GetComponentsInChildren<MaterialSetter>())
            {
                setter.SetSingleMaterial(data.Value ? freeSquareMaterial : opponentSquareMaterial);
            }
        }
    }

    public void ClearSelections()
    {
        foreach (var selector in instantiatedSelectors)
        {
            Destroy(selector.gameObject);
        }
        instantiatedSelectors.Clear();
    }
}
