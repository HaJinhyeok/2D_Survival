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
    public static List<string> PerkNameList = new List<string>();

    public static void Initialize()
    {
        #region PerkDictionary
        if (!PerkDictionary.ContainsKey(Define.SpeedUp))
        {
            PerkNameList.Add(Define.SpeedUp);
            PerkDictionary.Add(Define.SpeedUp, new Perk()
            {
                Name = "이동속도 증가",
                ImagePath = Define.QuickImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.Speed += 1f;
                    if (GameManager.Instance.PlayerInfo.Speed >= Define.MaxSpeed)
                        RemovePerk(Define.SpeedUp);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.MaxHpUp))
        {
            PerkNameList.Add(Define.MaxHpUp);
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
            PerkNameList.Add(Define.HpUp);
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
            PerkNameList.Add(Define.AtkUp);
            PerkDictionary.Add(Define.AtkUp, new Perk()
            {
                Name = "공격력 증가",
                ImagePath = Define.BuffImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.Atk += 1;
                    if (GameManager.Instance.PlayerInfo.Atk >= Define.MaxAtk)
                        RemovePerk(Define.AtkUp);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.MagneticDistanceUp))
        {
            PerkNameList.Add(Define.MagneticDistanceUp);
            PerkDictionary.Add(Define.MagneticDistanceUp, new Perk()
            {
                Name = "아이템 획득 거리 증가",
                ImagePath = Define.MagnetImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.MagneticDistance += 1;
                    if (GameManager.Instance.PlayerInfo.MagneticDistance >= Define.MaxMagneticDistance)
                        RemovePerk(Define.MagneticDistanceUp);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ShotIntervalDown))
        {
            PerkNameList.Add(Define.ShotIntervalDown);
            PerkDictionary.Add(Define.ShotIntervalDown, new Perk()
            {
                Name = "총알 발사 시간 간격 감소",
                ImagePath = Define.ShotImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.ShotInfo.Interval -= 0.1f;
                    if (GameManager.Instance.ShotInfo.Interval <= Define.MinShotInterval)
                        RemovePerk(Define.ShotIntervalDown);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ShotNumUp))
        {
            PerkNameList.Add(Define.ShotNumUp);
            PerkDictionary.Add(Define.ShotNumUp, new Perk()
            {
                Name = "총알 개수 증가",
                ImagePath = Define.ShotNumImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.ShotInfo.ShotNum += 4;
                    if (GameManager.Instance.ShotInfo.ShotNum >= Define.MaxShotNum)
                        RemovePerk(Define.ShotNumUp);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ShotSpeedUp))
        {
            PerkNameList.Add(Define.ShotSpeedUp);
            PerkDictionary.Add(Define.ShotSpeedUp, new Perk()
            {
                Name = "총알 속도 증가",
                ImagePath = Define.ShotSpeedImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.ShotInfo.Speed += 1f;
                    if (GameManager.Instance.ShotInfo.Speed >= Define.MaxShotSpeed)
                        RemovePerk(Define.ShotSpeedUp);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.SwordSpeedUp))
        {
            PerkNameList.Add(Define.SwordSpeedUp);
            PerkDictionary.Add(Define.SwordSpeedUp, new Perk()
            {
                Name = "검 회전 속도 증가",
                ImagePath = Define.SwordSpeedImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.WeaponInfo.SwordRotationSpeed += 90f;
                    if (GameManager.Instance.WeaponInfo.SwordRotationSpeed >= Define.MaxSwordSpeed)
                        RemovePerk(Define.SwordSpeedUp);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AxeAtkUp))
        {
            PerkNameList.Add(Define.AxeAtkUp);
            PerkDictionary.Add(Define.AxeAtkUp, new Perk()
            {
                Name = "곡괭이 공격력 증가",
                ImagePath = Define.AxeImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.WeaponInfo.AxeAtk += 1;
                    if (GameManager.Instance.WeaponInfo.AxeAtk >= Define.MaxAxeAtk)
                        RemovePerk(Define.AxeAtkUp);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AxeHitCountUp))
        {
            PerkNameList.Add(Define.AxeHitCountUp);
            PerkDictionary.Add(Define.AxeHitCountUp, new Perk()
            {
                Name = "곡괭이 타격 횟수 증가",
                ImagePath = Define.AxeImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.WeaponInfo.AxeHitCount += 2;
                    if (GameManager.Instance.WeaponInfo.AxeHitCount >= Define.MaxAxeHitCount)
                        RemovePerk(Define.AxeHitCountUp);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ExpMultiplierUp))
        {
            PerkNameList.Add(Define.ExpMultiplierUp);
            PerkDictionary.Add(Define.ExpMultiplierUp, new Perk()
            {
                Name = "경험치 획득량 10% 증가",
                ImagePath = Define.ExpImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.PlayerInfo.ExpMultiplier += 0.1f;
                    if (GameManager.Instance.PlayerInfo.ExpMultiplier >= Define.MaxExpMultiplier)
                        RemovePerk(Define.ExpMultiplierUp);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AddSword))
        {
            PerkNameList.Add(Define.AddSword);
            PerkDictionary.Add(Define.AddSword, new Perk()
            {
                Name = "검 개수 +1",
                ImagePath = Define.SwordImagePath,
                PerkEffect = () =>
                {
                    PoolManager.Instance.GetObject<SwordController>(ObjectManager.Instance.Player.transform.position);
                    GameManager.Instance.PlayerInfo.SwordNum++;

                    if (GameManager.Instance.PlayerInfo.SwordNum >= Define.MaxSwordNum)
                        RemovePerk(Define.AddSword);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AddAxe))
        {
            PerkNameList.Add(Define.AddAxe);
            PerkDictionary.Add(Define.AddAxe, new Perk()
            {
                Name = "곡괭이 개수 +1",
                ImagePath = Define.AxeImagePath,
                PerkEffect = () =>
                {
                    if (++GameManager.Instance.PlayerInfo.AxeNum >= Define.MaxAxeNum)
                        RemovePerk(Define.AddAxe);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ExplosionRadiusUp))
        {
            PerkNameList.Add(Define.ExplosionRadiusUp);
            PerkDictionary.Add(Define.ExplosionRadiusUp, new Perk()
            {
                Name = "폭발 범위 증가",
                ImagePath = Define.ExplosionImagePath,
                PerkEffect = () =>
                {
                    if (GameManager.Instance.WeaponInfo.ExplosionRadius == Define.InitExplosionRadius)
                    {
                        ObjectManager.Instance.Player.StartExplosion();
                    }
                    GameManager.Instance.WeaponInfo.ExplosionRadius += 1f;
                    if (GameManager.Instance.WeaponInfo.ExplosionRadius >= Define.MaxExplosionRadius)
                        RemovePerk(Define.ExplosionRadiusUp);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.ExplosionIntervalDown))
        {
            PerkNameList.Add(Define.ExplosionIntervalDown);
            PerkDictionary.Add(Define.ExplosionIntervalDown, new Perk()
            {
                Name = "폭발 시간 간격 감소",
                ImagePath = Define.ExplosionImagePath,
                PerkEffect = () =>
                {
                    GameManager.Instance.WeaponInfo.ExplosionInterval -= 1f;
                    if (GameManager.Instance.WeaponInfo.ExplosionInterval <= Define.MinExplosionInterval)
                        RemovePerk(Define.ExplosionIntervalDown);
                    GameManager.Instance.IsDone = true;
                }
            });
        }

        if (!PerkDictionary.ContainsKey(Define.AuraUpgrade))
        {
            PerkNameList.Add(Define.AuraUpgrade);
            PerkDictionary.Add(Define.AuraUpgrade, new Perk()
            {
                Name = "오오라 대미지 업그레이드",
                ImagePath = Define.AuraImagePath,
                PerkEffect = () =>
                {
                    if (GameManager.Instance.WeaponInfo.AuraAtk == 0)
                    {
                        ObjectManager.Instance.Spawn<AuraController>(ObjectManager.Instance.Player.transform.position);
                    }
                    if (++GameManager.Instance.WeaponInfo.AuraAtk >= Define.MaxAuraAtk)
                        RemovePerk(Define.AuraUpgrade);
                    GameManager.Instance.IsDone = true;
                }
            });
        }
        #endregion
    }

    public static void RemovePerk(string perkName)
    {
        PerkDictionary.Remove(perkName);
        PerkNameList.Remove(perkName);

        //PrintPerkList();
    }

    static void PrintPerkList()
    {
        Debug.Log("***Print PerkNameList***");
        for (int i = 0; i < PerkNameList.Count; i++)
        {
            Debug.Log(PerkNameList[i]);
        }
    }
}