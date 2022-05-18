using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cSpawnArea : MonoBehaviour
{
    public Transform playerPos;
    public List<cMonsterSpawnArea> spawnAreaList = new List<cMonsterSpawnArea>();
    public GameObject arrow; // ���� UI
    public Transform nextArea; // ��������
    public int curAreaNum = 0; // ���� ������ ��ȣ

    private void Start()
    {
        NextArea(curAreaNum);
        ArrowSetting();        
    }

    public void ActiveArrow()
    {
        arrow.gameObject.SetActive(true); // ���� ǥ�� ��
    }

    public void DeActiveArrow()
    {
        arrow.gameObject.SetActive(false); // ���� ǥ�� ��
    }

    public void NextArea(int index)
    {
        if (index > 0)
        {
            spawnAreaList[index - 1].gameObject.SetActive(false); // ���� ������ ����
        }

        nextArea = spawnAreaList[index].transform;
        nextArea.gameObject.SetActive(true);
        curAreaNum++;
        ActiveArrow();
    }

    void ArrowSetting()
    {
        // ȸ��
        StartCoroutine(Rotate());
    }

    IEnumerator Rotate()
    {
        while (true)
        {
            arrow.transform.position = playerPos.position; // �߽� = �÷��̾� ��ġ

            Vector3 dir = (nextArea.position - playerPos.position).normalized;

            // �÷��̾�� �������� ������ ���� ���
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
