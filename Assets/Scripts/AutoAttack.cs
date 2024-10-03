using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttack : MonoBehaviour
{
    Rigidbody rigidbody;
    [SerializeField] private Transform target;
    [SerializeField] private Transform target2;

    IEnumerator dieCoroutine;
    private void Start()
    {
        dieCoroutine = DestroyAttack();
        StartCoroutine(dieCoroutine);
    }

    IEnumerator DestroyAttack()
    {
        yield return new WaitForSeconds(1f);
        Destroy(this);

    }

    public void SetTarget(Vector3 a, Vector3 b, float speed)
    {
        Vector3 targetPos = a - b;
        float distance = targetPos.magnitude;
        Vector3 direction = targetPos / distance;
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.AddForce(targetPos.normalized * speed);
        
    }

    
}
