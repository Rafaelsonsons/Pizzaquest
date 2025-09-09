using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI maxHP;
	public TextMeshProUGUI currentHP;
	public Slider hpSlider;

	public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		hpSlider.maxValue = unit.maxHP;
		hpSlider.value = unit.currentHP;
	}

	public void SetHP(int hp)
	{
		hpSlider.value = hp;
	}

}
