using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [SerializeField] private Animator anim = null;
    [SerializeField] private Transform yPosObject = null;

    private float gravity = 9.8f;
    public float Gravity { set { gravity = value < 1f ? 1f : value; } get { return gravity; } }
    private float jumpPower = 8f;
    public float JumpPower { set { jumpPower = value < 1f ? 1f : value; } get { return jumpPower; } }

    private float jumpTime = 0f;
    private float criteria = 0f;
    private float originY = 0f;

    private void Awake()
    {
        originY = yPosObject.localPosition.y;
    }

    private float CalculateMaxHeight(float p_gravity, float p_power)
    {
        return 0.5f * (p_power * p_power) / p_gravity;
    }

    private float CalculateHeight(float p_jumpTime, float p_gravity, float p_power)
    {
        return (p_jumpTime * p_jumpTime * (-p_gravity) / 2) + (p_jumpTime * p_power);
    }

    public IEnumerator Jump() => JumpCo();

    private IEnumerator JumpCo()
    {
        jumpTime = 0f;
        anim.SetBool("isJump", true);

        criteria = 0.8f * CalculateMaxHeight(Gravity, JumpPower) + originY;

        yield return JumpUp(criteria);

        anim.SetTrigger("Up");

        yield return JumpStay(criteria);

        anim.SetTrigger("Down");

        yield return JumpDown(originY);

        jumpTime = 0f;
        anim.SetBool("isJump", false);
    }

    private IEnumerator JumpUp(float p_criteria)
    {
        var t_vector = yPosObject.localPosition;

        while (yPosObject.localPosition.y <= p_criteria)
        {
            t_vector.y = originY + CalculateHeight(jumpTime, Gravity, JumpPower);

            yPosObject.localPosition = t_vector;

            jumpTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator JumpStay(float p_criteria)
    {
        var t_vector = yPosObject.localPosition;

        while (yPosObject.localPosition.y >= p_criteria)
        {
            t_vector.y = originY + CalculateHeight(jumpTime, Gravity, JumpPower);

            yPosObject.localPosition = t_vector;

            jumpTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator JumpDown(float p_criteria)
    {
        var t_vector = yPosObject.localPosition;

        while (yPosObject.localPosition.y >= p_criteria)
        {
            t_vector.y = originY + CalculateHeight(jumpTime, Gravity, JumpPower);

            yPosObject.localPosition = t_vector;

            jumpTime += Time.deltaTime;
            yield return null;
        }

        t_vector.y = originY;
        yPosObject.localPosition = t_vector;
    }
}
