using UnityEngine;
using UnityEngine.UI;

public class EnemyLife : EnemyBase
{
    [SerializeField] private float currentLife = 1;
    [SerializeField] private Image lifeBar;
    float maxLife;

    private void Awake() 
    {
        maxLife = currentLife;
    }

    private void UpdateLifeBar()
    {
        //Vida atual / pela vida maxima
        lifeBar.fillAmount = currentLife / maxLife;

        if(currentLife <= 0)
            lifeBar.GetComponentInParent<Canvas>().gameObject.SetActive(false);
    }

    public void TakeDamage()
    {
        currentLife -= 1;
        UpdateLifeBar();

        if (currentLife <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            anim.Play("Death");
            MoveManager.instance.RemoveMovableEntityToList(gameObject.GetComponent<MoveBase>());
            CoreAndFragmentsManager.instance.GainFragments();
            FloatingTextManager.Instance.SpawnFloatingText(transform, null, 1, CurrencyType.fragments, "F0");
            Destroy(gameObject, 0.5f);
        }
    }
}
