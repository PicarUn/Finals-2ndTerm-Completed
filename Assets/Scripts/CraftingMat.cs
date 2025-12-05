using UnityEngine;
using System.Collections.Generic;

public class CraftingMat : MonoBehaviour
{
    public List<DraggableItem> itemsOnMat = new List<DraggableItem>();

    private BoxCollider2D col;

    private void Awake()
    {
        col = GetComponent<BoxCollider2D>();
        col.enabled = false;
    }

    public void SetColliderActive(bool state)
    {
        if (col != null)
            col.enabled = state;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DraggableItem item = other.GetComponent<DraggableItem>();
        if (item != null)
        {
            item.isOnCraftingMat = true;
            itemsOnMat.Add(item);
            CraftingManager.Instance.CheckForRecipe();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        DraggableItem item = other.GetComponent<DraggableItem>();
        if (item != null && itemsOnMat.Contains(item))
        {
            item.isOnCraftingMat = false;
            itemsOnMat.Remove(item);
        }
    }
}
