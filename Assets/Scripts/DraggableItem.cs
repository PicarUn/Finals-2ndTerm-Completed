using UnityEngine;

public class DraggableItem : MonoBehaviour
{
    private Camera cam;
    private Vector3 offset;

    public string itemID;
    public bool isOnCraftingMat = false;

    private void Start()
    {
        cam = Camera.main;
    }

    private void OnMouseDown()
    {
        offset = transform.position - cam.ScreenToWorldPoint(Input.mousePosition);

        NPCCustomer[] npcs = FindObjectsByType<NPCCustomer>(FindObjectsSortMode.None);
        foreach (var npc in npcs)
            npc.SetColliderActive(true);

        CraftingManager.Instance.craftingMat.SetColliderActive(true);
    }

    private void OnMouseUp()
    {
        NPCCustomer[] npcs = FindObjectsByType<NPCCustomer>(FindObjectsSortMode.None);
        foreach (var npc in npcs)
            npc.SetColliderActive(false);

        CraftingManager.Instance.craftingMat.SetColliderActive(false);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0) + offset;
    }
}