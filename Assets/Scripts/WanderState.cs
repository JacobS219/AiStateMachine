using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class WanderState : BaseState
{
    private Vector3? _destination;
    private float _stopDistance = 1f;
    private float _turnSpeed = 1f;
    private readonly LayerMask _layerMask = LayerMask.NameToLayer("Walls");
    private float _rayDistance = 3.5f;
    private Quaternion _desiredRotation;
    private Vector3 _direction;
    private Drone _drone;

    public WanderState(Drone drone) : base(drone.gameObject)
    {
        _drone = drone;
    }

    public override Type Tick()
    {
        var chaseTarget = CheckForAggro();
        if (chaseTarget != null)
        {
            _drone.SetTarget(chaseTarget);
            return typeof(ChaseState);
        }

        if (_destination.HasValue == false || Vector3.Distance(_transform.position, _destination.Value) <= _stopDistance)
        {
            FindRandomDestination();
        }

        _transform.rotation = Quaternion.Slerp(_transform.rotation, _desiredRotation, Time.deltaTime * _turnSpeed);

        if (IsForwardBlocked())
        {
            _transform.rotation = Quaternion.Lerp(_transform.rotation, _desiredRotation, 0.2f);
        }
        else
        {
            _transform.Translate(Vector3.forward * Time.deltaTime * GameSettings.DroneSpeed);
        }

        Debug.DrawRay(_transform.position, _direction * _rayDistance, Color.red);
        while (IsPathBlocked())
        {
            FindRandomDestination();
            Debug.Log("WALL!");
        }

        return null;
    }

    private bool IsForwardBlocked()
    {
        Ray ray = new Ray(_transform.position, _transform.forward);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
    }

    private bool IsPathBlocked()
    {
        Ray ray = new Ray(_transform.position, _direction);
        return Physics.SphereCast(ray, 0.5f, _rayDistance, _layerMask);
    }

    private void FindRandomDestination()
    {
        Vector3 testPosition = _transform.position + (_transform.forward * 4f) + new Vector3(Random.Range(-4.5f, 4.5f), 0f, Random.Range(-4.5f, 4.5f));

        _destination = new Vector3(testPosition.x, 1f, testPosition.z);

        _direction = Vector3.Normalize(_destination.Value - _transform.position);
        _direction = new Vector3(_direction.x, 0f, _direction.z);
        _desiredRotation = Quaternion.LookRotation(_direction);
    }

    Quaternion startingAngle = Quaternion.AngleAxis(-60, Vector3.up);
    Quaternion stepAngle = Quaternion.AngleAxis(5, Vector3.up);

    private Transform CheckForAggro()
    {
        RaycastHit hit;
        var angle = _transform.rotation * startingAngle;
        var direction = angle * Vector3.forward;
        var pos = _transform.position;
        for (int i = 0; i < 24; i++)
        {
            if (Physics.Raycast(pos, direction, out hit, GameSettings.AggroRadius))
            {
                var drone = hit.collider.GetComponent<Drone>();
                if (drone != null && drone.Team != _gameObject.GetComponent<Drone>().Team)
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.red);
                    return drone.transform;
                }
                else
                {
                    Debug.DrawRay(pos, direction * hit.distance, Color.yellow);
                }
            }
            else
            {
                Debug.DrawRay(pos, direction * GameSettings.AggroRadius, Color.white);
            }
            direction = stepAngle * direction;
        }

        return null;
    }
}
