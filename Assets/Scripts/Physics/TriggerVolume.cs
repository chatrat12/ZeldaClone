using System.Collections.Generic;
using UnityEngine;

public class TriggerVolume : MonoBehaviour
{
    public delegate void ColliderEvent(Collider collider);
    public event ColliderEvent TriggerEntered;
    public event ColliderEvent TriggerExited;

    public List<Collider> CollidersInVolume { get; private set; }

    public List<T> GetComponentsInVolume<T>(ref List<T> result) where T : Component
    {
        foreach (var collider in CollidersInVolume)
        {
            if (collider != null && collider.gameObject != null)
            {
                var component = collider.GetComponent<T>();
                if (component != null && !result.Contains(component))
                    result.Add(component);
            }
        }
        return result;
    }

    private void Awake()
    {
        CollidersInVolume = new List<Collider>();
    }

    private void LateUpdate()
    {
        PurgeNullObjects();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (!CollidersInVolume.Contains(collider))
            CollidersInVolume.Add(collider);
        if (TriggerEntered != null)
            TriggerEntered(collider);
    }
    private void OnTriggerExit(Collider collider)
    {
        if (CollidersInVolume.Contains(collider))
            CollidersInVolume.Remove(collider);
        if (TriggerExited != null)
            TriggerExited(collider);
    }

    private void PurgeNullObjects()
    {
        for (int i = 0; i < CollidersInVolume.Count; i++)
        {
            if(CollidersInVolume[i] == null)
            {
                CollidersInVolume.RemoveAt(i);
                i--;
            }
        }
    }

}
