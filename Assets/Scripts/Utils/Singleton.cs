using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            // DontDestroyOnLoad는 루트 오브젝트에만 사용가능하므로 
            // 루트 오브젝트가 아니면 최상위로 이동
            if (transform.parent != null)
            {
                transform.parent = null;
            }

            DontDestroyOnLoad(gameObject);   // 씬 변경 시에도 유지
            //SceneManager.sceneLoaded += OnSceneLoaded;  // 씬 변경 감지 추가
        }
        // 이미 싱글턴 인스턴스가 존재하면, 현재 객체를 삭제
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    ///// <summary>
    ///// 씬이 로드될 때 실행되는 메서드 (각 매니저에서 오버라이드)
    ///// </summary>
    ///// <param name="scene"></param>
    ///// <param name="mode"></param>
    //protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    // 각각 클래스에서 정의해서 사용한다
    //}
    //protected virtual void OnDestroy()
    //{
    //    SceneManager.sceneLoaded -= OnSceneLoaded;
    //}
}
