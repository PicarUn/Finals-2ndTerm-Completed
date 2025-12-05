using UnityEngine;
using System.Collections;

public class DragHammer : MonoBehaviour
{
    private Vector3 offset;
    private Camera cam;
    public float hitCooldown = 0.25f;
    private bool canHit = true;

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
        offset = Vector3.zero;

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

    private void Update()
    {
        if (!canHit) return;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.1f);
        foreach (var hit in hits)
        {
            var item = hit.GetComponent<DraggableItem>();
            if (item != null && item.isOnCraftingMat)
            {
                AudioManager.Instance.PlayHammer(); //Make into comment to test because it requires the main menu screen to be active first

                StartCoroutine(HitCooldown());
                CraftingManager.Instance.RegisterHit(item);
                break;
            }
        }
    }

    IEnumerator HitCooldown()
    {
        canHit = false;
        yield return new WaitForSeconds(hitCooldown);
        canHit = true;
    }

    public void ResetHammer()
    {
        StopAllCoroutines();
        canHit = true;
    }
}

