using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterClass", menuName = "ScriptableObjects/CharacterClass", order = 1)]
public class CharacterClass : ScriptableObject
{
    public int health;
    public float armor;
    public int moveSpeed;
    public int magic;
    public int[] damage = new int[2];
    public Material myColor;
    public GameObject projectile;
}
