using UnityEngine;
using System.Collections;
using Prime31.StateKit;

[RequireComponent(typeof(Rigidbody))]

public class CubeController : MonoBehaviour
{

    public float moveSpeed = 5f;

    private SKStateMachine<CubeController> cubeStateMachine;

    internal Rigidbody thisRigidbody;

    void Awake()
    {
        thisRigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        cubeStateMachine = new SKStateMachine<CubeController>(this, new CubeStateIdle());
        cubeStateMachine.addState(new CubeStateGoingLeft());
        cubeStateMachine.addState(new CubeStateGoingRight());
    }
    void Update()
    {
        cubeStateMachine.update(Time.deltaTime);
    }
    void FixedUpdate()
    {
        cubeStateMachine.fixedUpdate(Time.fixedDeltaTime);
    }
}

public class CubeStateIdle : SKState<CubeController> {

    public override void begin()
    {
        Debug.Log("BEGIN\t IDLE");
    }
    public override void update(float deltaTime)
    {
        var input = Input.GetAxisRaw("Horizontal");
        if (input == -1) _machine.changeState<CubeStateGoingLeft>();
        if (input ==  1) _machine.changeState<CubeStateGoingRight>();
        if (input ==  0) _machine.changeState<CubeStateIdle>();
    }
}

public class CubeStateGoingLeft : CubeStateIdle
{
    public override void begin()
    {
        Debug.Log("BEGIN\t LEFT");
    }
    public override void fixedReason() {
        Debug.Log("fixedReason\t LEFT");
    }
    public override void fixedUpdate(float fixedDeltaTime)
    {
        _context.thisRigidbody.MovePosition(_context.thisRigidbody.position +  Vector3.left * _context.moveSpeed * fixedDeltaTime);
    }
    public override void end()
    {
        Debug.Log("END\t LEFT " + _machine.elapsedTimeInStateFromFixedUpdate);
    }
}

public class CubeStateGoingRight : CubeStateIdle
{
    public override void begin()
    {
        Debug.Log("BEGIN\t RIGHT");
    }
    public override void fixedReason() {
        Debug.Log("fixedReason\t RIGHT");
    }
    public override void fixedUpdate(float fixedDeltaTime) {
        _context.thisRigidbody.MovePosition(_context.thisRigidbody.position + Vector3.right * _context.moveSpeed * fixedDeltaTime);
    }
    public override void end()
    {
        Debug.Log("END\t RIGHT " + _machine.elapsedTimeInStateFromFixedUpdate);
    }
}