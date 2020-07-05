using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacters : MonoBehaviour
{
    [System.Serializable]
    public class SpriteList
    {
        public string characterName;
        [Space]
        public Sprite frontIdle;
        public Sprite backIdle;
    }

    public SpriteList[] enemyCharacters;
}
