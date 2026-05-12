using System.Collections.Generic;
using UnityEngine;

public class DeathLineChecker : MonoBehaviour
{
    [SerializeField] float gameOverDelay = 3.0f;

    // instanceID → 데스존 체류 누적 시간
    readonly Dictionary<int, float> fruitTimers = new();
    bool triggered;

    void OnTriggerStay2D(Collider2D other)
    {
        if (triggered)
            return;

        if (!other.TryGetComponent<FruitMerge>(out _))
            return;

        // 낙하 중(Kinematic) 과일은 카운트 제외
        var rb = other.GetComponent<Rigidbody2D>();
        if (rb == null || rb.bodyType != RigidbodyType2D.Dynamic)
            return;

        int id = other.gameObject.GetInstanceID();
        fruitTimers.TryGetValue(id, out float elapsed);
        elapsed += Time.deltaTime;
        fruitTimers[id] = elapsed;

        if (elapsed >= gameOverDelay)
        {
            triggered = true;
            GameManager.Instance.TriggerGameOver();
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        fruitTimers.Remove(other.gameObject.GetInstanceID());
    }
}
