using System.Collections.Generic;
using System;
using UnityEngine;

#region Squad

[Serializable]
public class Squad
{
    public int id;
    public List<int> slot;

    public Squad(Squad squad)
    {
        id = squad.id;
        slot = new List<int>(squad.slot); // 리스트 깊은 복사
    }
}

[Serializable]
public class SquadData : ILoader<int, Squad>
{
    public List<Squad> squads = new();

    public Dictionary<int, Squad> MakeDict()
    {
        Dictionary<int, Squad> dict = new();
        foreach (Squad squad in squads)
            dict.Add(squad.id, squad);

        return dict;
    }
}

#endregion

#region Item

[Serializable]
public class Item
{
    public int id;
    public string name;
    public int count;
    public string desc;
    public string iconPath;
}

[Serializable]
public class ItemData : ILoader<int, Item>
{
    public List<Item> items = new();
    
    public Dictionary<int, Item> MakeDict()
    {
        Dictionary<int, Item> dict = new();
        foreach (Item item in items)
            dict.Add(item.id, item);

        return dict;
    }
}

#endregion

#region NikkeInfo

[Serializable]
public class NikkeInfo
{
    public int id;
    public string name;
    public string nikkeClass;
    public int burstLevel;
    public string element;
    public string rarity;
    public int hp;
    public int attack;
    public int defense;
    public NikkeColor color;
    public Weapon weapon;
    public List<Skill> skills;
}

[Serializable]
public class NikkeColor
{
    public float r;
    public float g;
    public float b;
}

[Serializable]
public class Weapon
{
    public string weaponClass;
    public int maxAmmo;
    public float reloadTime;
    public string controlType;
    public string description;
    public List<ValueData> values;
}

[Serializable]
public class Skill
{
    public int skillID;
    public int skillType;
    public string name;
    public string description;
    public string skillTypeName;
    public List<ValueData> values;
    public int cooldown;
    public string skillIconPath;
}

[Serializable]
public class ValueData
{
    public float percent;
    public float levelup;
}

[Serializable]
public class NikkeInfoData : ILoader<int, NikkeInfo>
{
    public List<NikkeInfo> characters = new();

    public Dictionary<int, NikkeInfo> MakeDict()
    {
        Dictionary<int, NikkeInfo> dict = new();
        foreach (NikkeInfo character in characters)
            dict.Add(character.id, character);

        return dict;
    }
}

#endregion

#region NikkeStatus

[Serializable]
public class NikkeStatus
{
    public int id; // 캐릭터 ID
    public int combatPoint;
    public int level; // 현재 레벨
    public int[] skillLevels; // 스킬 레벨 정보 (배열 형태로 변경)
}

[Serializable]
public class NikkeStatusData : ILoader<int, NikkeStatus>
{
    public List<NikkeStatus> characters = new();

    public Dictionary<int, NikkeStatus> MakeDict()
    {
        Dictionary<int, NikkeStatus> dict = new();
        foreach (NikkeStatus character in characters)
            dict.Add(character.id, character);

        return dict;
    }
}


#endregion

