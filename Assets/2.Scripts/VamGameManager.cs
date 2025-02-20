using UnityEngine;

public class VamGameManager : MonoBehaviour
{
    #region SingleTon

    private static VamGameManager s_instance = null;

    public static VamGameManager Instance
    {
        get
        {
            if (s_instance == null)
                return null;
            return s_instance;
        }
    }

    private void Awake()
    {
        if (s_instance == null)
            s_instance = this;
        else
            Destroy(gameObject);
    }

    #endregion

    void Start()
    {
        
    }

}
