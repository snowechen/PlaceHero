using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class player_data
{
    public int STR;
    public int DEX;
    public int VIT;
    public int INT;

    public int minStr { get { return STR / 6 + weapon_lv*2; } }
    public int maxStr { get { return STR / 4 + weapon_lv; } }
    public int maxvit { get { return VIT * 3 + 3; } }

    public int Def { get { return DEX / 3; } }


    public int weapon_lv;
    public int weapon_exp;
    public int weapon_nextexp { get { return (((int)Mathf.Pow((weapon_lv - 1), 3)) + 15) / 5 * ((weapon_lv - 1) * 2 + 20) + (10 - ((((int)Mathf.Pow((weapon_lv - 1), 3)) + 15) / 5 * ((weapon_lv - 1) * 2 + 20) % 10)) + (weapon_lv - 1) * 30; } }
    public int weapon_nextprice { get { return weapon_lv * 2 + 20; } }
    public float weapon_upexp { get { return (weapon_nextexp * 0.1f); } }
}

public class Player : MonoBehaviour {

    Animator anim;
    private bool isalive;
    public bool IsAlive { get { return isalive; } }
    private int maxHP;
    private int currHP;
    [SerializeField]
    private Text hp_text;
    [SerializeField]
    private Image hp_image;

    [SerializeField]
    private int level;
    [SerializeField]
    private Text level_text;
    public int Level { get { return level; } }

    private float currExp;
    private float nextExp;
    [SerializeField]
    private Text nextexp_text;

    [SerializeField]
    private int point;
    public int Point { get { return point; } set { point += value; } }
    [SerializeField]
    private Text point_text;

    [SerializeField]
    player_data data;

    private Enemy target;
    public player_data PlayerData { get { return data; } }
    public int Price { get; set; }
    [SerializeField]
    private Text price_txt;
    public bool StartFlag { get; set; }
	void Start () {
        StartFlag = false;
        anim = GetComponent<Animator>();
        maxHP = data.maxvit;
        currHP = maxHP;
        isalive = true;
        point_text.text = "ポイント：" + point;
        nextExp = (((int)Mathf.Pow((level - 1), 3)) + 15) / 5 * ((level - 1) * 2 + 20) + (10 - ((((int)Mathf.Pow((level - 1), 3)) + 15) / 5 * ((level - 1) * 2 + 20) % 10)) + (level - 1) * 30;
        StartCoroutine(Info_Updata());
    }
	
    public void start()
    {
        anim.SetBool("Start",true);
        StageController.instance.mapMoveStart();
        StartFlag = true;
    }

    void HP_check()
    {
        maxHP = data.maxvit;
        currHP = currHP > maxHP ? maxHP : currHP;
        currHP = currHP <= 0 ? 0 : currHP;
        if (currHP <= 0 && isalive) {
            isalive = false;
            anim.SetBool("Dead", true);
            StartCoroutine(ReSet());
        }

    }

    IEnumerator ReSet()
    {
        yield return new WaitForSeconds(3);
        isalive = true;
        currHP = maxHP;
        anim.SetBool("Dead", false);
        anim.SetBool("Battle", false);
        StageController.instance.MapReset();
        StageController.instance.mapMoveStart();
    }

    public void Damage(int damage)
    {
        int d = damage - data.Def;
        Debug.Log(d);
        if (d > 0) currHP -= d;
        else currHP -= 1;
        anim.SetTrigger("Damage");
        HP_check();
    }
    IEnumerator Info_Updata()
    {
        while (true)
        {
            HP_check();
            hp_image.fillAmount = (float)currHP / (float)maxHP;
            hp_text.text = currHP + " / " + maxHP;
            level_text.text = level.ToString();
            nextexp_text.text = "Next Level: " + currExp + " / " + nextExp;
            point_text.text = "ポイント：" + point;
            price_txt.text = "Coin：" + Price;
            yield return null;
        }
    }

    IEnumerator Attack()
    {
        while (isalive && target.IsAlive)
        {
            int index = Random.Range(0, 2);
            switch (index)
            {
                case 0:
                    anim.SetTrigger("Attack");
                    break;
                case 1:
                    anim.SetTrigger("Attack2");
                    break;
            }
            target.Damage(Random.Range(data.minStr, data.maxStr));
            yield return new WaitForSeconds(1);
        }
        if (isalive)
        {
            ExpUp(target.Exp);
            anim.SetBool("Battle", false);
            StageController.instance.mapMoveStart();
        }
    }

    void ExpUp(int exp)
    {
        currExp += exp;
        if (currExp >= nextExp)
        {
            StartCoroutine(LevelUp());
        }
    }

    IEnumerator LevelUp()
    {
        while (currExp >= nextExp)
        {
            level += 1;
            point += 5;
            currExp -= nextExp;
            currHP = maxHP;
            nextExp = (((int)Mathf.Pow((level - 1), 3)) + 15) / 5 * ((level - 1) * 2 + 20) + (10 - ((((int)Mathf.Pow((level - 1), 3)) + 15) / 5 * ((level - 1) * 2 + 20) % 10)) + (level - 1) * 30;
            yield return null;
        }
    }

    public void HpPlus()
    {
        int lossHP = maxHP - currHP;

        if(Price >= lossHP * 10)
        {
            Price -= lossHP * 10;
            currHP += lossHP;
        } 
        else
        {
            int cure = Price / 10;
            Price -= cure * 10;
            currHP += cure;
        }
       
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            anim.SetBool("Battle", true);
            StageController.instance.mapStop();
            target = other.GetComponent<Enemy>();
            StartCoroutine(Attack());
        }
    }
}
