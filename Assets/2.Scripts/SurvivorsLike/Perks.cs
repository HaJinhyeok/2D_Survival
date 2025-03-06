using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Perk
{
    public string Name;
    public UnityAction PerkEffect;
}
public static class Perks
{
    public static int FreePerkNum = 0;
    public static Dictionary<string, Perk> PerkDictionary = new Dictionary<string, Perk>();
    public static Dictionary<string, int> CostDictionary = new Dictionary<string, int>();
    public static void Initialize()
    {
        // CostDictionary 
        CostDictionary.Add(Define.SpeedUp, 0);
        FreePerkNum++;
        CostDictionary.Add(Define.HpUp, 0);
        FreePerkNum++;
        CostDictionary.Add(Define.AtkUp, 0);
        FreePerkNum++;
        CostDictionary.Add(Define.MagneticDistanceUp, 0);
        FreePerkNum++;

        CostDictionary.Add(Define.AddSword, 10);
        CostDictionary.Add(Define.AddAxe, 5);
        CostDictionary.Add(Define.AddExplosion, 10);

        // PerkDictionary
        PerkDictionary.Add(Define.SpeedUp, new Perk()
        {
            Name = "이동속도 증가",
            PerkEffect = () => 
            { 
                GameManager.Instance.PlayerInfo.Speed += 1;
                GameManager.Instance.IsDone = true;
            }
        });
        PerkDictionary.Add(Define.HpUp, new Perk()
        {
            Name = "최대 체력 증가",
            PerkEffect = () =>
            {
                GameManager.Instance.PlayerInfo.MaxHp += 5;
                GameManager.Instance.PlayerHp += 5;
                GameManager.Instance.IsDone = true;
            }
        });
        PerkDictionary.Add(Define.AtkUp, new Perk()
        {
            Name = "공격력 증가",
            PerkEffect = () =>
            {
                GameManager.Instance.PlayerInfo.Atk += 1;
                GameManager.Instance.IsDone = true;
            }
        });
        PerkDictionary.Add(Define.MagneticDistanceUp, new Perk()
        {
            Name = "자석 효과 범위 확대",
            PerkEffect = () =>
            {
                GameManager.Instance.PlayerInfo.MagneticDistance += 1;
            }
        });

        PerkDictionary.Add(Define.AddSword, new Perk()
        {
            Name = "검 개수 +1",
            PerkEffect = () =>
            {
                if (GameManager.Instance.Money < CostDictionary[Define.AddSword])
                {
                    // 특전 구매 불가
                    Debug.Log(Define.Warning_Not_Enough_Gold);
                    GameManager.Instance.IsDone = false;
                }
                else if (GameManager.Instance.PlayerInfo.SwordNum < 4)
                {
                    PoolManager.Instance.GetObject<SwordController>(ObjectManager.Instance.Player.transform.position);
                    GameManager.Instance.PlayerInfo.SwordNum = Mathf.Min(4, GameManager.Instance.PlayerInfo.SwordNum + 1);
                    GameManager.Instance.GetMoney(-CostDictionary[Define.AddSword]);
                    GameManager.Instance.IsDone = true;
                }
                else
                {
                    Debug.Log(Define.Warning_Full_Sword);
                    GameManager.Instance.IsDone = false;
                }
            }
        });
        PerkDictionary.Add(Define.AddAxe, new Perk()
        {
            Name = "곡괭이 개수 +1",
            PerkEffect = () =>
            {
                if (GameManager.Instance.Money < CostDictionary[Define.AddAxe])
                {
                    // 특전 구매 불가
                    Debug.Log(Define.Warning_Not_Enough_Gold);
                    GameManager.Instance.IsDone = false;
                }
                else if (GameManager.Instance.PlayerInfo.AxeNum < 4)
                {
                    GameManager.Instance.PlayerInfo.AxeNum = Mathf.Min(4, GameManager.Instance.PlayerInfo.AxeNum + 1);
                    GameManager.Instance.GetMoney(-CostDictionary[Define.AddAxe]);
                    GameManager.Instance.IsDone = true;
                }
                else
                {
                    Debug.Log(Define.Warning_Full_Pickaxe);
                    GameManager.Instance.IsDone = false;
                }
            }
        });
        PerkDictionary.Add(Define.AddExplosion, new Perk()
        {
            Name = "폭발 효과 업그레이드",
            PerkEffect=() => 
            {
                ObjectManager.Instance.Player.StartExplosion();
            }
        });
    }


}
