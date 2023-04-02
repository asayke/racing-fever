using System.Collections.Generic;
using UnityEngine;

public class MusicalController : MonoBehaviour
{
    private static readonly Dictionary<string, GameObject> _instances = new Dictionary<string, GameObject>();
    public string ID; 

    private void Awake()
    {
        if (_instances.ContainsKey(ID))
        {
            var existing = _instances[ID];

            if (existing != null)
            {
                if (ReferenceEquals(gameObject, existing))
                    return;

                Destroy(gameObject);
                return;
            }
        }

        _instances[ID] = gameObject;
        DontDestroyOnLoad(gameObject);
    }
}