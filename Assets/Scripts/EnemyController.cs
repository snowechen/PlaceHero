using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public static EnemyController instance;
    [SerializeField]
    private GameObject Enemy;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Text Turn_txt;
    private int[] turn;
    private GameObject enemy;

    public void TurnPlus() { turn[0] += 1;
        if (turn[0] >= 10)
        {
            turn[1] += 1;
            turn[0] = 0;
        }
    }
    private void Start()
    {
        instance = this;
        turn = new int[2];
        StartCoroutine(Updata());
    }

    IEnumerator Updata()
    {
        while (true)
        {
            if (player.IsAlive && player.StartFlag && enemy == null)
            {
                enemy = Instantiate(Enemy, player.transform.position + new Vector3(6, 0, 0),Enemy.transform.rotation) as GameObject;
                enemy.GetComponent<Enemy>().Init(turn[1], player.Level);
                Turn_txt.text = "Turn: " + turn[1] + " - "+turn[0] ;
            }
            yield return new WaitForSeconds(1);
        }
    }
}
