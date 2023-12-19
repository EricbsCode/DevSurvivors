using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelUpOptions
{
    public string ItemName;
    public string ItemType;
    public int ItemLevel;
    public string[] UpgradeStatTexts;
    public Sprite Picture;
    public int ChosenCount;
    public bool MaxLevelReached;
    public int FunctionID;
}
