using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public struct Perk
{
    public string Name;
    public string ImagePath;
    public UnityAction PerkEffect;
}
public static class Perks
{
    public static Dictionary<string, Perk> PerkDictionary = new Dictionary<string, Perk>();

    public static void Initialize()
    {
        #region PerkDictionary
        if (!PerkDictionary.ContainsKey(Define.SpeedUp))
        {
            PerkDictionary.Add(Define.SpeedUp, new Perk()
            {
                Name = "이동속도 증가",
                ImagePath = Define.QuickImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.Speed =
                    Mathf.Min(8, GameManager.Instance.PlayerInfo.Speed + 1);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.MaxHpUp))
        {
            PerkDictionary.Add(Define.MaxHpUp, new Perk()
            {
                Name = "최대 체력 10% 증가",
                ImagePath = Define.MaxHpImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.MaxHp *= 1.1f;
                    GameManager.Instance.PlayerHp = GameManager.Instance.PlayerHp;
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.HpUp))
        {
            PerkDictionary.Add(Define.HpUp, new Perk()
            {
                Name = "체력 15 회복",
                ImagePath = Define.HealImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.CurrentHp =
                    MathF.Min(GameManager.Instance.PlayerInfo.MaxHp, GameManager.Instance.PlayerInfo.CurrentHp + 15);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AtkUp))
        {
            PerkDictionary.Add(Define.AtkUp, new Perk()
            {
                Name = "공격력 증가",
                ImagePath = Define.BuffImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.Atk =
                    Mathf.Min(10, GameManager.Instance.PlayerInfo.Atk + 1);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.MagneticDistanceUp))
        {
            PerkDictionary.Add(Define.MagneticDistanceUp, new Perk()
            {
                Name = "아이템 획득 거리 증가",
                ImagePath = Define.MagnetImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.MagneticDistance =
                    Mathf.Min(10, GameManager.Instance.PlayerInfo.MagneticDistance + 1);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ShotIntervalDown))
        {
            PerkDictionary.Add(Define.ShotIntervalDown, new Perk()
            {
                Name = "총알 발사 시간 간격 감소",
                ImagePath = Define.ShotImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.ShotInfo.Interval =
                    Mathf.Max(0.5f, GameManager.Instance.ShotInfo.Interval - 0.1f);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ShotNumUp))
        {
            PerkDictionary.Add(Define.ShotNumUp, new Perk()
            {
                Name = "총알 개수 증가",
                ImagePath = Define.ShotNumImagePath,
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
                ImagePath = Define.ShotSpeedImagePath,
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
                ImagePath = Define.SwordSpeedImagePath,
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
                Name = "곡괭이 공격력 증가",
                ImagePath = Define.AxeImagePath,
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
                Name = "곡괭이 타격 횟수 증가",
                ImagePath = Define.AxeImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.WeaponInfo.AxeHitCount =
                    Mathf.Min(15, GameManager.Instance.WeaponInfo.AxeHitCount + 2);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ExpQuantityUp))
        {
            PerkDictionary.Add(Define.ExpQuantityUp, new Perk()
            {
                Name = "경험치 획득량 10% 증가",
                ImagePath = Define.ExpImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.ExpMultiplier =
                    Mathf.Min(2f, GameManager.Instance.PlayerInfo.ExpMultiplier + 0.1f);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AddSword))
        {
            PerkDictionary.Add(Define.AddSword, new Perk()
            {
                Name = "검 개수 +1",
                ImagePath = Define.SwordImagePath,
                PerkEffect = () =>
                {
                    if (GameManager.Instance.PlayerInfo.SwordNum < 4)
                    {
                        PoolManager.Instance.GetObject<SwordController>(ObjectManager.Instance.Player.transform.position);
                        GameManager.Instance.PlayerInfo.SwordNum = Mathf.Min(4, GameManager.Instance.PlayerInfo.SwordNum + 1);
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
                ImagePath = Define.AxeImagePath,
                PerkEffect = () =>
                {
                    if (GameManager.Instance.PlayerInfo.AxeNum < 4)
                    {
                        GameManager.Instance.PlayerInfo.AxeNum = Mathf.Min(4, GameManager.Instance.PlayerInfo.AxeNum + 1);
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
                ImagePath = Define.ExplosionImagePath,
                PerkEffect = () =>
                {
                    ObjectManager.Instance.Player.StartExplosion();
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AuraUpgrade))
        {
            PerkDictionary.Add(Define.AuraUpgrade, new Perk()
            {
                Name = "오오라 대미지 업그레이드",
                ImagePath = Define.AuraImagePath,
                PerkEffect = () =>
                {
                    if (GameManager.Instance.WeaponInfo.AuraAtk == 0)
                    {
                        ObjectManager.Instance.Spawn<AuraController>(ObjectManager.Instance.Player.transform.position);
                        GameManager.Instance.WeaponInfo.AuraAtk++;
                        GameManager.Instance.IsDone = true;
                    }
                    else if (GameManager.Instance.WeaponInfo.AuraAtk < 3)
                    {
                        GameManager.Instance.WeaponInfo.AuraAtk++;
                        GameManager.Instance.IsDone = true;
                    }
                    else
                    {
                        UI_PopUp.PopUpAction(Define.Warning_Full_Aura_Atk);
                        GameManager.Instance.IsDone = false;
                    }
                }
            });
        }
        #endregion
    }

}
