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
    public Weapon weapon;

    [System.Serializable]
    public class StageData
    {
        public int stage=1;
        public int enemyKill=0;
        public float time=0;
    }
    public StageData stageData;

    public int KillCount
    {
        get { return stageData.enemyKill; }

        set
        {
            stageData.enemyKill = value;
            uiManager.SetKillText(value);

            if(value%10==0) // 10마리 처치시마다 아이템소환
            {
                spawnManager.ItemSpawn();
            }
        }
    }
    private void Update()
    {
        if (player.playerData.isDead)
            return;
        stageData.time += Time.deltaTime;
        uiManager.SetTime((int)stageData.time);
    }
}
