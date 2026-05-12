using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class FruitSpawner : MonoBehaviour
{
    [SerializeField] FruitData[] spawnableFruits;
    [SerializeField] Transform nextPreviewPoint;
    [SerializeField] float spawnY = 4.5f;
    [SerializeField] float containerMinX = -4.0f;
    [SerializeField] float containerMaxX = 4.0f;
    [SerializeField] float dropCooldown = 1.2f;

    FruitData currentFruitData;
    FruitData nextFruitData;
    GameObject currentFruitGO;
    GameObject nextPreviewGO;
    bool isDropping;
    Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        nextFruitData = GetRandomFruit();
        SpawnCurrentFruit();
    }

    void Update()
    {
        if (isDropping || currentFruitGO == null || Mouse.current == null)
            return;

        Vector2 screenPos = Mouse.current.position.ReadValue();
        float worldX = mainCamera
            .ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, Mathf.Abs(mainCamera.transform.position.z)))
            .x;

        float radius = GetFruitRadius(currentFruitGO);
        float clampedX = Mathf.Clamp(worldX, containerMinX + radius, containerMaxX - radius);
        currentFruitGO.transform.position = new Vector3(clampedX, spawnY, 0f);

        if (Mouse.current.leftButton.wasPressedThisFrame)
            DropFruit();
    }

    void SpawnCurrentFruit()
    {
        currentFruitData = nextFruitData;
        nextFruitData = GetRandomFruit();

        currentFruitGO = Instantiate(currentFruitData.prefab, new Vector3(0f, spawnY, 0f), Quaternion.identity);
        var rb = currentFruitGO.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;

        RefreshPreview();
    }

    void RefreshPreview()
    {
        if (nextPreviewGO != null)
            Destroy(nextPreviewGO);

        if (nextPreviewPoint == null || nextFruitData.prefab == null)
            return;

        nextPreviewGO = Instantiate(nextFruitData.prefab, nextPreviewPoint.position, Quaternion.identity);
        var rb = nextPreviewGO.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;
        var col = nextPreviewGO.GetComponent<Collider2D>();
        if (col != null)
            col.enabled = false;
    }

    void DropFruit()
    {
        isDropping = true;
        var rb = currentFruitGO.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
        currentFruitGO = null;
        StartCoroutine(CooldownThenSpawn());
    }

    IEnumerator CooldownThenSpawn()
    {
        yield return new WaitForSeconds(dropCooldown);
        isDropping = false;
        SpawnCurrentFruit();
    }

    float GetFruitRadius(GameObject go)
    {
        var col = go.GetComponent<CircleCollider2D>();
        return col != null ? col.radius * go.transform.localScale.x : 0f;
    }

    FruitData GetRandomFruit() =>
        spawnableFruits[Random.Range(0, spawnableFruits.Length)];
}
