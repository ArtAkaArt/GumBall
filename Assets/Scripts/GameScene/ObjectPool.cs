using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private int length;

    protected List<GameObject> Pool;

    protected void Initialize(GameObject prefab)
    {
        Pool = Enumerable.Range(0, length)
            .Select(x =>
            {
                var pooledObject = Instantiate(prefab, transform);
                pooledObject.SetActive(false);
                return pooledObject;
            })
            .ToList();
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = Pool.FirstOrDefault(x => !x.activeSelf);
        return result != null;
    }

    public void ResetNodes() => Pool.ForEach(x => x.SetActive(false));
}