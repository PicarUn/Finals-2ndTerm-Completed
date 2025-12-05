using UnityEngine;

public class CraftingMaterial : MonoBehaviour
{
    private MaterialSpawner spawner;

    public void SetSpawner(MaterialSpawner s)
    {
        spawner = s;
    }

    public void Consume()
    {
        if (spawner != null)
            spawner.OnMaterialConsumed();

        Destroy(gameObject);
    }
}
