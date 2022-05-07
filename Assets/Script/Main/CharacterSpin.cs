using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterSpin : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public float ChRotSpeed = 100.0f;
    public bool Is_Character_DragON = false;
    Animator myAnim;

    public void OnDrag(PointerEventData eventData)
    {
        myAnim = GetComponentInParent<Animator>();
        if (!myAnim.GetBool("IsEMO")) { 
            Is_Character_DragON = true;
        float x = eventData.delta.x * Time.deltaTime * ChRotSpeed;
        transform.Rotate(0, -x, 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Is_Character_DragON = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        myAnim = GetComponentInParent<Animator>();
        if (!myAnim.GetBool("IsEMO")&&!Is_Character_DragON)
        myAnim.SetTrigger("EMO");
    }
}