using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    public class GameEventList
    {
        public VoidEventChannel GameStarted;

        public static GameEventList Instance;

        public static void CreateInstance()
        {
            Instance = new GameEventList();
        }

        public GameEventList()
        {
            GameStarted = new VoidEventChannel();
        }
    }

}
