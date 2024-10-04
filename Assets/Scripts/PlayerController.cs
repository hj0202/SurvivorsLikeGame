using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour, InputControls.IPlayerActions
{
    [SerializeField] private Transform camera;
    [SerializeField] private LayerMask layer;
    //[SerializeField] private Transform target;

    [SerializeField] private float maxHp;
    [SerializeField] private float walkSpeed;
    [SerializeField] private float range;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackPower;

    private InputControls input;
    private Animator animator;
    private Rigidbody rigidbody;
    private IEnumerator move;
    private float hp;
    private bool isDamaged;

    [SerializeField] private GameObject attack;

    private void Awake()
    {
        input = new InputControls();
        input.Player.SetCallbacks(this);

        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        
        Init();
    }

    private void Start()
    {
        StartCoroutine(Attack());
    }

    private void Update()
    {
        camera.position = transform.position + new Vector3(0, 20, -8);
    }

    public void Init()
    {
        hp = maxHp;
        isDamaged = false;
        transform.position = Vector3.up;
        transform.rotation = Quaternion.identity;
        animator.SetBool("Walk", false);
        UIManager.Instance.UpdateHpBar(hp, maxHp);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackSpeed);

            Vector3 target = FindTarget();
            if (target == Vector3.zero)
                continue;

            GameObject attackPrefab = GameManager.Resource.GetPrefab("Attack");
            if (attackPrefab != null)
            {
                Vector3 position = transform.position;
                Quaternion rotation = Quaternion.identity;
                GameObject obj = Instantiate(attackPrefab, position, rotation);
                obj.GetComponent<AutoAttack>().SetTarget(target, position, attackPower);
            }
            else
            {
                Debug.LogError("no Attack prefab");
            }

            // GameManager -> PoolManager -> GetAttack

            // GameObject attackObj = GetAttack();
            // position 설정
            // AutoAttack가져와서
            // SetTarget.
        }
    }

    private Vector3 FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range, layer);

        if (colliders.Length == 0)
            return Vector3.zero;
        Vector3 targetPosition = Vector3.zero;
        Vector3 myPosition = transform.position;
        float minDistance = range * range;
        for (int i = 0; i < colliders.Length; i++)
        {
            // Enemy가 죽은 상태면 무시하기
            EnemyController enemy = colliders[i].gameObject.GetComponent<EnemyController>();
            if (enemy.IsDie())
                continue;

            Vector3 colliderPosition = colliders[i].transform.position;

            if (enemy.IsBoss())
                return colliderPosition;

            // 거리 구하기
            Vector3 heading = colliderPosition - myPosition;
            float distance = heading.sqrMagnitude;

            // 최소 거리 갱신
            if (distance < minDistance)
            {
                
                minDistance = distance;
                targetPosition = colliderPosition;
            }    
        }

        // 최소 거리 위치 반환
        Debug.Log("target:" + targetPosition);
        return targetPosition;
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        
        if (context.started)
        {
            animator.SetBool("Walk", true);
        }
        else if (context.performed)
        {
            Vector2 dir = context.ReadValue<Vector2>();
            Debug.Log(Vector2.Angle(dir, Vector2.up));
            if (move != null)
                StopCoroutine(move);
            move = Move(dir);
            StartCoroutine(move);

        }
        else if (context.canceled)
        {
            if (move != null)
                StopCoroutine(move);
            
            animator.SetBool("Walk", false);
        }
    }

    IEnumerator Move(Vector2 dir)
    {
        transform.rotation = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.y));
        

        while (true)
        {
            yield return new WaitForFixedUpdate();

            float x = dir.x * Time.fixedDeltaTime * walkSpeed;
            float y = dir.y * Time.fixedDeltaTime * walkSpeed;
            Vector3 translation = new Vector3(x, 0, y);
            transform.Translate(translation, Space.World);

            
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isDamaged)
            return;

        if (collision.collider.CompareTag("Enemy"))
        {
            StartCoroutine(Damage());
        }
    }

    IEnumerator Damage()
    {
        Debug.Log("minus hp");
        hp -= 10;
        if (hp <= 0)
        {
            hp = 0;
            range = 0;
            GameManager.Instance.EndGame();
        }
        UIManager.Instance.UpdateHpBar(hp, maxHp);
        isDamaged = true;
        yield return new WaitForSeconds(1f);
        isDamaged = false;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    #region Input

    public void EnableInput()
    {
        input.Player.Enable();
    }

    public void DisableInput()
    {
        input.Player.Disable();
    }
    #endregion
}
