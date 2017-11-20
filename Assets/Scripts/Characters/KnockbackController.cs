using UnityEngine;

public class KnockbackController
{
    protected float _knockbackTime = 0.5f;
    protected float _timeKnockedBack = -1000f;
    protected Vector3 _knockbackDampeningVelocity = Vector3.zero;

    public bool BeingKnockedback
    {
        get { return Time.time - _timeKnockedBack < _knockbackTime; }
    }

    public void Knockback(float time)
    {
        _knockbackTime = time;
        _timeKnockedBack = Time.time;
    }
    
    public Vector3 GetVelocity(Vector3 currentVolocity)
    {
        return Vector3.SmoothDamp(currentVolocity, Vector3.zero, ref _knockbackDampeningVelocity, _knockbackTime);
    }
}
