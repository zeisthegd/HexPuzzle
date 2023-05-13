using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Penwyn.Tools;

namespace Penwyn.HexMap
{
    public class HexPlaceableLoader : MonoBehaviour
    {
        public HexSelectorTabMenu HexSelectorMenu;
        public List<HexType> PlaceableHexList;
        public List<Sprite> CategorySprites;

        private void Awake()
        {
            LoadCategoryTabs();
            LoadHexDataOnUI();
        }

        public void LoadCategoryTabs()
        {
            foreach (HexCategory category in Enum.GetValues(typeof(HexCategory)))
            {
                HexSelectorMenu.CreateButton(category.ToString(), CategorySprites[(int)category]);
                HexSelectorMenu.CreateContent(category.ToString());
                HexSelectorMenu.EnableTab(0);
            }
        }

        public void LoadHexDataOnUI()
        {
            foreach (HexType type in PlaceableHexList)
            {
                HexSelectorMenu.CreateHexButton(type).onClick.AddListener(() => { FindObjectOfType<HexMapEditor>().HexToPlace = type; });
            }
        }

    }
}
