using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Penwyn.Game
{
    /// <summary>
    /// To make sure the event lists are created.
    /// </summary>
    public class EventManager : MonoBehaviour
    {
        void Awake()
        {
            GameEventList.CreateInstance();
            HexTileEventList.CreateInstance();
        }
    }
}
