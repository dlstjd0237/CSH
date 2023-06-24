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
        _tmpText.ForceMeshUpdate(); //텍스트를 고치고 나면 메시정보도같이 고쳐줘야 제대로 텍스트 보이는데
                                    //일단 그걸 강제로 빨리하라고 하는거, 원래 셋텍스트 다음 프레임으로 이루어ㅓ진다.
        TMP_TextInfo textInfo = _tmpText.textInfo;
        //텍스트 정보들이 여기 들어온다.

        Vector3[] vertices = textInfo.meshInfo[0].vertices;//정점들을 미리 배열로 가져온다
        for (int i = 0; i < textInfo.characterCount; i++)
        {
            TMP_CharacterInfo charInfo = textInfo.characterInfo[i];
            if (charInfo.isVisible == false) continue;

            int vIndex = charInfo.vertexIndex;
            float firstX = vertices[vIndex].x;
            for (int j = 1; j < 3; j++)
            {
                Vector3 origin = vertices[vIndex + j]; //이 캐릭터의 정점이 나온다. //sin -1~1까지의 함수
                float y = (Mathf.Sin(Time.time * _textSpeed + firstX)+1)*0.5f;
                vertices[vIndex + j] = origin + new Vector3(0, y, 0);
            }
        }
        _tmpText.UpdateVertexData();

    }

}
