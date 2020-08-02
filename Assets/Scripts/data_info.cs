using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class data_info : MonoBehaviour {
    [SerializeField]
    private Text STR;
    [SerializeField]
    private Text DEX;
    [SerializeField]
    private Text VIT;
    [SerializeField]
    private Text INT;

    [SerializeField]
    private Text WeaponLv; // 武器等级
    [SerializeField]
    private Text WeaponPrice; // 武器升级价格
    [SerializeField]
    private Image WeaponExp; //武器经验值

    [SerializeField]
    private GameObject player;
    player_data data;

    public Button HpPlusBtn;
	void Start () {
        data = player.GetComponent<Player>().PlayerData;
	}

    private void OnGUI()
    {
        STR.text = "力：" + data.STR;
        DEX.text = "敏：" + data.DEX;
        VIT.text = "体：" + data.VIT;
        INT.text = "智：" + data.INT;

        WeaponLv.text = "Sword Level：" + data.weapon_lv;
        WeaponExp.fillAmount = data.weapon_exp / data.weapon_nextexp;
        WeaponPrice.text = "price：" + data.weapon_nextprice;

        if (player.GetComponent<Player>().Price < 1)
        {
            HpPlusBtn.enabled = false;
            HpPlusBtn.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        else { HpPlusBtn.enabled = true; HpPlusBtn.GetComponent<Image>().color = Color.white; }
    }

    // Update is called once per frame
    void Update () {
        
	}

    public void STR_up()
    {
        Player P = player.GetComponent<Player>();
        if (P.Point > 0)
        {
            data.STR += 1;
            P.Point = -1;
        }
    }
    public void DEX_up()
    {
        Player P = player.GetComponent<Player>();
        if (P.Point > 0)
        {
            data.DEX += 1;
            P.Point = -1;
        }
       
    }
    public void VIT_up()
    {
        Player P = player.GetComponent<Player>();
        if (P.Point > 0)
        {
            data.VIT += 1;
            P.Point = -1;
        }
    }
    public void INT_up()
    {
        Player P = player.GetComponent<Player>();
        if (P.Point > 0)
        {
            data.INT += 1;
            P.Point = -1;
        }
    }

    public void Weapon_Up()
    {
        Player p = player.GetComponent<Player>();
        if(p.Price >= data.weapon_nextprice)
        {
            data.weapon_exp += data.weapon_upexp;
            p.Price -= data.weapon_nextprice;
            if(data.weapon_exp >= data.weapon_nextexp)
            {
                data.weapon_lv++;
                data.weapon_exp = 0;
            }
        }
    }
}
