using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Components
    Animator animator;
    NavMeshAgent meshAgent;
    EnemyFactory enemyFactory;

    // Balance
    [SerializeField] private bool isBoss = false;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float maxHp = 10f;

    // etc
    GameObject target;
    private bool isDie;
    private float hp;
    IEnumerator bossWalk;
    
    public void Init(GameObject _target, EnemyFactory _enemyFactory)
    {
        target = _target;
        enemyFactory = _enemyFactory;
        
        hp = maxHp;
        isDie = false;
        meshAgent.speed = speed;

        if (isBoss)
        {
            UIManager.Instance.UpdateBossHpBar(hp, maxHp);
            bossWalk = BossWalk();
            StartCoroutine(bossWalk);
        }
    }

    private void Awake()
    {
        meshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (target)
            meshAgent.SetDestination(target.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            Destroy(other.gameObject);
            hp -= 5;

            if (isBoss)
            {
                UIManager.Instance.UpdateBossHpBar(hp, maxHp);
            }

            if (hp <= 0)
            {
                Die();
            }
            
        }
    }

    public bool IsDie()
    {
        return isDie;
    }

    public bool IsBoss()
    {
        return isBoss;
    }

    public void Die()
    { 
        isDie = true;
        animator.SetTrigger("Die");
        
        if (isBoss)
        {
            StopCoroutine(bossWalk);
        }
        meshAgent.speed = 0f;
        StartCoroutine(DieCoroutine());
    }
    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(1f);
        if (!isBoss)
            enemyFactory.Die(gameObject);
    }

    IEnumerator BossWalk()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            if (meshAgent.speed == speed)
            {
                meshAgent.speed = speed + 6f;
            }
            else
            {
                meshAgent.speed = speed;
            }
        }
    }
}
