using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private Transform spriteObject = null;

    private float gravity = 9.8f;
    public float Gravity { set { gravity = value < 1f ? 1f : value; } get { return gravity; } }
    private float jumpPower = 8f;
    public float JumpPower { set { jumpPower = value < 1f ? 1f : value; } get { return jumpPower; } }

    private bool isJump = false;
    
    private float jumpTime = 0f;
    private float criteria = 0f;
    private float originY = 0f;

    private void Awake()
    {
        originY = spriteObject.position.y;
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isJump)
        {
            StartCoroutine(Jump());
        }
    }

    private float CalculateMaxHeight(float p_gravity, float p_power)
    {
        return 0.5f * (p_power * p_power) / p_gravity;
    }

    private float CalculateHeight(float p_jumpTime, float p_gravity, float p_power)
    {
        return (p_jumpTime * p_jumpTime * (-p_gravity) / 2) + (p_jumpTime * p_power);
    }

    private IEnumerator Jump()
    {
        jumpTime = 0f;
        isJump = true;
        anim.SetBool("isJump", isJump);

        criteria = 0.8f * CalculateMaxHeight(Gravity, JumpPower) + originY;

        yield return JumpUp(criteria);

        anim.SetTrigger("Up");

        yield return JumpStay(criteria);

        anim.SetTrigger("Down");

        yield return JumpDown(originY);

        jumpTime = 0f;
        isJump = false;
        anim.SetBool("isJump", isJump);

        anim.ResetTrigger("Up");
        anim.ResetTrigger("Down");
    }

    private IEnumerator JumpUp(float p_criteria)
    {
        var t_vector = spriteObject.localPosition;

        while (spriteObject.localPosition.y <= p_criteria)
        {
            t_vector.y = originY + CalculateHeight(jumpTime, Gravity, JumpPower);

            spriteObject.localPosition = t_vector;

            jumpTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator JumpStay(float p_criteria)
    {
        var t_vector = spriteObject.localPosition;

        while (spriteObject.localPosition.y >= p_criteria)
        {
            t_vector.y = originY + CalculateHeight(jumpTime, Gravity, JumpPower);

            spriteObject.localPosition = t_vector;

            jumpTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator JumpDown(float p_criteria)
    {
        var t_vector = spriteObject.localPosition;

        while (spriteObject.localPosition.y >= p_criteria)
        {
            t_vector.y = originY + CalculateHeight(jumpTime, Gravity, JumpPower);

            spriteObject.localPosition = t_vector;

            jumpTime += Time.deltaTime;
            yield return null;
        }

        t_vector.y = originY;
        spriteObject.localPosition = t_vector;
    }
}
