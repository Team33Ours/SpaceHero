using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 체력에 따른 phase를 변경
/// 2025.02.26.ImSeonggyun
/// </summary>
public class BossMonsterResourceController : ResourceController
{
    SkillManager skillManager;
    BossMonsterController bossController;

    private void Start()
    {
        skillManager = SkillManager.Instance;
        bossController = GetComponent<BossMonsterController>();
    }

    public override bool ChangeHealth(float change)
    {
        // hp변화는 그대로 ResourceController의 로직을 따른다
        bool isHpChanged = base.ChangeHealth(change);
        if (isHpChanged)
            CheckHp();
        return isHpChanged;
    }

    public void CheckHp()
    {
        // Hp 변화 확인 후 
        // phase변화를 확인한다
        if (CurrentHealth >= 0.7 * MaxHealth)
        {
            // phase1
            bossController.phase = eBossPhase.Phase_1;
        }
        else if (CurrentHealth >= 0.4 * MaxHealth)
        {
            // phase2
            bossController.phase = eBossPhase.Phase_2;
        }
        else
        {
            // phase3
            bossController.phase = eBossPhase.Phase_3;
        }
    }
    // phase가 바뀌면 스킬을 바꾸는 것은 BossMonsterController에서 


}
