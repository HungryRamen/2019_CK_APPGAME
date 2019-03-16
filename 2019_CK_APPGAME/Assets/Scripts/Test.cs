using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class tt//Text Queu 구현용
{
    public char mCh;          //문자
    public string mColor;     //문자색깔
    public int mSize;        //문자크기

}

public class Test : MonoBehaviour
{
    private Queue<tt> testQueue;
    private List<Queue<tt>> testQueueList;
    // Start is called before the first frame update
    void Start()
    {
        testQueue = new Queue<tt>();
        testQueueList = new List<Queue<tt>>();
        tt t = new tt();
        t.mCh = 'd';
        t.mColor = "red";
        t.mSize = 10;
        testQueue.Enqueue(t);
        t.mCh = 'f';
        t.mColor = "cc";
        t.mSize = 50;
        testQueue.Enqueue(t);
        testQueueList.Add(testQueue);
        Queue<tt> test = new Queue<tt>(testQueueList[0]);
        tt t2 = test.Dequeue();
        Debug.Log(t2.mCh);
        Debug.Log(t2.mColor);
        Debug.Log(t2.mSize);
        Queue<tt> test2 = new Queue<tt>(testQueueList[0]);
        tt t3 = test2.Dequeue();
        Debug.Log(t3.mCh);
        Debug.Log(t3.mColor);
        Debug.Log(t3.mSize);
        Queue<tt> test3 = new Queue<tt>(testQueueList[0]);
        tt t4 = test3.Dequeue();
        Debug.Log(t4.mCh);
        Debug.Log(t4.mColor);
        Debug.Log(t4.mSize);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
