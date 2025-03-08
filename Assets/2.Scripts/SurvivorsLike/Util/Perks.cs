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
        if (!CostDictionary.ContainsKey(Define.SpeedUp))
        {
            CostDictionary.Add(Define.SpeedUp, 0);
            FreePerkNum++;
        }
        if (!CostDictionary.ContainsKey(Define.HpUp))
        {
            CostDictionary.Add(Define.HpUp, 0);
            FreePerkNum++;
        }
        if (!CostDictionary.ContainsKey(Define.AtkUp))
        {
            CostDictionary.Add(Define.AtkUp, 0);
            FreePerkNum++;
        }
        if (!CostDictionary.ContainsKey(Define.MagneticDistanceUp))
        {
            CostDictionary.Add(Define.MagneticDistanceUp, 0);
            FreePerkNum++;
        }

        if (!CostDictionary.ContainsKey(Define.AddSword))
        {
            CostDictionary.Add(Define.AddSword, 10);
        }
        if (!CostDictionary.ContainsKey(Define.AddAxe))
        {
            CostDictionary.Add(Define.AddAxe, 5);
        }
        if (!CostDictionary.ContainsKey(Define.AddExplosion))
        {
            CostDictionary.Add(Define.AddExplosion, 10);
        }

        // PerkDictionary
        if (!PerkDictionary.ContainsKey(Define.SpeedUp))
        {
            PerkDictionary.Add(Define.SpeedUp, new Perk()
            {
                Name = "�̵��ӵ� ����",
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.Speed += 1;
                    GameManager.Instance.IsDone = true;
                }
            });
        }
        if (!PerkDictionary.ContainsKey(Define.HpUp))
        {
            PerkDictionary.Add(Define.HpUp, new Perk()
            {
                Name = "�ִ� ü�� ����",
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.MaxHp += 5;
                    GameManager.Instance.PlayerHp += 5;
                    GameManager.Instance.IsDone = true;
                }
            });
        }
        if (!PerkDictionary.ContainsKey(Define.AtkUp))
        {
            PerkDictionary.Add(Define.AtkUp, new Perk()
            {
                Name = "���ݷ� ����",
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.Atk += 1;
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.MagneticDistanceUp))
            PerkDictionary.Add(Define.MagneticDistanceUp, new Perk()
            {
                Name = "�ڼ� ȿ�� ���� Ȯ��",
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.MagneticDistance += 1;
                    GameManager.Instance.IsDone = true;
                }
            });

        if (!PerkDictionary.ContainsKey(Define.AddSword))
        {
            PerkDictionary.Add(Define.AddSword, new Perk()
            {
                Name = "�� ���� +1",
                PerkEffect = () =>
                {
                    if (GameManager.Instance.Money < CostDictionary[Define.AddSword])
                    {
                        // Ư�� ���� �Ұ�
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
        }

        if (!PerkDictionary.ContainsKey(Define.AddAxe))
        {
            PerkDictionary.Add(Define.AddAxe, new Perk()
            {
                Name = "��� ���� +1",
                PerkEffect = () =>
                {
                    if (GameManager.Instance.Money < CostDictionary[Define.AddAxe])
                    {
                        // Ư�� ���� �Ұ�
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
        }

        if (!PerkDictionary.ContainsKey(Define.AddExplosion))
        {
            PerkDictionary.Add(Define.AddExplosion, new Perk()
            {
                Name = "���� ȿ�� ���׷��̵�",
                PerkEffect = () =>
                {
                    if (GameManager.Instance.Money < CostDictionary[Define.AddExplosion])
                    {
                        // Ư�� ���� �Ұ�
                        Debug.Log(Define.Warning_Not_Enough_Gold);
                        GameManager.Instance.IsDone = false;
                    }
                    else
                    {
                        ObjectManager.Instance.Player.StartExplosion();
                        GameManager.Instance.IsDone = true;
                    }
                }
            });
        }
    }

}
