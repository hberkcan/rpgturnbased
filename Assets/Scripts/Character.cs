using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Character
{
    public string Name;
    public Stat[] Stats;
    public int Experience;
    public int Level;

    public Character(string name, Stat[] stats, int experience, int level)
    {
        Name = name;
        Stats = stats;
        Experience = experience;
        Level = level;
    }
}
