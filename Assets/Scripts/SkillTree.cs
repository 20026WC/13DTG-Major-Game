using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    // Code comes from Tutorial: https://www.youtube.com/watch?v=fE0R6WLpmrE 

    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    public int[] SkillLevels;
    public int[] SkillCaps;
    public string[] SkillNames;
    public string[] SkillDescriptions;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public int SkillPoints;

    private void Start()
    {
        SkillPoints = 20;

        SkillLevels = new int[6];
        SkillCaps = new[] { 1, 5, 5, 2, 10, 10 };

        SkillNames = new[] { "Upgrade 1", "Upgrade 2", "Upgrade 3", "Upgrade 4", "Booster 5", "Booster 6", };
        SkillDescriptions = new[]
        {
            "D",
            "E",
            "U",
            "P",
            "L",
            "M",
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);

        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList) skill.UpdateUI();
    }
}
