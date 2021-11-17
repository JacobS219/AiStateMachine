using System;
using UnityEngine;

public class ChaseState : BaseState
{
    private Drone _drone;

    public ChaseState(Drone drone) : base(drone.gameObject)
    {
        _drone = drone;
    }

    public override Type Tick()
    {
        if (_drone.Target == null)
        {
            return typeof(WanderState);
        }

        _transform.LookAt(_drone.Target);
        _transform.Translate(Vector3.forward * Time.deltaTime * GameSettings.DroneSpeed);

        var distance = Vector3.Distance(_transform.position, _drone.Target.transform.position);
        if (distance <= GameSettings.AttackRange)
        {
            return typeof(AttackState);
        }

        return null;
    }
}
