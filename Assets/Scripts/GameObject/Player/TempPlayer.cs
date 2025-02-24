using UnityEngine;

public class TempPlayer : MonoBehaviour
{
    public static TempPlayer instance { get; private set; }

    public TempStatus Stat;

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
        // Å×½ºÆ®
        //Stat.currentHP -= Time.deltaTime;
    }
}
