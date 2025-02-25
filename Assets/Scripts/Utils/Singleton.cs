using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 매니저를 싱글턴으로 선언하기 위한 제네릭 클래스
/// 2025.02.24.ImSeonggyun
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                // 해당 컴포넌트를 가지고 있는 게임 오브젝트를 찾아서 리턴
                instance = (T)FindAnyObjectByType(typeof(T));

                // 인스턴스를 찾지 못한 경우
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(T).Name);
                    instance = go.AddComponent<T>();
                    DontDestroyOnLoad(go);
                }

            }
            return instance;
        }
    }
    protected virtual void Awake()
    {
        // 싱글턴 인스턴스가 없다면
        if (instance == null)
        {
            instance = this as T;            // 현재 객체를 싱글턴 인스턴스로 설정
            DontDestroyOnLoad(gameObject);   // 씬 변경 시에도 유지
        }
        // 이미 싱글턴 인스턴스가 존재하면, 현재 객체를 삭제
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
