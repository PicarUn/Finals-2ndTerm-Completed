using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveChars : MonoBehaviour
{
    public List<GameObject> characterPrefabs = new List<GameObject>();

    public Transform pointA;
    public Transform pointB;
    public Transform pointC;

    public float moveSpeed = 3f;
    public float spawnDelay = 0.5f;
    public float firstSpawnDelay = 1f;

    public int totalCharactersToSpawn = -1;

    private bool okGiven = false;
    private int lastIndex = -1;

    void Start()
    {
        MoveChars[] existing = FindObjectsByType<MoveChars>(FindObjectsSortMode.None);

        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(firstSpawnDelay);

        int counter = 0;

        while (totalCharactersToSpawn < 0 || counter < totalCharactersToSpawn)
        {
            okGiven = false;

            int index = Random.Range(0, characterPrefabs.Count);
            while (index == lastIndex && characterPrefabs.Count > 1)
            {
                index = Random.Range(0, characterPrefabs.Count);
            }
            lastIndex = index;

            GameObject prefab = characterPrefabs[index];
            GameObject newChar = Instantiate(prefab, pointA.position, Quaternion.identity);

            newChar.transform.SetParent(transform);

            Transform charTransform = newChar.transform;

            yield return MoveTo2D(charTransform, pointB.position);

            yield return new WaitUntil(() => okGiven == true);

            yield return MoveTo2D(charTransform, pointC.position);

            Destroy(newChar);

            counter++;

            yield return new WaitForSeconds(spawnDelay);
        }

    }

    IEnumerator MoveTo2D(Transform character, Vector2 target)
    {
        while (Vector2.Distance(character.position, target) > 0.05f)
        {
            Vector2 newPos = Vector2.MoveTowards(
                character.position,
                target,
                moveSpeed * Time.deltaTime
            );

            character.position = newPos;
            yield return null;
        }

        character.position = target;
    }

    public void CharacterOK()
    {
        okGiven = true;
    }
}