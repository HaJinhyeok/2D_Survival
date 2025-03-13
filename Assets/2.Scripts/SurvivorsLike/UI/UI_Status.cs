using TMPro;
using UnityEngine;

public class UI_Status : MonoBehaviour
{
    TMP_Text _statusText;

    private void Awake()
    {
        _statusText = GetComponentInChildren<TMP_Text>();
    }

    private void OnEnable()
    {
        _statusText.text =
            $"이름: {GameManager.Instance.PlayerInfo.PlayerName}\n" +
            $"최대 체력: {GameManager.Instance.PlayerInfo.MaxHp}\n" +
            $"현재 체력: {GameManager.Instance.PlayerInfo.CurrentHp}\n" +
            $"이동속도: {GameManager.Instance.PlayerInfo.Speed}\n" +
            $"아이템 획득 거리: {GameManager.Instance.PlayerInfo.MagneticDistance}\n" +
            $"경험치 추가 획득량: {(GameManager.Instance.PlayerInfo.ExpMultiplier - 1) * 100}%\n" +
            $"곡괭이 투척 개수: {GameManager.Instance.PlayerInfo.AxeNum}\n" +
            $"검 회전 개수: {GameManager.Instance.PlayerInfo.SwordNum}\n" +
            $"총알 발사 개수: {GameManager.Instance.ShotInfo.ShotNum}\n" +
            $"총알 발사 시간 간격: {GameManager.Instance.ShotInfo.Interval}s\n" +
            $"곡괭이 타격 횟수: {GameManager.Instance.WeaponInfo.AxeHitCount}\n" +
            $"곡괭이 추가 대미지: {GameManager.Instance.WeaponInfo.AxeAtk}\n" +
            $"폭발 대미지 {GameManager.Instance.WeaponInfo.ExplosionAtk}\n";

    }
}