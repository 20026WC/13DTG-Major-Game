using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTree : MonoBehaviour
{
    // Code comes from Tutorial: https://www.youtube.com/watch?v=fE0R6WLpmrE 

    public static SkillTree skillTree;
    private void Awake() => skillTree = this;

    private PlayerMovement Player;

    public int[] SkillLevels;
    // List of the skill caps of all of the skill tree skills. E.g. Skill 2 has a upgrade limit of 5
    public int[] SkillCaps;
    // List of SkillNames.
    public string[] SkillNames;
    public string[] SkillDescriptions;

    public List<Skill> SkillList;
    public GameObject SkillHolder;

    public List<GameObject> ConnectorList;
    public GameObject ConnectorHolder;

    // This is the skillPoints that the Player has. 
    public int SkillPoints;

    private void Start()
    {
        // begins the player with 2 skill points. 
        SkillPoints = 2;
        // This establishes that there are 6 skills. 
        SkillLevels = new int[6];
        // List of the skill caps of all of the skill tree skills. E.g. Skill 2 has a upgrade limit of 5
        SkillCaps = new[] { 1, 5, 10, 10, 1, 1 };

        // List of SkillNames.
        SkillNames = new[] { "First Upgrade", "Speed Upgrade", "Health Upgrade", "Attack Upgrade", "Health Upgrade", "Health Upgrade", };
        // list of skill descriptions.
        SkillDescriptions = new[]
        {
            "Unlock Skill Tree",
            "Increase Speed by 10",
            "Increases Player Health by 10",
            "Increases Player Attack by 10",
            "Heal Health after every Stage",
            "Beating bosses restores Health",
        };

        foreach (var skill in SkillHolder.GetComponentsInChildren<Skill>()) SkillList.Add(skill);
        foreach (var connector in ConnectorHolder.GetComponentsInChildren<RectTransform>()) ConnectorList.Add(connector.gameObject);

        for (var i = 0; i < SkillList.Count; i++) SkillList[i].id = i;

        SkillList[0].ConnectedSkills = new[] { 1, 2, 3 };
        SkillList[2].ConnectedSkills = new[] { 4, 5 };

        UpdateAllSkillUI();
    }

    public void UpdateAllSkillUI()
    {
        foreach (var skill in SkillList) skill.UpdateUI();
    }
}