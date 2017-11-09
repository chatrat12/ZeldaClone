using UnityEngine;

public class RupeeChest : Chest
{
    [SerializeField]
    private RupeeValue _rupeeValue = RupeeValue.One;

    [SerializeField]
    private Material _greenRupeeMaterail;
    [SerializeField]
    private Material _blueRupeeMaterail;
    [SerializeField]
    private Material _redRupeeMaterail;

    protected override void Awake()
    {
        base.Awake();
        var renderer = transform.GetChild(0).GetComponentInChildren<Renderer>();
        switch(_rupeeValue)
        {
            case RupeeValue.One:
                renderer.material = _greenRupeeMaterail;
                break;
            case RupeeValue.Five:
                renderer.material = _blueRupeeMaterail;
                break;
            case RupeeValue.Twenty:
                renderer.material = _redRupeeMaterail;
                break;
        }
    }


    public enum RupeeValue : int
    {
        One = 1, Five = 5, Twenty = 20
    }
}
