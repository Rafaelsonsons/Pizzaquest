using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

	public GameObject playerPrefab;
	public GameObject enemyPrefab;

	public Transform playerBattleStation;
	public Transform enemyBattleStation;

	Unit playerUnit;
	Unit enemyUnit;

	public Text dialogueText;

	public BattleHUD playerHUD;
	public BattleHUD enemyHUD;

	public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
		state = BattleState.START;
		StartCoroutine(SetupBattle());
    }

	IEnumerator SetupBattle()
	{
    Debug.Log("=== Iniciando SetupBattle() ===");

    // Instanciar Player
    GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
    playerUnit = playerGO.GetComponent<Unit>();
    if (playerUnit == null)
    {
        Debug.LogError("❌ O prefab do Player não tem o componente 'Unit' no objeto raiz!");
        yield break;
    }
    else
    {
        Debug.Log("✅ Player instanciado: " + playerUnit.unitName);
    }

    // Instanciar Inimigo
    GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
    enemyUnit = enemyGO.GetComponent<Unit>();
    if (enemyUnit == null)
    {
        Debug.LogError("❌ O prefab do Enemy não tem o componente 'Unit' no objeto raiz!");
        yield break;
    }
    else
    {
        Debug.Log("✅ Enemy instanciado: " + enemyUnit.unitName);
    }

    // Verificar DialogueText
    if (dialogueText == null)
    {
        Debug.LogError("❌ dialogueText não foi atribuído no Inspector!");
        yield break;
    }
    dialogueText.text = "A wild " + enemyUnit.unitName + " approaches...";
    Debug.Log("✅ dialogueText configurado");

    // Verificar PlayerHUD
    if (playerHUD == null)
    {
        Debug.LogError("❌ playerHUD não foi atribuído no Inspector!");
        yield break;
    }
    playerHUD.SetHUD(playerUnit);
    Debug.Log("✅ playerHUD configurado");

    // Verificar EnemyHUD
    if (enemyHUD == null)
    {
        Debug.LogError("❌ enemyHUD não foi atribuído no Inspector!");
        yield break;
    }
    enemyHUD.SetHUD(enemyUnit);
    Debug.Log("✅ enemyHUD configurado");

    // Esperar 2 segundos
    yield return new WaitForSeconds(2f);
    Debug.Log("⏳ Espera de 2 segundos concluída");

    // Transição de estado
    state = BattleState.PLAYERTURN;
    Debug.Log("➡️ Estado alterado para: " + state);

    PlayerTurn();
	}


	IEnumerator PlayerAttack()
	{
		bool isDead = enemyUnit.TakeDamage(playerUnit.damage);

		enemyHUD.SetHP(enemyUnit.currentHP);
		dialogueText.text = "The attack is successful!";

		yield return new WaitForSeconds(2f);

		if(isDead)
		{
			state = BattleState.WON;
			EndBattle();
		} else
		{
			state = BattleState.ENEMYTURN;
			StartCoroutine(EnemyTurn());
		}
	}

	IEnumerator EnemyTurn()
	{
		dialogueText.text = enemyUnit.unitName + " attacks!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		playerHUD.SetHP(playerUnit.currentHP);

		yield return new WaitForSeconds(1f);

		if(isDead)
		{
			state = BattleState.LOST;
			EndBattle();
		} else
		{
			state = BattleState.PLAYERTURN;
			PlayerTurn();
		}

	}

	void EndBattle()
	{
		if(state == BattleState.WON)
		{
			dialogueText.text = "You won the battle!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "You were defeated.";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Choose an action:";
	}

	IEnumerator PlayerHeal()
	{
		playerUnit.Heal(5);

		playerHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "You feel renewed strength!";

		yield return new WaitForSeconds(2f);

		state = BattleState.ENEMYTURN;
		StartCoroutine(EnemyTurn());
	}

	public void OnAttackButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerAttack());
	}

	public void OnHealButton()
	{
		if (state != BattleState.PLAYERTURN)
			return;

		StartCoroutine(PlayerHeal());
	}

}