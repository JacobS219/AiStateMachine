using System;
using UnityEngine;

public abstract class BaseState
{
    protected GameObject _gameObject;
    protected Transform _transform;
    public abstract Type Tick();

    public BaseState(GameObject gameObject)
    {
        _gameObject = gameObject;
        _transform = gameObject.transform;
    }
}
