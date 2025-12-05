using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class CraftingRecipe
{
    public List<string> requiredItems = new List<string>();

    public GameObject resultPrefab;
    public int requiredHits;
}
