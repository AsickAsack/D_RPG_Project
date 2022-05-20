using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class POnclickbtn : MonoBehaviour
{
    public TMPro.TMP_Text error;
    public Image errorimg;
    [SerializeField] bool isPlaying = false;
 
 
   public void Onclick()
    {
        if(GameData.Instance.playerdata.Key>0 && GameData.Instance.playerdata.Key !=1 && isPlaying == false)
        {
            SceneManager.LoadScene(4);
        }
        else if(isPlaying==false)
        {
            Debug.Log("열쇠가부족합니다");
           StartCoroutine(Fadein()); 
         
        }
        
    }
    IEnumerator Fadein()
    {
        isPlaying = true;
        Color color = errorimg.color;
        Color textcolor = error.color;


        while (color.a < 1.0f)
        {
            color.a += 0.05f;
            textcolor.a += 0.05f;
            errorimg.color = color;
            error.color = textcolor;
            yield return null;
        }
        

       
        while (color.a >= 0f)
        {
            color.a -= 0.05f;
            textcolor.a -= 0.05f;
            errorimg.color = color;
            error.color = textcolor;
            yield return null;
        }

        isPlaying = false;



    }
   
}
