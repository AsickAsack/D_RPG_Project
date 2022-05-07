using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBackGround : MonoBehaviour
{
    public TMPro.TMP_Text Loading_Text;
    int a;

    private void Awake()
    {
        a = Random.Range(0,4);

        this.transform.GetChild(a).GetComponent<UnityEngine.UI.Image>().enabled = true;

        switch(a)
        {
            case 0:
                Loading_Text.text = "사막의 왕은 '토레도' 입니다.";
                break;
            case 1:
                Loading_Text.text = "숲의 수호자 '나즈리엘'은 상대를 기절로 만드는데 특화된 부관입니다.";
                break;
            case 2:
                Loading_Text.text = "부관승급으로 부관의 능력치를 대폭 상승시킬 수 있습니다.";
                break;
            case 3:
                Loading_Text.text = "'키메라'는 잡아채는 순간 기절 상태 이상에 취약해집니다.";
                break;
        }


    }

}
