using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Levels", menuName = "ScriptableObjects/Levels", order = 1)]
public class Level : ScriptableObject 
{     
    [SerializeField] private Vector3 _playerSpawn = new Vector3(100f, 100f, 0f);
    [SerializeField] private List<Vector3> _enemySpawn = new List<Vector3>();
    [SerializeField] private GameObject _levelPrefab;
    [SerializeField] private int _enemyNumber = 5;
    
    public Vector3 PlayerSpawn => _playerSpawn;
    public int EnemyNumber => _enemyNumber;
    public List<Vector3> EnemySpawn => _enemySpawn;
    public GameObject LevelPrefab => _levelPrefab;
                 
    public bool IsLevelDone {
        get
        {
            return PlayerPrefs.GetInt(name + ".isDone", 0) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(name + ".isDone", value ? 1 : 0);
        }
    }
}
