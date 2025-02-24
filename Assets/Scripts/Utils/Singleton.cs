using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �Ŵ����� �̱������� �����ϱ� ���� ���׸� Ŭ����
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
                // �ش� ������Ʈ�� ������ �ִ� ���� ������Ʈ�� ã�Ƽ� ����
                instance = (T)FindAnyObjectByType(typeof(T));

                // �ν��Ͻ��� ã�� ���� ���
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
        // �̱��� �ν��Ͻ��� ���ٸ�
        if (instance == null)
        {
            instance = this as T;            // ���� ��ü�� �̱��� �ν��Ͻ��� ����
            DontDestroyOnLoad(gameObject);   // �� ���� �ÿ��� ����
        }
        // �̹� �̱��� �ν��Ͻ��� �����ϸ�, ���� ��ü�� ����
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
