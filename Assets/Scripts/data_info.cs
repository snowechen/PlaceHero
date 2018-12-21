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
    private GameObject player;
    player_data data;

    public Button HpPlusBtn;
	void Start () {
        data = player.GetComponent<Player>().PlayerData;
	}
	
	// Update is called once per frame
	void Update () {
        STR.text = "力：" + data.STR;
        DEX.text = "敏：" + data.DEX;
        VIT.text = "体：" + data.VIT;
        INT.text = "智：" + data.INT;

        if (player.GetComponent<Player>().Price < 1)
        {
            HpPlusBtn.enabled = false;
            HpPlusBtn.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        }
        else { HpPlusBtn.enabled = true; HpPlusBtn.GetComponent<Image>().color = Color.white; }
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
}
