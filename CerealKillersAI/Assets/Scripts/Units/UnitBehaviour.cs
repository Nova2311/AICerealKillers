using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class UnitBehaviour : MonoBehaviour
{

    public enum UnitState
    {
        Idle,
        Engage,
        Attack,
        AttackMove,
        Move
    }

    public enum Objective
    {
        Capture,
        Guard
    }

    //public GameObject red_gold, blue_gold, red_base, blue_base;
    public Vector3 target_pos;
    public UnitState start_state;
    private UnitState state_;
    private Objective objective_;
    private CapturePoint red_gold_, blue_gold_;
    private float speed_;
    private float los_length_;
    private float attack_range_;
    private Vector3 target_position_;
    private GameObject enemy_target_, capture_target_;
    private bool guarding_;

    // Use this for initialization
    void Start()
    {
        state_ = start_state;
        target_position_ = target_pos;
        speed_ = 10.0f;
        los_length_ = 30.0f;
        enemy_target_ = null;
        guarding_ = false;
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
                Move(target_position_);
                break;
            case UnitState.Move:
                Move(target_position_);
                break;
        }
    }

    public UnitState GetState()
    {
        return state_;
    }

    public void SetCaptureTarget(GameObject target)
    {
        capture_target_ = target;
        target_position_ = target.transform.position;
    }

    private void Idle()
    {
        gameObject.transform.Rotate(new Vector3(0.0f, speed_ * Time.deltaTime, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, los_length_))
        {
            Debug.Log("Spotted object");
            state_ = UnitState.Engage;
            enemy_target_ = hit.transform.Find("UnitHitbox").gameObject;

        }
    }

    private void Engage()
    {
        float step = speed_ * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, enemy_target_.transform.position, step);
    }

    private void Attack()
    {
        //placeholder for attack animation
        gameObject.transform.Rotate(new Vector3(0.0f, speed_ * 100.0f * Time.deltaTime, 0.0f));
    }

    private void Move(Vector3 target_position)
    {
        float step = speed_ * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target_position, step);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (state_)
        {
            case UnitState.AttackMove:
                if ((other.tag == "Collider" && other.name == "UnitHitbox") && gameObject.name == "VisualRange")
                {
                    Debug.Log("Target found");
                    state_ = UnitState.Engage;
                    enemy_target_ = other.gameObject;
                }
                break;
            case UnitState.Engage:
                //if (gameObject.name == "UnitHitbox" && other.name == "UnitHitbox")
                //{
                //    Debug.Log("Target reached");
                //    state_ = UnitState.Attack;
                //}
                Debug.Log(transform. + ", " + other.name);
                EditorApplication.isPaused = true;
                break;
        }
    }
}
