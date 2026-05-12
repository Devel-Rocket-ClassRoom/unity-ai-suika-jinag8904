using System.Linq;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        foreach (var i in Enumerable.Range(0, 10)) { Debug.Log(i); }
    }
}