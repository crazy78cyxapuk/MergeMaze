using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private float speed;

    private float _timer = 3f;

    private void OnEnable()
    {
        GetComponent<Collider>().enabled = false;

        StartCoroutine(DisableTrap());
    }

    private void Update()
    {
        transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
    }

    IEnumerator DisableTrap()
    {
        yield return new WaitForSeconds(_timer);

        gameObject.SetActive(false);
    }
}
