using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "SuikaGame/FruitData")]
public class FruitData : ScriptableObject
{
    public int tier;
    public string fruitName;
    public int mergeScore;
    public float radiusMultiplier;
    public FruitData nextTier;
    public GameObject prefab;
}
