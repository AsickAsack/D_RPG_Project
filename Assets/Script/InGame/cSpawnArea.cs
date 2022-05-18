using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSpawnArea : MonoBehaviour
{
    public Transform playerPos;
    public List<cMonsterSpawnArea> spawnAreaList = new List<cMonsterSpawnArea>();
    public GameObject arrow; // 방향 UI
    public Transform nextArea; // 다음지역
    public int curAreaNum = 0; // 현재 지역의 번호

    private void Start()
    {
        NextArea(curAreaNum);
        ArrowSetting();        
    }

    public void ActiveArrow()
    {
        arrow.gameObject.SetActive(true); // 방향 표시 켬
    }

    public void DeActiveArrow()
    {
        arrow.gameObject.SetActive(false); // 방향 표시 끔
    }

    public void NextArea(int index)
    {
        if (index > 0)
        {
            spawnAreaList[index - 1].gameObject.SetActive(false); // 이전 지역을 꺼줌
        }

        nextArea = spawnAreaList[index].transform;
        nextArea.gameObject.SetActive(true);
        curAreaNum++;
        ActiveArrow();
    }

    void ArrowSetting()
    {
        // 회전
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            arrow.transform.position = playerPos.position; // 중심 = 플레이어 위치

            Vector3 dir = (nextArea.position - playerPos.position).normalized;

            // 플레이어와 다음지역 사이의 각도 계산
            cCharacter.CalculateAngle(arrow.transform.forward, dir, arrow.transform.right, out ROTDATA myRotData);

            while (!Mathf.Approximately(myRotData.angle, 0.0f))
            {
                float delta = 180.0f * Time.deltaTime;

                delta = delta > myRotData.angle ? myRotData.angle : delta;

                arrow.transform.Rotate(Vector3.up * delta * myRotData.rotDir);
                myRotData.angle -= delta;

                yield return null;
            }

            yield return null;
        }
    }
}
