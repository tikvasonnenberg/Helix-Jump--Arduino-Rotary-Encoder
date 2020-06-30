using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
//defines all the game variables that can be changed each level
public class Platform
{
    private const int MIN_PART_COUNT = 1;
    private const int MAX_PART_COUNT = 11;
    private const int MIN_DEATH_PART_COUNT = 0;
    private const int MAX_DEATH_PART_COUNT = 11;

    [Range(MIN_PART_COUNT, MAX_PART_COUNT)]
    public int partCount = MAX_PART_COUNT;

    [Range(MIN_DEATH_PART_COUNT, MAX_DEATH_PART_COUNT)]
    public int deathPartCount = MIN_DEATH_PART_COUNT;//was 1 and not 0
}
[CreateAssetMenu(fileName ="NewLevel")]

public class Level : ScriptableObject
{
    public Color levelBackgroundColor=Color.white;
    public Color levelPlatformColor = Color.white;
    public Color levelBallColor = Color.white;
    public Color levelHelixColor = Color.white;
    public Color levelGoalColor = Color.white;
    public Color deathPartColor = Color.white;
    public List<Platform> platforms = new List<Platform>();

}
