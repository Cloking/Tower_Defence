using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public static int money;
    public int StartMoney = 400;

    public static int lives;
    public int startLives = 20;

    public static int rounds;

    public void Start()
    {
        rounds = 0;
        money = StartMoney;
        lives = startLives;
    }

}
