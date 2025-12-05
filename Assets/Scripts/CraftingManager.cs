using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    public List<CraftingRecipe> recipes = new List<CraftingRecipe>();

    public CraftingMat craftingMat;

    private CraftingRecipe activeRecipe = null;
    private int currentHits = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void CheckForRecipe()
    {
        var items = craftingMat.itemsOnMat;

        if (items.Count == 0)
        {
            activeRecipe = null;
            return;
        }

        List<string> matIDs = items.Select(i => i.itemID).ToList();

        foreach (var recipe in recipes)
        {
            if (recipe.requiredItems.Count != matIDs.Count)
                continue;

            bool match =
                !recipe.requiredItems.Except(matIDs).Any() &&
                !matIDs.Except(recipe.requiredItems).Any();

            if (match)
            {
                if (activeRecipe != recipe)
                {
                    activeRecipe = recipe;
                    currentHits = 0;

                    activeRecipe.requiredHits = Random.Range(1, 5);
                    Debug.Log($"New Recipe Formed! Needs {activeRecipe.requiredHits} hits.");
                }

                return;
            }
        }

        activeRecipe = null;
    }

    public void RegisterHit(DraggableItem hitItem)
    {
        if (activeRecipe == null) return;

        currentHits++;
        Debug.Log($"Hit {currentHits}/{activeRecipe.requiredHits}");

        if (currentHits >= activeRecipe.requiredHits)
        {
            CraftItem();
        }
    }

    void CraftItem()
    {
        var items = new List<DraggableItem>(craftingMat.itemsOnMat);

        foreach (var i in items)
        {
            if (i != null)
            {
                CraftingMaterial mat = i.GetComponent<CraftingMaterial>();
                if (mat != null)
                    mat.Consume();
                else
                    Destroy(i.gameObject);
            }
        }

        craftingMat.itemsOnMat.Clear();
        Instantiate(activeRecipe.resultPrefab, craftingMat.transform.position, Quaternion.identity);

        activeRecipe = null;
    }
}
