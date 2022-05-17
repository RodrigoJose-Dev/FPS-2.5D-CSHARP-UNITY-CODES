using UnityEngine;
using UnityEngine.UI;
using TMPro;

[ExecuteInEditMode]
public class CanvasManager : MonoBehaviour
{
	//instance CanvasManager
	private static CanvasManager _instance;
	public static CanvasManager Instance { get { return _instance; } }

	[Header("Canvas Text Settings")]
	[SerializeField]
	[Tooltip("This property works with the TextMesh ammo")]
	private TextMeshProUGUI ammoCount;
	[SerializeField]
	[Tooltip("This property works with the TextMesh heath")]
	private TextMeshProUGUI healthCount;
	[SerializeField]
	[Tooltip("This property works with the TextMesh armor")]
	private TextMeshProUGUI armorCount;
	[Header("Canvas UI Settings")]
	[SerializeField]
	[Tooltip("This property works with the image health on the screen")]
	private Image healthIndicator;
	[SerializeField]
	[Tooltip("This property works with the sprites")]
	private Sprite health1, health2, health3,
		health4, health5;
	[SerializeField]
	[Tooltip("This property works with the sprites of the keys")]
	private GameObject redKey, greenKey, blueKey;
	[SerializeField]
	[Tooltip("This property works with the take damage img")]
	private Image takeDamageImg;
	[SerializeField]
	[Tooltip("This property works with the menu pause panel")]
	private GameObject menuPausePnl;

	
	private void Awake()
	{
		if(_instance != null && _instance != this)
		{
			Destroy(this.gameObject);
		}
		else
		{
			_instance = this;
		}

		menuPausePnl.SetActive(false);
	}
	

	//methods to update UI ammo, health, armor and health indicator

	public void UpdateAmmo(int ammoValue)
	{
		ammoCount.text = ammoValue.ToString();
	}

	public void UpdateHealth(int healthValue)
	{
		healthCount.text = healthValue.ToString() + "%";
		UpdateHealthIndicator(healthValue);
	}

	public void UpdateArmor(int armorValue)
	{
		armorCount.text = armorValue.ToString() + "%";
	}

	public void UpdateHealthIndicator(int healthValue)
	{
		if(healthValue >= 80)
		{
			healthIndicator.sprite = health1;
		}

		if(healthValue < 80 && healthValue >= 60)
		{
			healthIndicator.sprite = health2;
		}

		if(healthValue < 60 && healthValue > 30)
		{
			healthIndicator.sprite = health3;
		}

		if(healthValue < 30 && healthValue > 0)
		{
			healthIndicator.sprite = health4;
		}

		if(healthValue <= 0)
		{
			healthIndicator.sprite = health5;
		}
	}

	//this method activate keys in the UI
	public void UpdateKeys(string keyColor)
	{
		if(keyColor == "red")
		{
			redKey.SetActive(true);
		}

		if (keyColor == "green")
		{
			greenKey.SetActive(true);
		}

		if (keyColor == "blue")
		{
			blueKey.SetActive(true);
		}
	}

	//this method clears the keys
	public void ClearKeys()
	{
		redKey.SetActive(false);
		greenKey.SetActive(false);
		blueKey.SetActive(false);
	}
		
	//methods UI hurt player
	public void DamageImgEnabled()
	{
		takeDamageImg.enabled = true;
	}

	public void DamageImgDisabled()
	{
		takeDamageImg.enabled = false;
	}

	//pause methods
	public void ShowPausePnl(bool isPaused)
	{ //pause and unpause pressing escape button
		if (isPaused)
		{
			menuPausePnl.SetActive(true);
		}
		else
		{
			menuPausePnl.SetActive(false);

		}
			
	}
		
}
