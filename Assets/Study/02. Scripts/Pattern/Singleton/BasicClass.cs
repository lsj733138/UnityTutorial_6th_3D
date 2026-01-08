using UnityEngine;

public class BasicClass : SingletonCore<BasicClass>
{
    protected override void Awake()
    {
        base.Awake();
    }
}
