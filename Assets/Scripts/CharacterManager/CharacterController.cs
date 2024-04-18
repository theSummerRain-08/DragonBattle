using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    [SerializeField] protected CharacterSketon characterSketon = null;
    [SerializeField] protected CharacterAttack characterAttack = null;
    [SerializeField] protected PlayerParticle characterParticle = null;
    protected abstract void Move();
    public abstract void Attack(AttackType type);
    protected abstract void Die();

}
