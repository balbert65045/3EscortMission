using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscortObj : MonoBehaviour {

    private Rigidbody m_Rigidbody;
    private Animator m_Animator;
    

    [SerializeField]
    float speed = 1f;
    [SerializeField]
    float m_AnimSpeedMultiplier = 1f;
    [SerializeField]
    float m_GroundCheckDistance = 0.1f;
    [SerializeField]
    float m_ForwardCheckDistance = 10f;
    [SerializeField]
    float m_MovingTurnSpeed = 360;
    [SerializeField]
    float m_StationaryTurnSpeed = 180;


    float m_TurnAmount;
    float m_ForwardAmount;
    public bool m_IsGrounded = true;
    Vector3 m_GroundNormal;


    public enum PlayerState {ForwardMove, RightMove, LeftMove};
    public PlayerState EscortState;
    // Use this for initialization
    void Start () {

        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        EscortState = PlayerState.ForwardMove;
        //_Animator 

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        CheckForMoveChange();
        Move();
        CheckGroundStatus();
    }

    private void Move()
    {
        Vector3 Move = Vector3.zero;
        if (EscortState == PlayerState.ForwardMove)
        {
            if (transform.rotation.eulerAngles.y > 10 && (transform.rotation.eulerAngles.y < 180))
            {
                Move = new Vector3(speed, 0, speed);
                m_TurnAmount = -1;
            }
            else if (transform.rotation.eulerAngles.y > 180)
            {
                m_TurnAmount = 1;
                Move = new Vector3(-speed, 0, speed);
            }
            else
            {
                Move = new Vector3(0, 0, speed);
                m_TurnAmount = 0;
            }

        }
        else if (EscortState == PlayerState.RightMove)
        {
            //facing forward therfore turn right
            if (transform.rotation.eulerAngles.y < 90 || (transform.rotation.eulerAngles.y >= 359))
            {
                Move = new Vector3(speed, 0, speed);
                m_TurnAmount = 1;
            }
            // continue running right
            else
            {
                Move = new Vector3(speed, 0, 0);
                m_TurnAmount = 0;
            }
        }
        else if (EscortState == PlayerState.LeftMove)
        {

            // facing forward therefore turn left
            if (transform.rotation.eulerAngles.y > 270 || (transform.rotation.eulerAngles.y <= 1))
            {
                Move = new Vector3(-speed, 0, speed);
                m_TurnAmount = -1;
            }
            // continue running left
            else
            {
                Move = new Vector3(-speed, 0, 0);
                m_TurnAmount = 0;
            }
          
        }

    //    m_TurnAmount = Mathf.Atan2(Move.x, Move.z);
        ApplyExtraTurnRotation();


        Vector3 MoveDirection = Move.normalized;
        m_Rigidbody.velocity = Move;
        Animate(MoveDirection);
    }

    void ApplyExtraTurnRotation()
    {
        if (m_TurnAmount == 0 && EscortState == PlayerState.ForwardMove)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (m_TurnAmount == 0 && EscortState == PlayerState.LeftMove)
        {
            transform.rotation = Quaternion.Euler(0, 270, 0);
        }
        else if (m_TurnAmount == 0 && EscortState == PlayerState.RightMove)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            // help the character turn faster (this is in addition to root rotation in the animation)
            float turnSpeed = Mathf.Lerp(m_StationaryTurnSpeed, m_MovingTurnSpeed, m_ForwardAmount);
            transform.Rotate(0, m_TurnAmount * turnSpeed * Time.deltaTime, 0);
        }
    }

    private void Animate(Vector3 MoveDirection)
    {
        m_ForwardAmount = MoveDirection.magnitude;
       // Debug.Log(MoveDirection.magnitude);
        // Always Grounded//
        //Possibly change//
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.1f, Time.deltaTime);
        m_Animator.SetBool("OnGround", true);
        //
        //
        m_Animator.SetFloat("Forward", m_ForwardAmount, 0.1f, Time.deltaTime);

        if (m_IsGrounded && MoveDirection.magnitude > 0)
        {
            m_Animator.speed = m_AnimSpeedMultiplier;
        }
        else
        {
            // don't use that while airborne
         //   m_Animator.speed = 1;
        }
    }

    void CheckGroundStatus()
    {
        RaycastHit hitInfo;
#if UNITY_EDITOR
        // helper to visualise the ground check ray in the scene view
        Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * m_GroundCheckDistance));
#endif
        // 0.1f is a small offset to start the ray from inside the character
        // it is also good to note that the transform position in the sample assets is at the base of the character
        if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, m_GroundCheckDistance))
        {
            m_GroundNormal = hitInfo.normal;
            m_IsGrounded = true;
            m_Animator.applyRootMotion = true;
        }
        else
        {
            m_IsGrounded = false;
            m_GroundNormal = Vector3.up;
            m_Animator.applyRootMotion = false;
        }
    }

    void CheckForMoveChange()
    {
        RaycastHit hitInfo;

        Debug.DrawLine(transform.position + (Vector3.up * 0.5f), transform.position + (Vector3.up * 0.5f) + (Vector3.forward * m_ForwardCheckDistance));

        if (Physics.Raycast(transform.position + (Vector3.up * 0.5f), Vector3.forward, out hitInfo, m_ForwardCheckDistance))
        {
           
            if (hitInfo.transform.GetComponentInParent<BoxArrow>() != null)
            {
                // Left Box
                if (hitInfo.transform.GetComponentInParent<BoxArrow>().BoxType == 1)
                {
                    EscortState = PlayerState.LeftMove;
                    return;
                }
                else if (hitInfo.transform.GetComponentInParent<BoxArrow>().BoxType == 2)
                {
                    EscortState = PlayerState.RightMove;
                    return;
                }
            }
        }
        EscortState = PlayerState.ForwardMove;
    }

}
