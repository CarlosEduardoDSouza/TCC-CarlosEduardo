using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(EnemyLife))]
public class EnemyTakeDamage : EnemyBase
{
    [Header("Vfx Options")]
    public List<GameObject> vfxs = new List<GameObject>();
    [SerializeField] private float vfxDuration = 0.333f;
    [SerializeField] private Vector2 vfxMovement;
    EnemyLife lifeScript;

    private void Awake() 
    {
        lifeScript = GetComponent<EnemyLife>();
    }

    public void TakeDamage()
    {
        CreateVFXIfPossible();
        PlayDamageAnimation();
        lifeScript.TakeDamage();

        if (PlayerCrit.Instance.RoolCritHit())
        {
            FloatingTextManager.Instance.SpawnFloatingText(transform, "CRIT");
        }
    }

    private void PlayDamageAnimation()
    {
        anim.Play("Hit");
    }

    private void CreateVFXIfPossible()
    {
        for (int i = 0; i < vfxs.Count; i++)
        {
            //Cria o obj de efeito no jogo
            GameObject vfx = Instantiate(vfxs[i], transform);

            //Calcula o movimento que a animação vai ter
            Vector3 animMovement = new Vector3(
                Random.Range(vfxMovement.x / 3, vfxMovement.x),
                Random.Range(vfxMovement.y / 3, vfxMovement.y),
                0) + transform.position;

            //Faz a animação efetivamente, e ao terminar, destroi o efeito
            vfx.transform.DOMove(animMovement, vfxDuration).OnComplete(() =>
            {
                Destroy(vfx);
            });

            //Faz a animação para alterar a escala do efeito
            vfx.transform.DOScale(new Vector3(0, 0, 0), vfxDuration).SetEase(Ease.InExpo);
        }
    }
}
