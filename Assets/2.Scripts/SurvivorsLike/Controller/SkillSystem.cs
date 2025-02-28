using System.Collections;
using UnityEngine;

public class SkillSystem : MonoBehaviour
{
    void Start()
    {
        
    }

    IEnumerator CoStartLetter()
    {
        yield return new WaitForSeconds(5f);

    }
}
