using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBehaviour : MonoBehaviour
{
    public bool controledByPlayer;

    public Rigidbody characterRigidbody;

    public Animator animator;

    public GameObject head;
    public GameObject chest;

    public GameObject rightArm;
    public GameObject leftArm;
    public GameObject rightLeg;
    public GameObject leftLeg;

    public float hp;
    public float speed;
    public float jump;

    public bool checkFloor;
    public bool isOnFloor;

    void Start()
    {
        checkFloor = true;
        isOnFloor = true;
    }

    IEnumerator WaitAndEnableCheckFloor(float delay)
    {
        yield return new WaitForSeconds(delay);
        checkFloor = true;
    }

    void Update()
    {
        if (controledByPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnFloor)
            {
                // jump
                animator.SetTrigger("Jump");
                checkFloor = false;
                isOnFloor = false;
                animator.SetBool("IsOnGround", isOnFloor);
                StartCoroutine(WaitAndEnableCheckFloor(0.4f));
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                // attack
                animator.SetTrigger("RightPunch");
            }

            float direction = 0;
            if (Input.GetKey(KeyCode.Q))
            {
                // left
                direction = -1;
            }
            if (Input.GetKey(KeyCode.D))
            {
                // right
                direction = 1;
            }
            Vector3 velocityWithoutX = new Vector3(0, characterRigidbody.velocity.y, characterRigidbody.velocity.z);
            characterRigidbody.velocity = velocityWithoutX + speed * Vector3.right * direction;
            animator.SetBool("Walking", (direction != 0));
        }

        if (checkFloor)
        {
            Ray groundRay = new Ray(rightLeg.transform.position, -Vector3.up);
            RaycastHit hit;
            if (Physics.Raycast(groundRay, out hit, 1.2f) && (hit.collider.tag.Equals("Ground") || hit.collider.tag.Equals("Player")))
            {
                isOnFloor = true;
            }
            else
            {
                isOnFloor = false;
            }
            animator.SetBool("IsOnGround", isOnFloor);
        }
    }

    public void Jump()
    {
        Vector3 velocityWithoutY = new Vector3(characterRigidbody.velocity.x, 0, characterRigidbody.velocity.z);
        characterRigidbody.velocity = velocityWithoutY + Vector3.up * jump;
    }

    public void RightArmStartHit()
    {
        rightArm.tag = "HitBox";
        rightArm.GetComponent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.red, 0.5f);
    }
    public void RightArmStopHit()
    {
        rightArm.tag = "Player";
        rightArm.GetComponent<Renderer>().material.color = Color.white;
    }

    public void RightLegStartHit()
    {
        rightLeg.tag = "HitBox";
        rightLeg.GetComponent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.red, 0.5f);
    }
    public void RightLegStopHit()
    {
        rightLeg.tag = "Player";
        rightLeg.GetComponent<Renderer>().material.color = Color.white;
    }

    public void LeftLegStartHit()
    {
        leftLeg.tag = "HitBox";
        leftLeg.GetComponent<Renderer>().material.color = Color.Lerp(Color.yellow, Color.red, 0.5f);
    }
    public void LeftLegStopHit()
    {
        leftLeg.tag = "Player";
        leftLeg.GetComponent<Renderer>().material.color = Color.white;
    }

    public void GetsHit(float damage, Vector3 otherPosition)
    {
        hp -= damage;
        characterRigidbody.AddExplosionForce(100, otherPosition, 200);
    }
}
