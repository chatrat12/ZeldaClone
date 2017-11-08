using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    protected GameObject _owner;

    protected virtual void Awake()
    {
        var character = GetComponentInParent<Character>();
        if (character != null)
            _owner = character.gameObject;

    }

    protected virtual void Update() { }
    public virtual void BeginAttack() { }
    public virtual void EndAttack() { }
    public virtual void AttackNow() { }

    
}
