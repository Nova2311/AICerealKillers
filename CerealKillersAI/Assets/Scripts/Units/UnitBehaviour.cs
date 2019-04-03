using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour
{

    public enum UnitState
    {
        Idle,
        Engage,
        Attack,
        AttackMove,
        Capture,
        Move
    }

    private UnitState state_;
    private float speed_;
    private float los_length_;
    private float attack_range_;
    private Vector3 target_position_;
    private GameObject enemy_target_;

    // Use this for initialization
    void Start()
    {
        state_ = UnitState.AttackMove;
        speed_ = 10.0f;
        los_length_ = 30.0f;
        enemy_target_ = null;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state_)
        {
            case UnitState.Idle:
                Idle();
                break;
            case UnitState.Engage:
                Engage();
                break;
            case UnitState.Attack:
                Attack();
                break;
            case UnitState.AttackMove:
                AttackMove(new Vector3(0.0f, 0.0f, 0.0f));
                break;
            case UnitState.Capture:
                Capture();
                break;
            case UnitState.Move:
                Move();
                break;
        }
    }

    public UnitState GetState()
    {
        return state_;
    }

    private void Idle()
    {
        gameObject.transform.Rotate(new Vector3(0.0f, speed_ * Time.deltaTime, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, los_length_))
        {
            Debug.Log("Spotted object");
            state_ = UnitState.Engage;
            enemy_target_ = hit.transform.gameObject;
        }
    }

    private void Engage()
    {
        float step = speed_ * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, enemy_target_.transform.position, step);
    }

    private void Attack()
    {
        gameObject.transform.Rotate(new Vector3(0.0f, speed_ * 100.0f * Time.deltaTime, 0.0f));
    }

    private void AttackMove(Vector3 target_position)
    {
        float step = speed_ * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target_position, step);
    }

    private void Capture()
    {

    }

    private void Move()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        switch (state_)
        {
            case UnitState.Engage:
                if (other.gameObject == enemy_target_)
                {
                    Debug.Log("Target reached");
                    state_ = UnitState.Attack;
                }
                break;
            case UnitState.AttackMove:
                if (other.tag == "Blue")
                {
                    Debug.Log("Target found");
                    state_ = UnitState.Engage;
                    enemy_target_ = other.gameObject;
                }
                break;
        }
    }
}
