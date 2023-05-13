using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Penwyn.HexMap;

namespace Penwyn.Game
{
    public class HexTileEventChannel
    {
        public event UnityAction<HexTile> OnEventRaised;
        public void RaiseEvent(HexTile gameObject)
        {
            if (OnEventRaised != null)
                OnEventRaised?.Invoke(gameObject);
        }
    }
}