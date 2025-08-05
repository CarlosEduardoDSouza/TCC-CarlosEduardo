using UnityEngine;

[RequireComponent(typeof(Animator))]
public class ChestController : MonoBehaviour
{   
    private Animator _anim;
    [SerializeField] private float amount = 0;
    [SerializeField] private float minMultiplier = 0;
    [SerializeField] private float maxMultiplier = 0;

    private void Awake() 
    {
        _anim = GetComponent<Animator>();
    }

    private void Update() => KillX();

    private void OnTriggerEnter2D(Collider2D collider)   
    {  
        if(collider.CompareTag("Player"))
        {
            _anim.Play("Open");
            
            GainGold();

        }
    }

    private void GainGold()
    {
        float multiplier = Random.Range(minMultiplier, maxMultiplier);
        float coinAmount = amount * multiplier;
        CurrencyManager.Instance.GainCurrency(CurrencyType.gold, coinAmount);
        FloatingTextManager.Instance.SpawnFloatingText(transform, transform, coinAmount, CurrencyType.gold);
    }

    public void KillX()
    {
        if(transform.position.x <= -2f)
        {
            MoveManager.instance.RemoveMovableEntityToList(GetComponentInChildren<MoveBase>());
            Destroy(transform.parent.gameObject);
        }
        
    }

}
