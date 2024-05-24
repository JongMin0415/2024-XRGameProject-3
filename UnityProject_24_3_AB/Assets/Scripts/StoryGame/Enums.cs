using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enums
{

    public enum StoryType
    {
        MAIN,
        SUB,
        SERIAL
    }

    public enum EventType
    {
        NONE,
        GOTOBATTLE = 100,
        CheckSTR = 1000
    }

    public enum ResultType
    {
        AdExperience,
        GoToNextStory,
        GoToRandomStory
    }

    [System.Serializable]
    public class Stats
    {
        public int hpPoint;
        public int spPoint;

        public int currentHpPoint;
        public int currentSpPoint;
        public int currentXpPoint;

        public int strength;
        public int dexterity;
        public int consitution;
        public int Intelligence;
        public int wisdom;
        public int charisma;
    }
}
