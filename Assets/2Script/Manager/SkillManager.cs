using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    [SerializeField] private Skill baseAttack = null;
    public Skill BaseAttack { get { return baseAttack; } }
    [SerializeField] private Skill aSkill = null;
    public Skill ASkill { get { return aSkill; } }
    [SerializeField] private Skill sSkill = null;
    public Skill SSkill { get { return sSkill; } }
    [SerializeField] private Skill dSkill = null;
    public Skill DSkill { get { return dSkill; } }
    [SerializeField] private Skill fSkill = null;
    public Skill FSkill { get { return fSkill; } }
}
