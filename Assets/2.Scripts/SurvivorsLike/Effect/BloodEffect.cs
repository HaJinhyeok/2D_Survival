using UnityEngine;

public class BloodEffect : MonoBehaviour
{
    // * Bleeding Animation Event
    void EndBleeding()
    {
        //Destroy(GameObject.FindGameObjectWithTag(Define.BleedingTag));
        Destroy(gameObject);
    }
}
