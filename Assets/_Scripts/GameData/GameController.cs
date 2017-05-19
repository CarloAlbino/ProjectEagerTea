using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public enum EGameMode
    {
        Menu,
        FreeRoam,
        BattleRoam, 
        Battle, 
    }

    public class GameController : MonoBehaviour
    {
        public EGameMode gameMode { get; private set; }
    
        void Start()
        {
            gameMode = EGameMode.Menu;
        }
    }
}
