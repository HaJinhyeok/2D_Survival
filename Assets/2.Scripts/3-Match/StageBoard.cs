using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class StageBoard : MonoBehaviour
{
    public GameObject Cell;

    int _row = 7, _col = 7;
    Vector2Int[,] _cellPos;
    Dictionary<Vector2Int, GemType> _cellInfo = new Dictionary<Vector2Int, GemType>();

    private void Awake()
    {
        _cellPos = new Vector2Int[_row, _col];
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _col; j++)
            {
                _cellPos[i, j] = new Vector2Int(i - 3, j - 3);
            }
        }
    }
    void Start()
    {
        for (int i = 0; i < _row; i++)
        {
            for (int j = 0; j < _col; j++)
            {
                Instantiate(Cell, new Vector3(_cellPos[i, j].x, _cellPos[i, j].y, 0), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    enum GemType
    {
        Red,
        Blue,
        Green
    }
}
