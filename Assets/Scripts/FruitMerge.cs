using UnityEngine;

public class FruitMerge : MonoBehaviour
{
    public static event System.Action<FruitData> OnFruitMerged;

    [SerializeField] public FruitData fruitData;

    public bool isMerging;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (isMerging)
            return;

        var otherMerge = other.gameObject.GetComponent<FruitMerge>();
        if (otherMerge == null || otherMerge.isMerging)
            return;

        if (otherMerge.fruitData.tier != fruitData.tier)
            return;

        // 두 오브젝트 중 하나만 실행
        if (GetInstanceID() > otherMerge.GetInstanceID())
            return;

        isMerging = true;
        otherMerge.isMerging = true;

        Vector3 midPos = (transform.position + other.transform.position) * 0.5f;

        if (fruitData.nextTier != null)
            Instantiate(fruitData.nextTier.prefab, midPos, Quaternion.identity);

        OnFruitMerged?.Invoke(fruitData);

        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
