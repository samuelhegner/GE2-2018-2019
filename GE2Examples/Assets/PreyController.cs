﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PatrolState : AiState
{
    public override void Enter()
    {
        owner.GetComponent<FollowPath>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<PreyController>().predator.transform.position,
            owner.transform.position) < 10)
        {
            owner.ChangeState(new DefendState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<FollowPath>().enabled = false;
    }
}

public class DefendState : AiState
{
    public override void Enter()
    {
        owner.GetComponent<Pursue>().target = owner.GetComponent<PreyController>().predator.GetComponent<Boid>();
        owner.GetComponent<Pursue>().enabled = true;
    }

    public override void Think()
    {
        if (Vector3.Distance(
            owner.GetComponent<PreyController>().predator.transform.position,
            owner.transform.position) > 30)
        {
            owner.ChangeState(new PatrolState());
        }
    }

    public override void Exit()
    {
        owner.GetComponent<Pursue>().enabled = false;
    }



}

public class PreyController : MonoBehaviour
{
    public GameObject predator;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<LabTest>().ChangeState(new PatrolState());   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
