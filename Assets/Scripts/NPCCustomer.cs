using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NPCCustomer : MonoBehaviour
{
    [Header("Request Settings")]
    public GameObject[] possibleRequests;
    public string requestedItemID;

    [Header("Request Icon")]
    public Transform iconAnchor;
    public Image requestIconUI;
    public Sprite defaultIconSprite;

    [Header("Patience Settings")]
    public Slider patienceBar;
    public float maxPatience = 10f;
    private float currentPatience;

    private MoveChars moveChars;
    private BoxCollider2D npcCollider;
    private Camera cam;

    private bool requestSatisfied = false;

    private void Awake()
    {
        npcCollider = GetComponent<BoxCollider2D>();
        npcCollider.enabled = false;

        moveChars = FindAnyObjectByType<MoveChars>();
    }

    private void Start()
    {
        cam = Camera.main;
        if (requestIconUI == null)
            requestIconUI = GameObject.Find("RequestIconUI").GetComponent<Image>();

        if (patienceBar == null)
            patienceBar = GameObject.Find("PatienceBar").GetComponent<Slider>();

        PickRandomRequest();
        SetupUI();
        StartCoroutine(PatienceRoutine());
    }

    void PickRandomRequest()
    {
        int index = Random.Range(0, possibleRequests.Length);
        var prefab = possibleRequests[index];

        var item = prefab.GetComponent<DraggableItem>();
        requestedItemID = item.itemID;

        if (requestIconUI != null)
            requestIconUI.sprite = prefab.GetComponent<SpriteRenderer>().sprite;
    }

    void SetupUI()
    {
        currentPatience = maxPatience;

        if (patienceBar != null)
        {
            patienceBar.maxValue = maxPatience;
            patienceBar.value = maxPatience;
        }
    }

    IEnumerator PatienceRoutine()
    {
        while (currentPatience > 0 && requestSatisfied == false)
        {
            currentPatience -= Time.deltaTime;

            if (patienceBar != null)
                patienceBar.value = currentPatience;

            yield return null;
        }

        if (!requestSatisfied)
        {
            PlayerLives.Instance.LoseLife();
        }

        moveChars.CharacterOK();
    }

    public void SetColliderActive(bool state)
    {
        npcCollider.enabled = state;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        DraggableItem item = other.GetComponent<DraggableItem>();
        if (item == null) return;

        if (item.itemID == requestedItemID)
        {
            requestSatisfied = true;

            Destroy(item.gameObject);

            StartCoroutine(GrantSuccess());
        }
    }

    IEnumerator GrantSuccess()
    {
        yield return new WaitForSeconds(0.3f);
        moveChars.CharacterOK();
    }

    private void LateUpdate()
    {
        if (cam == null || iconAnchor == null) return;

        if (requestIconUI != null)
        {
            requestIconUI.transform.position =
                cam.WorldToScreenPoint(iconAnchor.position);
        }

        if (patienceBar != null)
        {
            patienceBar.transform.position =
                cam.WorldToScreenPoint(iconAnchor.position + new Vector3(0, 0.5f, 0));
        }
    }
}
