using System;
using UnityEngine;

public class AttackState : BaseState
{
    private Launcher _launcher;
    private Drone _drone;

    private float _attackReadyTimer;
    private float _targetReCheckRate = .5f;
    private float _nextCheckTime;

    public AttackState(Drone drone, Launcher launcher) : base(drone.gameObject)
    {
        _drone = drone;
        _launcher = launcher;
    }

    public override Type Tick()
    {
        if (_drone.Target == null)
        {
            return typeof(WanderState);
        }

        _attackReadyTimer -= Time.deltaTime;

        if (_attackReadyTimer <= 0f)
        {
            _launcher.FireWeapon();
        }

        if (_drone.Target.hasChanged)
        {
            //_nextCheckTime = Time.time + _targetReCheckRate;
            return typeof(ChaseState);
        }

        //if (TimeToCheckPoisition())
        //{
        //    if (_drone.Target.hasChanged)
        //    {
        //        _nextCheckTime = Time.time + _targetReCheckRate;
        //        return typeof(ChaseState);
        //    }
        //    _nextCheckTime = Time.time + _targetReCheckRate;
        //}

        return null;
    }

    private bool TimeToCheckPoisition()
    {
        return Time.time > _nextCheckTime;
    }
}