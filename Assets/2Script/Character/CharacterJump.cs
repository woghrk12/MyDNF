using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJump : MonoBehaviour
{
    [SerializeField] private Animator anim = null;

    private float gravity = 9.8f;
    public float Gravity { set { gravity = value < 1f ? 1f : value; } get { return gravity; } }
    private float jumpPower = 8f;
    public float JumpPower { set { jumpPower = value < 1f ? 1f : value; } get { return jumpPower; } }

    private float jumpTime = 0f;
    private float criteria = 0f;
    private float originY = 0f;

    public void InitializeValue(HitBox p_hitBox)
    {
        originY = p_hitBox.YPos;                            
    }

    private float CalculateMaxHeight(float p_gravity, float p_power)
    {
        return 0.5f * (p_power * p_power) / p_gravity;
    }

    private float CalculateHeight(float p_jumpTime, float p_gravity, float p_power)
    {
        return (p_jumpTime * p_jumpTime * (-p_gravity) / 2) + (p_jumpTime * p_power);
    }

    public IEnumerator Jump(CharacterTransform p_transform) => JumpCo(p_transform);

    private IEnumerator JumpCo(CharacterTransform p_transform)
    {
        jumpTime = 0f;
        anim.SetBool("isJump", true);

        criteria = 0.8f * CalculateMaxHeight(Gravity, JumpPower) + originY;

        yield return JumpUp(p_transform, criteria);

        anim.SetTrigger("Up");

        yield return JumpStay(p_transform, criteria);

        anim.SetTrigger("Down");

        yield return JumpDown(p_transform, originY);

        jumpTime = 0f;
        anim.SetBool("isJump", false);
    }

    private IEnumerator JumpUp(CharacterTransform p_transform, float p_criteria)
    {
        while (p_transform.YPos <= p_criteria)
        {
            p_transform.YPos = originY + CalculateHeight(jumpTime, Gravity, JumpPower);
            jumpTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator JumpStay(CharacterTransform p_transform, float p_criteria)
    {
        while (p_transform.YPos >= p_criteria)
        {
            p_transform.YPos = originY + CalculateHeight(jumpTime, Gravity, JumpPower);
            jumpTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator JumpDown(CharacterTransform p_transform, float p_criteria)
    {
        while (p_transform.YPos >= p_criteria)
        {
            p_transform.YPos = originY + CalculateHeight(jumpTime, Gravity, JumpPower);
            jumpTime += Time.deltaTime;
            yield return null;
        }
        p_transform.YPos = originY;
    }
}
