using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PassiveItemScriptableObject",menuName = "ScriptableObjects/PassiveItem")]
public class PassiveItemScriptableObject : ScriptableObject
{
    [SerializeField]
    float multipier;
    public float Multipier { get => multipier; private set => multipier = value; }

    [SerializeField]
    int level;
    public int Level { get => level; private set => level = value; }

    [SerializeField]
    GameObject nextLevelPrefab;
    public GameObject NextLevelPrefab { get => nextLevelPrefab; private set => nextLevelPrefab = value; }

    [SerializeField]
    Sprite icon;
    public Sprite Icon { get => icon; private set => icon = value; }
}
