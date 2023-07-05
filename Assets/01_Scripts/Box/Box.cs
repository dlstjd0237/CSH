using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EnemyLvel
{
    Lvel1 = 1,
    Lvel2,
    Lvel3,
    Lvel4,
    Lvel5

}
public class Box : MonoBehaviour
{
    [SerializeField] private EnemyLvel _enemyLvel;
    [SerializeField] private DebriEffect _debriPrefab;

    public void DestroyBox(Vector2 dir, float force)
    {
        DebriEffect effect = PoolManager.Instance.Pop("DebrlEffect") as DebriEffect;
        effect.transform.position = transform.position;
        effect.Explosion(dir, force);
        StateManager.Instance.AddCoin(100);
        if (Random.Range(0, 101) <= 100)
        {

            switch (_enemyLvel)
            {
                case EnemyLvel.Lvel1:
                    if (GameManager.Instance.Lvel2) break;
                    GameManager.Instance.SetLvel(2);
                    GameManager.Instance.Lvel2 = true; break;
                case EnemyLvel.Lvel2:
                    if (GameManager.Instance.Lvel3) break;
                    GameManager.Instance.SetLvel(3);
                    GameManager.Instance.Lvel3 = true; break;
                case EnemyLvel.Lvel3:
                    GameManager.Instance.Lvel4 = true; break;
                case EnemyLvel.Lvel4:
                    GameManager.Instance.Lvel5 = true; break;
            }
        }
        gameObject.SetActive(false); //자기ㅇ는 없게고 파티클 이펙트 만들어주기
    }




    //private void DestroyBox()
    //{
    //    Texture2D originalTex = _spriteRenderer.sprite.texture;
    //    //스프라이트 렌더러의 텍스쳐가 가져진다.
    //    float sWidth = _spriteRenderer.sprite.rect.width; //32 rect 안의 width 텍스처에서 잘라온 공간
    //                                                      //float sHeight = _spriteRenderer.sprite.rect.height; 
    //                                                      //Debug.Log(_spritRanderer.sprite.tect) 택스트 상에서 어디를 잘라 왔는가

    //    float boundWidth = _spriteRenderer.sprite.bounds.size.x; //1    

    //    float ppu = sWidth / boundWidth;

    //    int debriSize = 12;

    //    //Rect smallRect = new Rect(Random.Range(0, 25), Random.Range(0, 25), debriSize, debriSize);

    //    //Sprite s = Sprite.Create(originalTex, smallRect, Vector2.one * 0.5f, ppu);


    //    for (int k = 0; k < 30; k++)
    //    {

    //    float x = Random.Range(0, sWidth - debriSize);
    //    float y = Random.Range(0, sWidth - debriSize);
    //    Rect smallRect = new Rect(x, y, debriSize, debriSize);

    //    Texture2D newTex = new Texture2D(debriSize, debriSize);

    //    for (int i = 0; i < debriSize; i++)
    //    {
    //        for (int j = 0; j < debriSize; j++)
    //        {
    //            Color c = originalTex.GetPixel((int)x + j, (int)y + i);
    //            if (i < j)
    //                c.a = 0;
    //            newTex.SetPixel(j, i, c);
    //        }
    //    }
    //    newTex.filterMode = FilterMode.Point;
    //    newTex.Apply(); //내부 바이트 정보들을 실제 텍스쳐로 반영하는 작업

    //    Sprite s = Sprite.Create(newTex, new Rect(0,0,debriSize,debriSize), Vector2.one * 0.5f, ppu);

    //    GameObject obj = new GameObject();
    //    obj.AddComponent<SpriteRenderer>().sprite = s;
    //    Rigidbody2D rigid = obj.AddComponent<Rigidbody2D>();
    //    obj.AddComponent<PolygonCollider2D>();

    //    obj.transform.position = transform.position;
    //    Vector2 dir = Random.insideUnitCircle.normalized;
    //    rigid.AddForce(dir * 0.5f, ForceMode2D.Impulse);
    //    }




    //    Destroy(gameObject);
    //}
}
