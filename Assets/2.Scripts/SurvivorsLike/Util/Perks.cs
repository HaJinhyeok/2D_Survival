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
        #region CostDictionary - Free
        if (!CostDictionary.ContainsKey(Define.SpeedUp))
        {
            CostDictionary.Add(Define.SpeedUp, 0);
            FreePerkNum++;
        }
        if (!CostDictionary.ContainsKey(Define.MaxHpUp))
        {
            CostDictionary.Add(Define.MaxHpUp, 0);
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
        if (!CostDictionary.ContainsKey(Define.ShotIntervalDown))
        {
            CostDictionary.Add(Define.ShotIntervalDown, 0);
            FreePerkNum++;
        }
        if (!CostDictionary.ContainsKey(Define.ShotNumUp))
        {
            CostDictionary.Add(Define.ShotNumUp, 0);
            FreePerkNum++;
        }
        if (!CostDictionary.ContainsKey(Define.ShotSpeedUp))
        {
            CostDictionary.Add(Define.ShotSpeedUp, 0);
            FreePerkNum++;
        }
        if (!CostDictionary.ContainsKey(Define.SwordSpeedUp))
        {
            CostDictionary.Add(Define.SwordSpeedUp, 0);
            FreePerkNum++;
        }
        if (!CostDictionary.ContainsKey(Define.AxeAtkUp))
        {
            CostDictionary.Add(Define.AxeAtkUp, 0);
            FreePerkNum++;
        }
        if (!CostDictionary.ContainsKey(Define.AxeHitCountUp))
        {
            CostDictionary.Add(Define.AxeHitCountUp, 0);
            FreePerkNum++;
        }
        #endregion

        #region CostDictionary - NonFree
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
        #endregion

        #region PerkDictionary - Free
        if (!PerkDictionary.ContainsKey(Define.SpeedUp))
        {
            PerkDictionary.Add(Define.SpeedUp, new Perk()
            {
                Name = "이동속도 증가",
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.Speed += 1;
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.MaxHpUp))
        {
            PerkDictionary.Add(Define.MaxHpUp, new Perk()
            {
                Name = "최대 체력 10% 증가",
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.MaxHp *= 1.1f;
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.HpUp))
        {
            PerkDictionary.Add(Define.HpUp, new Perk()
            {
                Name = "체력 15 회복",
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.CurrentHp += 15;
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AtkUp))
        {
            PerkDictionary.Add(Define.AtkUp, new Perk()
            {
                Name = "공격력 증가",
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.Atk += 1;
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.MagneticDistanceUp))
        {
            PerkDictionary.Add(Define.MagneticDistanceUp, new Perk()
            {
                Name = "자석 효과 범위 확대",
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.MagneticDistance += 1;
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ShotIntervalDown))
        {
            PerkDictionary.Add(Define.ShotIntervalDown, new Perk()
            {
                Name = "총알 발사 시간 간격 감소",
                PerkEffect = () =>
                {
                    GameManager.Instance.ShotInfo.Interval =
                    Mathf.Max(0.6f, GameManager.Instance.ShotInfo.Interval - 0.1f);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ShotNumUp))
        {
            PerkDictionary.Add(Define.ShotNumUp, new Perk()
            {
                Name = "총알 개수 증가",
                PerkEffect = () =>
                {
                    GameManager.Instance.ShotInfo.ShotNum =
                    Mathf.Min(36, GameManager.Instance.ShotInfo.ShotNum + 4);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ShotSpeedUp))
        {
            PerkDictionary.Add(Define.ShotSpeedUp, new Perk()
            {
                Name = "총알 속도 증가",
                PerkEffect = () =>
                {
                    GameManager.Instance.ShotInfo.Speed =
                    Mathf.Min(1000f, GameManager.Instance.ShotInfo.Speed + 50f);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.SwordSpeedUp))
        {
            PerkDictionary.Add(Define.SwordSpeedUp, new Perk()
            {
                Name = "검 회전 속도 증가",
                PerkEffect = () =>
                {
                    GameManager.Instance.WeaponInfo.SwordRotationSpeed =
                    Mathf.Min(360f, GameManager.Instance.WeaponInfo.SwordRotationSpeed + 90f);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AxeAtkUp))
        {
            PerkDictionary.Add(Define.AxeAtkUp, new Perk()
            {
                Name = "도끼 공격력 증가",
                PerkEffect = () =>
                {
                    GameManager.Instance.WeaponInfo.AxeAtk =
                    Mathf.Min(10, GameManager.Instance.WeaponInfo.AxeAtk + 1);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AxeHitCountUp))
        {
            PerkDictionary.Add(Define.AxeHitCountUp, new Perk()
            {
                Name = "도끼 타격 횟수 증가",
                PerkEffect = () =>
                {
                    GameManager.Instance.WeaponInfo.AxeHitCount =
                    Mathf.Min(10, GameManager.Instance.WeaponInfo.AxeHitCount + 1);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        #endregion

        #region PerkDictionary - NonFree
        if (!PerkDictionary.ContainsKey(Define.AddSword))
        {
            PerkDictionary.Add(Define.AddSword, new Perk()
            {
                Name = "검 개수 +1",
                PerkEffect = () =>
                {
                    if (GameManager.Instance.Money < CostDictionary[Define.AddSword])
                    {
                        // 특전 구매 불가
                        UI_PopUp.PopUpAction(Define.Warning_Not_Enough_Gold);
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
                        UI_PopUp.PopUpAction(Define.Warning_Full_Sword);
                        GameManager.Instance.IsDone = false;
                    }
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AddAxe))
        {
            PerkDictionary.Add(Define.AddAxe, new Perk()
            {
                Name = "곡괭이 개수 +1",
                PerkEffect = () =>
                {
                    if (GameManager.Instance.Money < CostDictionary[Define.AddAxe])
                    {
                        // 특전 구매 불가
                        UI_PopUp.PopUpAction(Define.Warning_Not_Enough_Gold);
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
                        UI_PopUp.PopUpAction(Define.Warning_Full_Pickaxe);
                        GameManager.Instance.IsDone = false;
                    }
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AddExplosion))
        {
            PerkDictionary.Add(Define.AddExplosion, new Perk()
            {
                Name = "폭발 효과 업그레이드",
                PerkEffect = () =>
                {
                    if (GameManager.Instance.Money < CostDictionary[Define.AddExplosion])
                    {
                        // 특전 구매 불가
                        UI_PopUp.PopUpAction(Define.Warning_Not_Enough_Gold);
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
        #endregion
    }

}
