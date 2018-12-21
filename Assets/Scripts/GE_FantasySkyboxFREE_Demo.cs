#region Namespaces

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

#endregion // Namespaces

public class GE_FantasySkyboxFREE_Demo : MonoBehaviour
{


	#region Variables
	
	[System.Serializable]		
	public class LightAndSky
	{
		// Name
		public string m_Name;
		
		// Light
		public Light m_Light;
		
		// Skybox
		public Material m_Skybox;
		
		// Fog
		public Color m_FogColor;
		
		// Ambient
		public Color m_AmbientLight;
	}
	
	// List of LightAndSky class
	public LightAndSky[] m_LightAndSkyList;

	// Index to current Skybox
	int m_CurrentSkyBox = 2;
	
#endregion // Variables

	

#region MonoBehaviour

	
	void Start ()
	{

		// Display first skybox in m_LightAndSkyList
		SetSkyBox(m_CurrentSkyBox);

		// Update UI Text elements
		//UpdateDetailsText();
		//UpdateHowToText();
	}
	
	
	void Update ()
	{
		// User press Left key
		//if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
		//{
		//	// Show previous skybox
		//	OnPreviousSkybox();
		//}
		//// User press Right key
		//if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
		//{
		//	// Show next skybox
		//	OnNextSkybox();
		//}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		Debug.Log("OnTriggerExit="+other.name);
		
		// Reset player position when user move it away from terrain
		this.transform.localPosition = new Vector3(0,1,0);
	}

	#endregion // MonoBehaviour

	// ########################################
	// Switch skybox functions
	// ########################################

	#region Switch skybox functions

	// Switch to previous skybox
	public void OnPreviousSkybox()
	{
		SwitchSkyBox(-1);
		//UpdateDetailsText();
	}
	
	// Switch to next skybox
	public void OnNextSkybox()
	{
		SwitchSkyBox(+1);
		//UpdateDetailsText();
	}

	#endregion // MonoBehaviour

	// ########################################
	// Show skybox functions
	// ########################################

	#region Show skybox functions

	// Switch to a skybox by direction
	// DiffNum less than 0 means previous skybox
	// DiffNum larger than 0 means next skybox
	void SwitchSkyBox(int DiffNum)
	{
		// Update add m_CurrentSkyBox with DiffNum
		m_CurrentSkyBox += DiffNum;
		
		// Clamp m_CurrentSkyBox between 0 and m_LightAndSkyList.Length
		if(m_CurrentSkyBox<0)
		{
			m_CurrentSkyBox = m_LightAndSkyList.Length-1;
		}
		if(m_CurrentSkyBox>=m_LightAndSkyList.Length)
		{
			m_CurrentSkyBox = 0;
		}
		
		// Switch skybox in RenderSettings
		RenderSettings.skybox = m_LightAndSkyList[m_CurrentSkyBox].m_Skybox;
		
		// Switch light
		for(int i=0;i<m_LightAndSkyList.Length;i++)
		{
			m_LightAndSkyList[i].m_Light.gameObject.SetActive(false);
		}
		m_LightAndSkyList[m_CurrentSkyBox].m_Light.gameObject.SetActive(true);
		
		// Enable fog
		RenderSettings.fog = true;
		
		// Set the fog color
		if(m_CurrentSkyBox>=0 && m_CurrentSkyBox<m_LightAndSkyList.Length)
		{
			RenderSettings.fogColor = m_LightAndSkyList[m_CurrentSkyBox].m_FogColor;
		}
		else
		{
			RenderSettings.fogColor = Color.white;
		}
		
		// Set the ambient lighting
		if(m_CurrentSkyBox>=0 && m_CurrentSkyBox<m_LightAndSkyList.Length)
		{
			RenderSettings.ambientLight = m_LightAndSkyList[m_CurrentSkyBox].m_AmbientLight;
		}
		else
		{
			RenderSettings.ambientLight = Color.white;
		}
	}
    void SetSkyBox(int DiffNum)
    {
        // Update add m_CurrentSkyBox with DiffNum
        m_CurrentSkyBox = DiffNum;

        // Clamp m_CurrentSkyBox between 0 and m_LightAndSkyList.Length
        if (m_CurrentSkyBox < 0)
        {
            m_CurrentSkyBox = m_LightAndSkyList.Length - 1;
        }
        if (m_CurrentSkyBox >= m_LightAndSkyList.Length)
        {
            m_CurrentSkyBox = 0;
        }

        // Switch skybox in RenderSettings
        RenderSettings.skybox = m_LightAndSkyList[m_CurrentSkyBox].m_Skybox;

        // Switch light
        for (int i = 0; i < m_LightAndSkyList.Length; i++)
        {
            m_LightAndSkyList[i].m_Light.gameObject.SetActive(false);
        }
        m_LightAndSkyList[m_CurrentSkyBox].m_Light.gameObject.SetActive(true);

        // Enable fog
        RenderSettings.fog = true;

        // Set the fog color
        if (m_CurrentSkyBox >= 0 && m_CurrentSkyBox < m_LightAndSkyList.Length)
        {
            RenderSettings.fogColor = m_LightAndSkyList[m_CurrentSkyBox].m_FogColor;
        }
        else
        {
            RenderSettings.fogColor = Color.white;
        }

        // Set the ambient lighting
        if (m_CurrentSkyBox >= 0 && m_CurrentSkyBox < m_LightAndSkyList.Length)
        {
            RenderSettings.ambientLight = m_LightAndSkyList[m_CurrentSkyBox].m_AmbientLight;
        }
        else
        {
            RenderSettings.ambientLight = Color.white;
        }
    }
    #endregion // Show skybox functions

    // ########################################
    // Update UI text functions
    // ########################################

    //#region Update UI text functions



    //// Update details UI Text
    //void UpdateDetailsText()
    //{
    //	// Update ItemNum text
    //	GameObject Text_ItemNum = GameObject.Find("Text_ItemNum");
    //	if(Text_ItemNum!=null)
    //	{ 				
    //		Text pText = Text_ItemNum.GetComponent<Text>();
    //		pText.text = string.Format("{0:00} of {1:00}",m_CurrentSkyBox+1, m_LightAndSkyList.Length);
    //	}

    //	// Update Details text
    //	GameObject Text_Details = GameObject.Find("Text_Details");
    //	if(Text_Details!=null)
    //	{ 				
    //		Text pText = Text_Details.GetComponent<Text>();
    //		pText.text = string.Format(m_LightAndSkyList[m_CurrentSkyBox].m_Name);
    //	}
    //}

    //// Update how to UI Text
    //void UpdateHowToText()
    //{
    //	// Find Text_HowTo in the scene
    //	GameObject Text_HowTo = GameObject.Find("Text_HowTo");
    //	if(Text_HowTo!=null)
    //	{
    //		// Update text according to target platform
    //		if(Application.platform == RuntimePlatform.IPhonePlayer ||
    //			Application.platform == RuntimePlatform.Android)//  ||
    //			//Application.platform == RuntimePlatform.BlackBerryPlayer ||
    //               //Application.platform == RuntimePlatform.WSAPlayer)
    //		{
    //			Text pText = Text_HowTo.GetComponent<Text>();
    //			pText.text = "Move: Joystick on left | Look: Joystick on right | Change Skybox: Tap";
    //		}
    //		else
    //		{
    //			Text pText = Text_HowTo.GetComponent<Text>();
    //			pText.text = "Switch Skybox: Left/Right Arrow | Turn: Mouse Drag | Release Mouse: ESC Button";
    //		}
    //	}
    //}

    //#endregion // Update UI text functions

}
