using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameUltis;

public class CharacterAttack : MonoBehaviour
{
    [SerializeField] private GameObject[] skills;
    [SerializeField] private GameObject[] normalAttackObj;

    public AttackType attackType;

    public void UseSkill(AttackType type, Vector3 position, Character character) {
        if (character == Character.Player) {
            if (type == AttackType.Skill3) {
                ObjectPooling.Instance.SpawnObject(skills[(int)type], position, true);
            } else {
                ObjectPooling.Instance.SpawnObject(skills[(int)type], position);
            }
        }
        if (character == Character.Enemy)
            ObjectPooling.Instance.SpawnObject(skills[(int)type], position);
    }

    public void UseNormalAttack(Vector3 position) {
        
        for (int i = 0; i < normalAttackObj.Length; i++) {
            ObjectPooling.Instance.SpawnObject(normalAttackObj[i], position);
        }
    }

    #region Player's Skill Manager

    public Vector3 SkillPosition(AttackType type, Vector3 position) {
        float skillPosiontionY = 0f;
        float skillPositionX = 0f;
        switch (type) {
            case AttackType.NormalAttack:
                skillPosiontionY = 0.5f;
                skillPositionX = position.x + 1f;
                break;
            case AttackType.Skill1:
                skillPosiontionY = 0.2f;
                skillPositionX = position.x + 0.9f;
                break;
            case AttackType.Skill2:
                skillPosiontionY = 0.5f;
                skillPositionX = position.x + 1.5f;
                break;
            case AttackType.Skill3:
                skillPosiontionY = 2.5f;
                skillPositionX = position.x + 0.5f;
                break;
            case AttackType.Skill4:
                skillPosiontionY = 0.5f;
                skillPositionX = -12f;
                break;
        }
        return new Vector3(skillPositionX, position.y + skillPosiontionY, position.z);

    }

    public float TimeToCastSkill(AttackType type) {
        return GameConstants.TimeToCastSkill[(int)type];

    }
    #endregion
}
