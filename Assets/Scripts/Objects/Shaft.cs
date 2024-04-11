using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.Math;

public class Shaft : MonoBehaviour//���̵�ĳһ����
{
    int weight;
    public List<Bead> beads;
    public Vector2 preMousePos;
    
    Vector2 originMousePos;
    public List<Bead> movingBeads = new List<Bead>();
    public static float moveThreshold = 0.3f;
    public  float maxMoveDistance = 1.4f;
    
    public void Move(int index, Vector2 newMousePos)
    {
        float deltaY = newMousePos.y - preMousePos.y;
        bool canMoveDown = !beads[index].isDown;
        bool toMax = false;

        //��һ���ƶ�,ȷ����Ҫ�ƶ������飬���ӵ��б�
        if (movingBeads.Count == 0)
        {
            if (deltaY == 0)
            {

            }
            else if (deltaY < 0)//�����ƶ�
            {
                for (int i = index; i < beads.Count; i++)
                {
                    if (!beads[i].isDown)
                    {
                        movingBeads.Add(beads[i]);
                    }
                }
            }
            else
            {
                for (int i = index; i >= 0; i--)
                {
                    if (beads[i].isDown)
                    {
                        movingBeads.Add(beads[i]);
                    }
                }
            }

        }

        float allDistance = newMousePos.y - originMousePos.y;

        toMax = Abs(allDistance) > maxMoveDistance;//������ƶ����볬�����ޣ���Ǵ˴��ƶ��󽫻ᳬ��


        if ((canMoveDown && allDistance <= 0) || (!canMoveDown && allDistance >= 0))
        {
            //
        }
        else//��Ч�ƶ�
        {
            deltaY = 0;
        }

        foreach (Bead bead in movingBeads)
        {
            Vector2 nextPos = bead.transform.position + new Vector3(0, deltaY, 0);
            if (toMax)
            {
                bead.MoveToSlot();
            }
            else
            {
                bead.MoveTo(nextPos);
            }
            
        }

        preMousePos = newMousePos;
    }

    public void RecordMouse()
    {
        preMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        originMousePos = preMousePos;
    }

    public void Judge(int index, Vector2 endPos)
    {
        float deltaY = endPos.y - originMousePos.y;
        Debug.Log(deltaY);
        
        if (deltaY < moveThreshold && deltaY > -moveThreshold)//���ƶ�
        {
            ResetPos();
        }
        else
        {
            foreach(var bead in movingBeads)
            {
                bead.ChangeSlot(this.maxMoveDistance);
                bead.MoveToSlot();
            }
        }

        movingBeads.Clear();
    }

    private void ResetPos()
    {
        foreach (var bead in movingBeads)
        {
            bead.MoveToSlot();
        }
    }
}
