using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCharacters : MonoBehaviour
{
    [System.Serializable]
    public class SpriteList
    {
        public string characterName;
        public int skinVariant;
        [Space]
        public Sprite frontIdle;
        public Sprite frontOneArm1;
        public Sprite frontOneArm2;
        public Sprite frontOneArmUp1;
        public Sprite frontOneArmUp2;
        public Sprite frontTwoArms;
        [Space]
        public Sprite backIdle;
        public Sprite backOneArm1;
        public Sprite backOneArm2;
        public Sprite backOneArmUp1;
        public Sprite backOneArmUp2;
        public Sprite backTwoArms;
    }

    public SpriteList[] humanCharacters;
}
