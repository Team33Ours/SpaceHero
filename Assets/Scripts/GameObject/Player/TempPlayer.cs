using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    public static TempPlayer instance { get; private set; }

    public TempPlayerStatus Stat;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        Stat.currentHP -= Time.deltaTime;
    }

    public TempPlayer GetInstance()
    {
        return instance;
    }
}
