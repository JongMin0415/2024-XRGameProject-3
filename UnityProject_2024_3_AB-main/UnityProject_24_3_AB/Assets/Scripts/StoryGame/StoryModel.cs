using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "NewStory", menuName = "ScriptableObjects/StoryModel")]
public class StoryModel : ScriptableObject
{
    public int storyNumber;
    public Texture2D mainImage;

    public enum STORYTYPE
    {
        MAIN,
        SUB,
        SERIAL
    }

    public STORYTYPE storyType;
    public bool storyDone;

    [TextArea(10, 10)]
    public string storyText;

    public Option[] options;

    [System.Serializable]
    public class Option
    {
        public string optionText;
        public string buttonText;

        public EventCheck eventCheck;
    }

    [System.Serializable]
    public class EventCheck
    {
        public int checkvalue;

        public enum EventType : int
        {
            NONE,
            GoToBattle,
            CheckSTR,
            CheckDEX,
            CheckCON,
            CheckINT,
            CheckWIS,
            CheckCHA
        }

        public EventType eventType;

        public Result[] sucessResult;       //선택지에 대한 효과 배열
        public Result[] failResult;
    }
    [System.Serializable]
    public class Result                 // 결과값 정보 데이터
    {
        public enum ResultType : int
        {
            ChangeHp,
            ChangeSp,
            AddExperience,
            GoToShop,
            GoToNextStory,
            GoTORandomStory
        }

        public ResultType resultType;
        public int value;
        public Stats stats;

    }
}