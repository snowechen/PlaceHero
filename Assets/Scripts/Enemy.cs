using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random; 

public class Enemy : MonoBehaviour {
    [SerializeField]
    private int EnemyHp;
    private int maxHp;
    [SerializeField]
    private int level;
    private int exp;
    public int Exp { get { return exp; } }
    private bool battle;

    Animator anim;

    private bool isalive;
    public bool IsAlive { get { return isalive; } }
    private Player target;

    [SerializeField]
    private int damage;

    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private GameObject Coin;
    public void Init(int turn,int lv)
    {
        level = Random.Range(lv-1<1?1:lv-1, lv + 4);
        EnemyHp = 10 + level * 10;
        maxHp = EnemyHp;
        exp = (level-1) * 2 + 20;
        battle = false;
        isalive = true;
        damage = Random.Range(level, level*3+turn);
        Debug.Log(level+" HP"+EnemyHp+", exp"+exp+", Damage:"+damage);
        StartCoroutine(move());
       // Enemy_UI.instance.gameObject.SetActive(true);
    }
	void Start () {
        anim = GetComponent<Animator>();
        Enemy_UI.instance.show();
        Enemy_UI.instance.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.6f, 0)) / Enemy_UI.instance.UIscale;
    }
	
    IEnumerator move()
    {
        while (!battle)
        {
            transform.position += new Vector3(-0.02f, 0, 0);
            yield return null;
        }
    }


    IEnumerator Attack()
    {
        while (isalive && target.IsAlive)
        {
            yield return new WaitForSeconds(attackSpeed);
            anim.SetTrigger("Attack");
            
        }
        if (!target.IsAlive) Dead(0,false);
    }

    private void Update()
    {
        if (target!=null && !isalive)
        {
            if(!target.GetComponent<Animator>().GetBool("Battle"))
            transform.position += new Vector3(-0.01f, 0, 0);
        }
            Enemy_UI.instance.level.text = "Lv." + level;
            Enemy_UI.instance.hpbar.fillAmount = (float)EnemyHp / (float)maxHp;
            Enemy_UI.instance.GetComponent<RectTransform>().anchoredPosition = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0, 0.6f, 0)) / Enemy_UI.instance.UIscale;
    }

    public void Damage(int damage)
    {
        EnemyHp -= damage;
        anim.SetTrigger("Damage");
        if (EnemyHp <= 0)
        {
            Dead(4,true);
            EnemyController.instance.TurnPlus();
        }
    }

    void Dead(float t,bool drop)
    {
        //if (drop)
        //{
            
        //}
        isalive = false;
        anim.SetBool("Dead", true);
        Destroy(gameObject, t);
        GetComponent<Collider>().enabled = false;
        StartCoroutine(move());
        Enemy_UI.instance.hid();
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("Battle", true);
            target = other.GetComponent<Player>();
            battle = true;
            StartCoroutine(Attack());
        }
    }

    public void OnEvent(string key)
    {
        Debug.Log("EnemyEvent: "+key);
        switch (key)
        {
            case "Dead":
                target.Price += level * 10;
                Instantiate(Coin, transform.position, Quaternion.LookRotation(Vector3.up));
                break;
            case "Attack":
                target.Damage(damage);
                break;
        }
    }
    
}
