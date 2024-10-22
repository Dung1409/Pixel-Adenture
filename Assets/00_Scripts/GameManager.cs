using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager instance => _instance;

    [SerializeField] Text HP;
    public int hp;
    [SerializeField] int Score;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this; 
        }
        else
        {
            Destroy(this);
        }
        if (PlayerPrefs.HasKey("HP"))
        {
            hp = PlayerPrefs.GetInt("HP"); 
        }
        else
        {
            hp = 100;
        }
    }

    public void  UpgradeText()
    {
        HP.text = hp.ToString();
    }

    public void UpgradeScore()
    {
        Score += 1;
    }

    public void getDamage(int _dmg)
    {
        hp -= _dmg;
        PlayerPrefs.SetInt("HP", hp);
    }

    private void Update()
    {
        UpgradeText();
    }
}
