![Typing SVG](https://readme-typing-svg.demolab.com?font=Fira+Code&size=30&pause=1000&width=435&lines=Space+Hero+Game)

# Description
- **프로젝트 소개** <br>
내일 배움 캠프에서 진행한 팀 프로젝트로 5인으로 개발한, 궁수의 전설 모작 게임입니다 <br>
- **개발기간**: 2025.02.21 ~ 2025.02.28
- **사용기술** <br>
-언어: C#<br>
-엔진: Unity2D<br>
-개발환경: Windows11, GitHub<br>

-**팀 소개**<br>
-팀장 임성균: 일반몬스터 AI, 보스몬스터 AI와 스킬<br>
-팀원 최재혁: 플레이어 이동, 공격, 스킬, 게임 사운드<br>
-팀원 신찬영: 전반적인 게임 UI, 룰렛 애니메이션, 스킬 가챠 시스템<br>
-팀원 한만진: 타일 맵 생성, 맵 장애물 랜덤 배치 구현<br>
-팀원 김준수: 아이템생성 구현, 프로젝트 컨셉 구성<br>
<br><br>
---
## 목차
- 기획 의도
- 발생 및 해결한 주요 문제점
- 핵심 기능
<br>

---
## 기획 의도
- 유니티 & Git을 사용한 협업 경험
- Entity Component System 아키텍처를 사용하여 새로운 방식의 구현 해보기 
<br>

---

## 발생 및 해결한 주요 문제점
### Entity Component System 아키텍처를 사용한 플레이어, 몬스터의 설계
- 플레이어와 몬스터가 기능을 공유하는 기능이 많아서, 둘의 로직을 최대한 공유하면서 다른 구현을 하는 것이 어려웠습니다.<br>
필요한 부분에 상속을 이용하여 플레이어와 몬스터가 필요한 경우 다른 로직을 수행하게 하였습니다.<br>
### 맵의 장애물 배치
- 벽으로 쓰는 타일을 만약 맵에 포함한다면, 벽으로 쓰는 타일을 장애물로 만들 때, 기존의 벽에 덮어씌워질 수 있습니다 <br>
  맵을 만들 때 바닥타일만 깔고, 벽타일을 포함하지 않으며, 장애물을 설치할 때 벽으로 쓰는 타일을 이용하여 가장자리에 경계를 먼저 설치한 다음 내부에 장애물을 설치합니다. 또한, 벽 타일 중에서 계단은 제외하였습니다 <br>
### Scriptable 객체를 사용하는데 데이터를 읽지 못하는 문
- Getcompoent<TempGameObjectUI>();로 데이터를 읽어주려 했으나 실패. Scriptable 에셋은 그게 안됩니다(아마도 에디터에서 메모리 주소를 참조하고 있기때문에?)<br>
TempStatus를 가지고 있는 Player(GameObject 객체) 클래스의 Start 부분에서 GameObjectUI에 Stat을 주입함으로써 해결했습니다. <br>
이 때 GameObjectUI의 TempStatus stat의 보안수준이 public이라 에디터에서 볼 수 있었는데, internal로 에디터에서 안보이게끔 처리했습니다 <br>
### 플레이어 파괴 현상
- 아이템이 파괴되지 않고 플레이어가 파괴되는 현상이 발생했습니다. <br>
OnTriggerEnter2D에 들어오는 Collider2D의 변수가 player였고, 삭제되는 오브젝트를 다시 점검하여 아이템이 제대로 삭제되게 하였습니다.<Br> 
### 몬스터의 원거리 공격시 투사체가 나오지 않았던 문제
- 몬스터의 투사체가 나오지 않는 문제가 있었습니다. <br>
처음에는 애니메이션의 문제라 생각했지만, 실제로 몬스터 투사체를 만드는 부분에서, 프리팹을 등록하지 않아서 null이 나오는 문제였습니다. 프리팹을 추가한 후, 생성한 투사체가 정상적으로 타겟으로 이동합니다.<br>
- 하지만, 보스몬스터의 투사체는 타겟을 향해 이동하지 않는 문제가 있습니다. <br>
이 문제는 아직 해결중에 있습니다.<br>
### 보스몬스터의 체력변화에 따른 공격로직의 변화
- 체력변화를 할 때 공격패턴이 변할 때 여러 오류가 발생하는 문제가 있습니다.<br>
로직이 변하는 것으로 봐서 패턴 자체가 변하는 것에는 문제가 없지만, 세부적인 이동에 문제가 있으며, 아직 해결중에 있습니다. <br>
- 보스몬스터의 특정 스킬을 발동할 때 main sprite의 자식으로 있는 sub sprite의 애니메이션이 재생하지 않는 문제가 있습니다.<br>
이 부분은 아직 해결중에 있습니다. <br>
- 보스몬스터의 스킬 쿨타임에 coroutine을 적용하고 있는데, 제대로 적용이 되지 않는 문제가 있습니다<br>
이 부분은 아직 해결중에 있습니다. <br>

 
<br><br>

---

## 핵심 기능
### 스킬 가챠 시스템
- 경험치를 채우면 스킬 중 랜덤 3개 중 하나를 선택할 수 있는 가챠 시스템이 발동됩니다. <br>
<div style="display: flex; justify-content: space-around;">
  <img src="https://github.com/Team33Ours/SpaceHero/blob/New_Dev/gacha.png" alt="Image" width="600">
</div>

### 스킬 강화 시스템
- 스킬에 의한 플레이어의 강화는 누적되어 나타납니다. <br>
<div style="display: flex; justify-content: space-around;">
  <img src="https://github.com/Team33Ours/SpaceHero/blob/New_Dev/before%20upgrade.png" alt="Image" width="600">
</div>
전<br><br>
<div style="display: flex; justify-content: space-around;">
  <img src="https://github.com/Team33Ours/SpaceHero/blob/New_Dev/after%20upgrade.png" alt="Image" width="600">
</div>
후<br><br>

### 업적 시스템
- 플레이어가 달성한 업적은 기록에 남아, 다음 실행때에도 유지됩니다. <br>
<div style="display: flex; justify-content: space-around;">
  <img src="https://github.com/Team33Ours/SpaceHero/blob/New_Dev/achivement%20system.png" alt="Image" width="600">
</div>

완성되는 기능은 추가 예정입니다.<br>
감사합니다.<br>





