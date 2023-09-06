using UnityEngine;
using TMPro;
public class BattleSceneManager : MonoBehaviour
{
    public static BattleSceneManager Instance { private set; get; }

    [SerializeField] private BattleAnimationBG anim;
    //Player
    public CardObject Player;
    public ActionCardLogic P_ActionCard;
    public TextMeshProUGUI P_Name;
    public TextMeshProUGUI P_HealthText;
    public int P_Health;
    public enum PStatus {None,Attacking,Defending,Running };
    public PStatus PlayerStatus = PStatus.None;

    //Enemy
    public CardObject Enemy;
    public ActionCardLogic E_ActionCard;
    public TextMeshProUGUI E_Name;
    public TextMeshProUGUI E_HealthText;
    public int E_Health;
    public enum EStatus { None, Attacking, Defending, Running };
    public EStatus EnemyStatus = EStatus.None;


    public void Awake()
    {
        #region instance
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        #endregion
        
        SetVars();
    }

    public void SetVars()
    {

        Player = GameManager.Instance.PlayerActiveUnit.GetComponent<UnitInfo>().CardInfo;
        Enemy = GameManager.Instance.AIActiveUnit.GetComponent<UnitInfo>().CardInfo;

        #region Player
        P_Name.text = Player.CardName;
        P_Health = Player.HealthPoints;
        P_HealthText.text = P_Health.ToString();
        #endregion

        #region Enemy
        E_Name.text = Enemy.CardName;
        E_Health = Enemy.HealthPoints;
        E_HealthText.text = E_Health.ToString();
        #endregion

    }

    private void Update()
    {
        if (E_Health <= 0) 
            gameObject.SetActive(false);
    }
}
