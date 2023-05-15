using System;
using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using NaughtyAttributes;
using Penwyn.Tools;

using TMPro;

namespace Penwyn.HexMap
{
    public class HexMapSerializer : SingletonMonoBehaviour<HexMapSerializer>
    {
        [Header("File Name")]
        public TMP_InputField FileNameTxt;
        public string FileSuffix;
        public const string SaveFolderPath = "Assets/Resources/LevelData/";

        [Header("Data")]
        private HexLevelData _readingLevel;
        private List<HexLevelData> _levelFiles;


        [Button("Save")]
        public void SaveLevel()
        {
            if (string.IsNullOrEmpty(FileNameTxt.text))
            {
                Debug.LogError("Please input file save name!!!");
                return;
            }

            if (!FolderExists(SaveFolderPath))
                Directory.CreateDirectory(SaveFolderPath);

            _readingLevel = new HexLevelData();
            CollectLevelData(FindObjectOfType<HexMapEditor>().PlacedHexes);

            File.WriteAllText(GetFilePath(SaveFolderPath, FileNameTxt.text, FileSuffix), JsonUtility.ToJson(_readingLevel));
        }

        [Button("Load")]
        public void LoadLevel()
        {
            if (string.IsNullOrEmpty(FileNameTxt.text))
            {
                Debug.LogError("Please input file load name!!!");
                return;
            }

            if (!FolderExists(SaveFolderPath))
                Directory.CreateDirectory(SaveFolderPath);
            else
            {
                _readingLevel = JsonUtility.FromJson<HexLevelData>(File.ReadAllText(GetFilePath(SaveFolderPath, FileNameTxt.text, FileSuffix)));
                //Debug.Log(File.ReadAllText(GetFilePath(SaveFolderPath, FileNameTxt.text, FileSuffix)));
                FindObjectOfType<HexMapGen>().Load(_readingLevel.HexMap.ToList());
            }
        }

        public void CollectLevelData(List<Hex> hexmap)
        {
            //Clean up empty cells
            hexmap.RemoveAll(x => x.Type.Category == HexCategory.EMPTY);

            //Assign Data
            _readingLevel.Name = FileNameTxt.text;
            _readingLevel.HexMap = hexmap;
        }

        public string GetSettingsPath(string folderPath, string levelName)
        {
            return GetFilePath(folderPath, levelName, FileSuffix);
        }

        /// <summary>
        /// Get FilePath
        /// </summary>
        /// <param name="folderPath">Folder path</param>
        /// <param name="levelName">Level name</param>
        /// <param name="prefix">Type of resource.</param>
        /// <returns></returns>
        public string GetFilePath(string folderPath, string levelName, string suffix)
        {
            return $"{folderPath}{levelName}_{suffix}.json";
        }

        /// <summary>
        /// Check if folder directory exists.
        /// </summary>
        /// <param name="folderPath">Folder path</param>
        private bool FolderExists(string folderPath)
        {
            return System.IO.Directory.Exists(Application.dataPath + "/" + folderPath.Remove(0, 7));
        }

        public HexLevelData ReadingLevel { get => _readingLevel; }

    }
}
