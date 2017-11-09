using UnityEngine;


// Used to detect opposing doorways to setup passageways
public class DoorwayDetector : MonoBehaviour
{

    public Doorway FindDoorway()
    {
        var overlappingColliders = Physics.OverlapBox(transform.position, Vector3.one);
        foreach(var collider in overlappingColliders)
        {
            var doorway = collider.GetComponent<Doorway>();
            if (doorway != null)
                return doorway;
        }
        return null;
    }
}
