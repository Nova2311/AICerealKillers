using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Unit : MonoBehaviour, ISelectHandler, IPointerClickHandler, IDeselectHandler {

    public static HashSet<Unit> allMySelectabkes = new HashSet<Unit>();
    public static HashSet<Unit> currentlySelected = new HashSet<Unit>();

    public GameObject selected;

    public static bool hasLeader = false;
    public bool isLeader = false;

    public enum UnitState
    {
        Idle,
        Engage,
        Attack,
        Capture,
        Move
    }

    private UnitState state_;
    private float speed_;
    private float los_length_;
    private float attack_range_;
    private GameObject target_;


    private void Awake() {
        allMySelectabkes.Add(this);
        selected.SetActive(false);
    }

    void Start()
    {
        state_ = UnitState.Idle;
        speed_ = 30.0f;
        los_length_ = 30.0f;
        target_ = null;
    }

    private void Update()
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
            case UnitState.Capture:
                Capture();
                break;
            case UnitState.Move:
                Move();
                break;
        }
    }

    public void OnPointerClick(PointerEventData eventData) {
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.RightControl)) {
            DeselectAll(eventData);
        }
        OnSelect(eventData);      
    }

    public void OnSelect(BaseEventData eventData) {
        currentlySelected.Add(this);
        selected.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData) {
        selected.SetActive(false);
    }

    public static void DeselectAll(BaseEventData eventData) {
        foreach (Unit selectable in currentlySelected) {
            selectable.OnDeselect(eventData);
            selectable.isLeader = false;
        }
        hasLeader = false;

        Debug.Log("Leaders removed");
        currentlySelected.Clear();
    }

    public static void GenerateLeader() {
        foreach (Unit unit in currentlySelected) {
            if (!hasLeader) {
                hasLeader = true;
                unit.isLeader = true;
                Debug.Log(unit.gameObject.name);
                return;
            }
        }
    }

    private void Idle()
    {
        gameObject.transform.Rotate(new Vector3(0.0f, speed_ * Time.deltaTime, 0.0f));
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, los_length_))
        {
            Debug.Log("Spotted object");
            state_ = UnitState.Engage;
            target_ = hit.transform.gameObject;
        }
    }

    private void Engage()
    {
        transform.Translate(Vector3.Normalize(target_.transform.position - transform.position) * 0.5f);
    }

    private void Attack()
    {
        gameObject.transform.Rotate(new Vector3(0.0f, speed_ * 100.0f * Time.deltaTime, 0.0f));
    }

    private void Capture()
    {

    }

    private void Move()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target_)
        {
            Debug.Log("Target reached");
            state_ = UnitState.Attack;
        }
    }
}
