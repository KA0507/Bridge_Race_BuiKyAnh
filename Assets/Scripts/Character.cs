using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private int characterColor;
    [SerializeField] private StackBrick stackBrick;
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private ColorData cd;
    [SerializeField] private Level level;


    private int intCurrentStage = -1;
    private Stage currentStage;
    private string currentAnimName;

    // Thay đổi màu
    public void ChangeColor(int color)
    {
        characterColor = color;
        skinnedMeshRenderer.material = cd.GetMaterial((ColorType)CharacterColor);
    }

    // Thay đổi Anim
    public void ChangeAnim(string animName)
    {
        if (currentAnimName != animName)
        {
            anim.ResetTrigger(Animator.StringToHash(currentAnimName));
            currentAnimName = animName;
            anim.SetTrigger(Animator.StringToHash(currentAnimName));
        }
    }

    // Thay đổi khu vực
    public virtual void ChangeStage()
    {
        if (intCurrentStage != -1)
        {
            currentStage.ColorOnStage[CharacterColor] = false;
        }
        currentStage = level.Stages[++intCurrentStage];
        currentStage.ColorOnStage[CharacterColor] = true;
        currentStage.CreateBrick(characterColor);
        currentStage.TimeSinceInactive = 0;
    }
    public int CharacterColor { get { return characterColor; } set { characterColor = value; } }
    public StackBrick StackBrick { get { return stackBrick; } }
    public int IntCurrentStage { get { return intCurrentStage; } set { intCurrentStage = value; } }
    public Stage CurrentStage { get { return currentStage; } set { currentStage = value; } }
    public Level Level { get { return level; } set { level = value; } }


}
