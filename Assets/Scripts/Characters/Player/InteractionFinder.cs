using System.Collections.Generic;
using UnityEngine;

public class InteractionFinder : MonoBehaviour
{
    private List<InteractableObject> _overlappingInteracables = new List<InteractableObject>();

    public InteractableObject GetViableInteraction()
    {
        InteractableObject currentObject = null;
        float currentDistance = 0f;

        for (int i = 0; i < _overlappingInteracables.Count; i++)
        {
            if (_overlappingInteracables[i] == null) continue;
            if (LookingAtObject(_overlappingInteracables[i].gameObject))
            {
                if (currentObject == null)
                {
                    currentObject = _overlappingInteracables[i];
                    currentDistance = Vector3.Distance(transform.position, _overlappingInteracables[i].transform.position);
                }
                else
                {
                    var dist = Vector3.Distance(transform.position, _overlappingInteracables[i].transform.position);
                    if (dist < currentDistance)
                    {
                        currentObject = _overlappingInteracables[i];
                        currentDistance = dist;
                    }
                }
            }
        }
        return currentObject;
    }

    private bool LookingAtObject(GameObject obj)
    {
        //return true;
        var refDirection = obj.transform.position - transform.position;
        refDirection.y = 0;
        refDirection.Normalize();
        var lookDirection = transform.forward;
        lookDirection.y = 0;
        lookDirection.Normalize();

        var dot = Vector3.Dot(refDirection, lookDirection);
        return dot > 0.5;
    }

    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<InteractableObject>();
        if (interactable != null)
        {
            if (!_overlappingInteracables.Contains(interactable))
                _overlappingInteracables.Add(interactable);
        }
    }
    private void OnTriggerExit(Collider other)
    {

        var interactable = other.GetComponent<InteractableObject>();
        if (interactable != null)
        {
            if (_overlappingInteracables.Contains(interactable))
                _overlappingInteracables.Remove(interactable);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label(string.Format("Interactables: {0}", _overlappingInteracables.Count));
    }
}
