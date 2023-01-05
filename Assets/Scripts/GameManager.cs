using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()=>instance=this;

    public Player player;
    public JoyStickManager joystickManager;
    public UiManager uiManager;
    public PoolManager poolManager;
    public SpawnManager spawnManager;

    [System.Serializable]
    public class StageData
    {
        public int stage=1;
        public int enemyKill=0;
        public float time=0;
    }
    public StageData stageData;

    private void Update()
    {
        stageData.time += Time.deltaTime;
        uiManager.SetTime((int)stageData.time);
    }
}
