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

	public BattleHUD piromanteHUD;
	public BattleHUD sovadorHUD;
	public BattleHUD faqueiroHUD;
	public  List<BattleHUD> enemyHUD;

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
    dialogueText.text = " Um " + enemyUnit.unitName + " se aproxima...";
    Debug.Log("✅ dialogueText configurado");

    // Verificar PlayerHUD
    if (piromanteHUD == null)
    {
        Debug.LogError("❌ playerHUD não foi atribuído no Inspector!");
        yield break;
    }
    piromanteHUD.SetHUD(playerUnit);
    Debug.Log("✅ playerHUD configurado");

    // Verificar EnemyHUD
    if (enemyHUD == null)
    {
        Debug.LogError("❌ enemyHUD não foi atribuído no Inspector!");
        yield break;
    }
    enemyHUD[0].SetHUD(enemyUnit);
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

		enemyHUD[0].SetHP(enemyUnit.currentHP);
		dialogueText.text = "O ataque acertou!";

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
		dialogueText.text = enemyUnit.unitName + " ataca!";

		yield return new WaitForSeconds(1f);

		bool isDead = playerUnit.TakeDamage(enemyUnit.damage);

		piromanteHUD.SetHP(playerUnit.currentHP);

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
			dialogueText.text = "VocÊ venceu!";
		} else if (state == BattleState.LOST)
		{
			dialogueText.text = "Você perdeu";
		}
	}

	void PlayerTurn()
	{
		dialogueText.text = "Escolha uma ação";
	}

	IEnumerator PlayerHeal()
	{
		playerUnit.Heal(5);

		piromanteHUD.SetHP(playerUnit.currentHP);
		dialogueText.text = "Você se sente renovado!";

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