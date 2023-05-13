using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

using Penwyn.HexMap;

namespace Penwyn.Tools
{
    public class HexSelectorTabMenu : TabMenu
    {
        public Button HexButtonPrefab;

        public Button CreateHexButton(HexType type)
        {
            Button button = Instantiate(HexButtonPrefab, Contents[(int)type.Category].content.transform);
            button.GetComponentsInChildren<Image>().Last().sprite = type.Sprite;
            button.gameObject.name = type.name;
            return button;
        }
    }
}

