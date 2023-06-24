using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class WobblyText : MonoBehaviour
{
    [SerializeField] private float _textSpeed = 10.0f;
    private TMP_Text _tmpText;

    private void Awake()
    {
        _tmpText = GetComponent<TMP_Text>();
    }

    private void Update()
    {
        _tmpText.ForceMeshUpdate(); //�ؽ�Ʈ�� ��ġ�� ���� �޽����������� ������� ����� �ؽ�Ʈ ���̴µ�
                                    //�ϴ� �װ� ������ �����϶�� �ϴ°�, ���� ���ؽ�Ʈ ���� ���������� �̷�������.
        TMP_TextInfo textInfo = _tmpText.textInfo;
        //�ؽ�Ʈ �������� ���� ���´�.

        Vector3[] vertices = textInfo.meshInfo[0].vertices;//�������� �̸� �迭�� �����´�
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (charInfo.isVisible == false) continue;

            int vIndex = charInfo.vertexIndex;
            float firstX = vertices[vIndex].x;
            for (int j = 1; j < 3; j++)
            {
                Vector3 origin = vertices[vIndex + j]; //�� ĳ������ ������ ���´�. //sin -1~1������ �Լ�
                float y = (Mathf.Sin(Time.time * _textSpeed + firstX)+1)*0.5f;
                vertices[vIndex + j] = origin + new Vector3(0, y, 0);
            }
        }
        _tmpText.UpdateVertexData();

    }

}
