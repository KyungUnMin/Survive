using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHandler : MonoBehaviour
{
    /*
     * 이 코드는 MonoBehaviour가 상속되지 않은 클래스에서 코루틴을 쓸때 사용함
     * 내가 직접 만들거나 배운건 아니라 나도 동작원리는 모르겠음,,
     * 근데 사용법만 알고있음 나중에 말씀하시면 사용법 예시 보여드릴께요
     */

    IEnumerator enumerator = null;

    private void Coroutine(IEnumerator coro)
    {
        enumerator = coro;
        StartCoroutine(coro);
    }

    void Update()
    {
        if (enumerator != null)
        {
            if (enumerator.Current == null)
                Destroy(gameObject);
        }
    }

    public void Stop()
    {
        StopCoroutine(enumerator.ToString());
        Destroy(gameObject);
    }

    public static CoroutineHandler Start_Coroutine(IEnumerator coro)
    {
        GameObject obj = new GameObject("CoroutineHandler");
        CoroutineHandler handler = obj.AddComponent<CoroutineHandler>();
        if (handler)
            handler.Coroutine(coro);
        return handler;
    }
}
