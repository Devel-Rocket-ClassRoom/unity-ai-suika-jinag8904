using System.Collections.Generic;
using UnityEngine;

public class DeathLineChecker : MonoBehaviour
{
    [SerializeField] float gameOverDelay = 3.0f;

    readonly Dictionary<int, float> fruitTimers = new();
    readonly HashSet<int> currentFrameIDs = new();
    readonly List<int> staleIDs = new();
    BoxCollider2D zoneCollider;
    bool triggered;

    void Awake()
    {
        zoneCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (triggered)
            return;

        currentFrameIDs.Clear();

        Vector2 center = (Vector2)transform.position + zoneCollider.offset;
        var cols = Physics2D.OverlapBoxAll(center, zoneCollider.size, 0f);

        foreach (var col in cols)
        {
            if (!col.TryGetComponent<FruitMerge>(out _))
                continue;

            var rb = col.attachedRigidbody;
            if (rb == null || rb.bodyType != RigidbodyType2D.Dynamic)
                continue;

            int id = col.gameObject.GetInstanceID();
            currentFrameIDs.Add(id);

            fruitTimers.TryGetValue(id, out float elapsed);
            elapsed += Time.deltaTime;
            fruitTimers[id] = elapsed;

            if (elapsed >= gameOverDelay)
            {
                triggered = true;
                GameManager.Instance.TriggerGameOver();
                return;
            }
        }

        // 데스존을 떠난 과일 정리
        staleIDs.Clear();
        foreach (var kvp in fruitTimers)
            if (!currentFrameIDs.Contains(kvp.Key))
                staleIDs.Add(kvp.Key);
        foreach (var id in staleIDs)
            fruitTimers.Remove(id);
    }
}
