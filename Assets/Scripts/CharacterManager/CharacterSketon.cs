using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameEnum;

public class CharacterSketon : MonoBehaviour {
    #region Inspector
    // [SpineAnimation] attribute allows an Inspector dropdown of Spine animation names coming form SkeletonAnimation.
    [SpineAnimation]
    public string idleAnimation;

    [SpineAnimation]
    public string skill0Animation;

    [SpineAnimation]
    public string skill1Animation;

    [SpineAnimation]
    public string skill2Animation;

    [SpineAnimation]
    public string skill3Animation;

    [SpineAnimation]
    public string skill4Animation;

    [SpineAnimation]
    public string transformAnimation;

    [SpineAnimation]
    public string tuskill0Animation;

    [SpineAnimation]
    public string tuskill4Animation;


    [SpineAnimation]
    public string[] enemyAnimation;

    #endregion

    SkeletonAnimation skeletonAnimation;

    public Spine.AnimationState spineAnimationState;
    public Spine.Skeleton skeleton;

    [SerializeField] PlayerParticle playerParticle; 

    //public SkeletonDataAsset skeletonDataAsset;
    private void Awake() {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
    }
    public void UpdateSkin(string name) {
        skeletonAnimation.initialSkinName = name;

        Skeleton skeleton = skeletonAnimation.Skeleton;

        // Lấy ra data của skin mới từ SkeletonData
        Skin newSkin = skeleton.Data.FindSkin(name);
        skeleton.SetSkin(newSkin);
        skeleton.SetSlotsToSetupPose();
    }




    void Start() {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        spineAnimationState = skeletonAnimation.AnimationState;
        skeleton = skeletonAnimation.Skeleton;
    }
    public void SetIdleAnim() {
        spineAnimationState.SetAnimation(0, idleAnimation, false);
        characterState = CharacterState.Idle;
        playerParticle.StopHandParticle();
    }
    #region Player AttackAnim

    public void SetAnimation(AttackType type) {
        float timeDelay = GameConstants.TimeToCastSkill[(int)type] - 0.5f;
        switch (type) {
            case AttackType.NormalAttack:
                
                SetAttackSkill2Anim(GameConstants.TimeToCastSkill[(int)type]);
                break;

            case AttackType.Skill1:
                SetAttackSkill1Anim(timeDelay);
                break;

            case AttackType.Skill2:
                SetAttackSkill2Anim(GameConstants.TimeToCastSkill[(int)type]);
                break;

            case AttackType.Skill3:
                SetAttackSkill3Anim(3f);
                break;

            case AttackType.Skill4:
                SetAttackSkill4Anim(GameConstants.TimeToCastSkill[(int)type]);
                break;

        }
    }



    
    //IEnumerator DelayBackToIdle() {
    //    yield return new WaitForSeconds(1f);
    //}


    // transform
    public void SetTransformAnim(float time) {
        StartCoroutine(DelayCastTransform(time));
    }
    IEnumerator DelayCastTransform(float time) {
        spineAnimationState.SetAnimation(0, transformAnimation, false);
        yield return new WaitForSeconds(time);
        SetIdleAnim();
    }
    
    //1
    public void SetAttackSkill1Anim(float time) {
        StartCoroutine(DelayCastSkill1(time));
    }

    IEnumerator DelayCastSkill1(float time) {
        spineAnimationState.SetAnimation(0, tuskill0Animation, false);
        yield return new WaitForSeconds(time);
        spineAnimationState.SetAnimation(0, skill1Animation, false);
        yield return new WaitForSeconds(1f);

        SetIdleAnim();
    }

    //2
    public void SetAttackSkill2Anim(float time) {
        StartCoroutine(DelayCastSkill2(time));
    }

    IEnumerator DelayCastSkill2(float time) {
        spineAnimationState.TimeScale = 3f;
        spineAnimationState.SetAnimation(0, skill1Animation, false);
        spineAnimationState.TimeScale = 1f;
        yield return new WaitForSeconds(time);

        SetIdleAnim();
    }
    //3
    public void SetAttackSkill3Anim(float time) {
        StartCoroutine(DelayCastSkill3(time));
    }

    IEnumerator DelayCastSkill3(float time) {
        spineAnimationState.SetAnimation(0, tuskill4Animation, false);
        yield return new WaitForSeconds(time);
        spineAnimationState.SetAnimation(0, skill4Animation, false);
        yield return new WaitForSeconds(1f);

        SetIdleAnim();
    }
    //4
    public void SetAttackSkill4Anim(float time) {
        spineAnimationState.TimeScale = 3f;
        StartCoroutine(DelayCastSkill4(time));

    }

    IEnumerator DelayCastSkill4(float time) {
        spineAnimationState.SetAnimation(0, skill3Animation, false);
        yield return new WaitForSeconds(time);
        spineAnimationState.TimeScale = 1f;
        SetIdleAnim();
    }
    #endregion

    #region EnemyAttackAnim

    public void SetEnemyAtkAnim(float time, AttackType type) {
        StartCoroutine(DelayEnemyCastSkill(time, type));
    }

    IEnumerator DelayEnemyCastSkill(float time, AttackType type) {
        spineAnimationState.SetAnimation(0, enemyAnimation[(int)type], false);
        yield return new WaitForSeconds(time);
        SetEnemyIdleAnim();
    }

    public void SetEnemyIdleAnim() {
        spineAnimationState.SetAnimation(0, idleAnimation, false);
    }
    #endregion

    #region Change Enemy Skin
    public void ChangeEnemySkeletonData(SkeletonDataAsset newSkeletonData) {
        SkeletonAnimation skeletonAnimation = GetComponent<SkeletonAnimation>();
        if (skeletonAnimation != null && spineAnimationState != null) {
            string currentAnimationName = spineAnimationState.GetCurrent(0).Animation.Name;
            skeletonAnimation.skeletonDataAsset = newSkeletonData;
            skeletonAnimation.Initialize(true);
            spineAnimationState = skeletonAnimation.AnimationState;

            spineAnimationState.SetAnimation(0, currentAnimationName, false);
        } 
    }


    #endregion
}
