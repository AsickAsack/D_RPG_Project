using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class cAnimEvent : MonoBehaviour
{
    public UnityAction Attack = null;

    public GameObject SlashEffect;

    public void OnAttack()
    {
        Attack?.Invoke();
    }

    public void OnSlash()
    {
        StartCoroutine(Slash());
    }

    IEnumerator Slash()
    {
        GameObject obj = Instantiate(SlashEffect, this.transform);
        yield return new WaitForSeconds(1.0f);
        Destroy(obj);
    }
}
