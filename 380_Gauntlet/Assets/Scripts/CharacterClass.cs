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
    public float shotSpeed;
    public Material myColor;
    public GameObject projectile;

    [Header("Upgraded Stats")]
    public float armorUpgrade;
    public int magicUpgrade;
    public int shotPowerUpgrade;
    public float shotSpeedUpgrade;
    public int speedUpgrade;
    public int[] fightPowerUpgrade = new int[2];
}
