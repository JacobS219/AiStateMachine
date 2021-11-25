using System;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    
    [SerializeField] private Team _team;
    [SerializeField] private GameObject _laserVisual;

    public Transform Target { get; private set; }
    public Team Team => _team;
    public StateMachine StateMachine => GetComponent<StateMachine>();

    public Launcher launcher;

    void Awake()
    {
        InitializeStateMachine();
    }

    private void InitializeStateMachine()
    {
        var states = new Dictionary<Type, BaseState>()
        {
            { typeof(WanderState), new WanderState(this) },
            { typeof(ChaseState), new ChaseState(this) },
            { typeof(AttackState), new AttackState(this, launcher) }
        };

        GetComponent<StateMachine>().SetStates(states);
    }

    public void SetTarget(Transform target)
    {
        Target = target;
    }
}

public enum Team
{
    Red,
    Blue,
    Green,
    Yellow
}
