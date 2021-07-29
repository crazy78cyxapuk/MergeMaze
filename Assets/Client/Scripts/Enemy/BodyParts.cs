using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyParts : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private Rigidbody[] rb;

    [SerializeField] private float timer = 3f;

    private void Awake()
    {
        transform.parent = null;
    }

    private void OnEnable()
    {
        ForcePart();
    }

    private void ForcePart()
    {
        for (int i = 0; i < rb.Length; i++)
        {
            rb[i].AddForce((transform.up + -transform.forward) * force, ForceMode.Impulse);
        }

        StartCoroutine(TimerDisable());
    }

    IEnumerator TimerDisable()
    {
        yield return new WaitForSeconds(timer);

        gameObject.SetActive(false);
    }
}
