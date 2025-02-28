using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Achievement
{
    public string id;            // 업적 ID
    public string title;         // 업적 이름
    public string description;   // 업적 설명
    public int goalValue;        // 목표 (예: 10마리 처치)
    public int currentValue;     // 현재 진행도
    public bool isCompleted;     // 완료 여부
    public int rewardAmount;     // 보상 (골드 등)
    public bool isRewarded;      // 보상 지급 여부

    public Achievement()
    {

    }
    public Achievement(string id, string title, string description, int goalValue, int rewardAmount)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.goalValue = goalValue;
        this.currentValue = 0;
        this.isCompleted = false;
        this.rewardAmount = rewardAmount;
        this.isRewarded = false;
    }
}


