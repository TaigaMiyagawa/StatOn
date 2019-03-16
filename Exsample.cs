using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exsample : MonoBehaviour
{
    public float axis_x = 0;
    public float axis_y = 0;
    public float axis_z = -1;
    //private Rigidbody rb; // Rididbody
    public float speed = 5;

    Vector3 rotatePoint = Vector3.zero; //回転の中心
    Vector3 rotateAxis = Vector3.zero; //回転軸
    float starAngle = 0f; //回転角度

    float starSizeHalf; //☆の大きさの半分
    bool isRotate = false; //回転中に立つフラッグ　入力切替

    public int ONOFF_count = 5;

    public int GetONOFF_count()
    {
        return ONOFF_count;
    }


    // Start is called before the first frame update
    void Start()
    {   
        starSizeHalf = transform.localScale.x / 2f;
        //rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //var moveHorizontal = Input.GetAxis("Horizontal");
        if (isRotate)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            rotatePoint = transform.position + new Vector3(0, -1, 0);
            //rotatePoint = new Vector3(moveHorizontal, 0f, 0f);
            rotateAxis = new Vector3(axis_x, axis_y, axis_z);
            //rb.AddForce(rotatePoint * speed);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            rotatePoint = transform.position + new Vector3(0, -1, -1);
            //rotatePoint = new Vector3(moveHorizontal, 0f, 0f);
            rotateAxis = new Vector3(0, 0, 1);
            //rb.AddForce(rotatePoint * speed);
        }

        if (rotatePoint == Vector3.zero)
        {
            return;
        }
        StartCoroutine(MoveStar());
    }

    IEnumerator MoveStar()
    {
        //回転中はフラグ立つ
        isRotate = true;

        //回転処理
        float sumAngle = 0f; //angleの合計を保存
        while (sumAngle < 72f)
        {
            starAngle = 5f; //ここを変えると回転速度が変わる
            sumAngle += starAngle;

            //72度以上回転しないように制限
            if (sumAngle > 72f)
            {
                starAngle -= sumAngle - 72f;
            }
            transform.RotateAround(rotatePoint, rotateAxis, starAngle);
            
            yield return null;
        }

        //回転中のフラッグをfalseに
        isRotate = false;
        rotatePoint = Vector3.zero;
        rotateAxis = Vector3.zero;
        yield break;
    }


}
