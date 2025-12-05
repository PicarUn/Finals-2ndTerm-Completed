using UnityEngine;

public class MaterialSpawner : MonoBehaviour
{
    public GameObject materialPrefab;

    private GameObject currentMaterial;

    void Start()
    {
        SpawnMaterial();
    }

    public void SpawnMaterial()
    {
        currentMaterial = Instantiate(materialPrefab, transform.position, Quaternion.identity);

        CraftingMaterial mat = currentMaterial.GetComponent<CraftingMaterial>();
        if (mat != null)
            mat.SetSpawner(this);
    }

    public void OnMaterialConsumed()
    {
        SpawnMaterial();
    }
}
