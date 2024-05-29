using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using Unity.VisualScripting;

#if UNITY_EDITOR
[CustomEditor(typeof(GameSystem))]
public class GameSystemEdiot : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GameSystem gameSystem = (GameSystem)target;

        if (GUILayout.Button("Rest Stroy Modes"))
        {
            gameSystem.ResetStoryModels();
        }
    }
}
#endif

public class GameSystem : MonoBehaviour
{
    public static GameSystem instance;

    private void Awake()
    {
        instance = this;
    }

    public enum GAMESTATE
    {
        STORYSHOW,
        WAITSELECT,
        STORYEND
    }

    public Stats stats;
    public GAMESTATE currentState;
    public int currentStoryIndex = 1;
    public StoryModel[] stroyModels;

    public void Start()
    {
        ChangeState(GAMESTATE.STORYSHOW);
    }



#if UNITY_EDITOR


    [ContextMenu("Reset Story Models")]
    public void ResetStoryModels()
    {
        stroyModels = Resources.LoadAll<StoryModel>("");
    }
#endif

    public void ChangeState(GAMESTATE temp)
    {
        currentState = temp;

        if (currentState == GAMESTATE.STORYSHOW)
        {
            StoryShow(currentStoryIndex);
        }
    }

    public void ApplyChoice(StoryModel.Result result)
    {
        switch (result.resultType)
        {
            case StoryModel.Result.ResultType.ChangeHp:
                stats.currentHpPoint += result.value;
                ChangeStats(result);
                break;

            case StoryModel.Result.ResultType.AddExperience:
                stats.currentXpPoint += result.value;
                ChangeStats(result);
                break;

            case StoryModel.Result.ResultType.GoToNextStory:
                currentStoryIndex = result.value;
                ChangeState(GAMESTATE.STORYSHOW);
                ChangeStats(result);
                break;

            case StoryModel.Result.ResultType.GoTORandomStory:
                RandomStory();
                ChangeState(GAMESTATE.STORYSHOW);
                ChangeStats(result);
                break;
            default:
                Debug.LogError("Unknown type");
                break;


        }
    }

    public void ChangeStats(StoryModel.Result result)
    {
        if (result.stats.hpPoint > 0) stats.hpPoint += result.stats.hpPoint;
        if (result.stats.spPoint > 0) stats.spPoint += result.stats.spPoint;

        if (result.stats.currentHpPoint > 0) stats.currentHpPoint += result.stats.currentHpPoint;
        if (result.stats.currentSpPoint > 0) stats.currentSpPoint += result.stats.currentSpPoint;
        if (result.stats.currentXpPoint > 0) stats.currentXpPoint += result.stats.currentXpPoint;

        if (result.stats.strength > 0) stats.strength += result.stats.strength;
        if (result.stats.dexterity > 0) stats.dexterity += result.stats.dexterity;
        if (result.stats.consitution > 0) stats.consitution += result.stats.consitution;
        if (result.stats.wisdom > 0) stats.wisdom += result.stats.wisdom;
        if (result.stats.Intelligence > 0) stats.Intelligence += result.stats.Intelligence;
        if (result.stats.charisma > 0) stats.charisma += result.stats.charisma;


    }

    public void StoryShow(int number)
    {
        StoryModel tempStoryModels = FindStoryModel(number);

        //StorySystem.Instace.currentStoryModel = tempStoryMoels;
        //StorySystem.Instance.CoShowText();
    }

    StoryModel FindStoryModel(int number)
    {
        StoryModel tempStoryModels = null;
        for (int i = 0; i < stroyModels.Length; i++)         // for 문으로 StroyModel 을 검색하여 Number 와 같은 스토리 번호로 스토리 모델을 찾아 반환한다.
        {
            if (stroyModels[i].storyNumber == number)
            {
                tempStoryModels = stroyModels[i];
                break;
            }
        }
        return tempStoryModels;
    }

    StoryModel RandomStory()
    {
        StoryModel tempStoryModels = null;

        List<StoryModel> storyModelList = new List<StoryModel>();

        for (int i = 0; i < stroyModels.Length; i++)         // for 문으로 StroyModel 을 검색하여 Number 와 같은 스토리 번호로 스토리 모델을 찾아 반환한다.
        {
            if (stroyModels[i].storyType == StoryModel.STORYTYPE.MAIN)
            {
                storyModelList.Add(stroyModels[i]);
            }
        }

        tempStoryModels = storyModelList[Random.Range(0, storyModelList.Count)];
        currentStoryIndex = tempStoryModels.storyNumber;
        return tempStoryModels;
    }
}