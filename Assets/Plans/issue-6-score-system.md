# Project Overview
- Game Title: Suika Game (수박 게임)
- High-Level Concept: 물리 기반 과일 합성 퍼즐 게임.
- Players: 싱글 플레이어
- Target Platform: Standalone Windows
- Render Pipeline: UniversalRP (2D)
- UI System: UI Toolkit (Unity 6 권장 사양)

# Game Mechanics
## Core Gameplay Loop
- 과일 투하 → 합성 → 점수 획득 → 최고 점수 갱신 및 저장.
- 수박(11 티어) 합성 시 특별 보너스(81점) 지급 및 소멸.

# UI
- **Score HUD**:
  - 현재 점수: 상단 좌측 표시.
  - 최고 점수: 현재 점수 아래 표시.
- **기술**: UI Toolkit (.uxml, .uss) 사용.

# Key Assets & Context
- `Assets/Scripts/ScoreManager.cs`: 점수 관리 및 로컬 저장 (Singleton).
- `Assets/UI/ScoreHUD.uxml`: HUD 레이아웃 정의.
- `Assets/UI/ScoreHUD.uss`: HUD 스타일 정의.
- `Assets/Scripts/ScoreUI.cs`: UI Toolkit과 ScoreManager 연결.

# Implementation Steps
1. **ScoreManager 구현**:
   - `currentScore`, `bestScore` 변수 선언.
   - `AddScore(int amount)` 메서드: 점수 합산 및 UI 갱신 이벤트 호출.
   - `SaveBest()`, `LoadBest()`: `PlayerPrefs`를 이용한 최고 점수 저장/불러오기.
   - `Awake`에서 `LoadBest()` 호출 및 싱글턴 설정.
2. **FruitMerge 수정**:
   - `FruitMerge.OnFruitMerged` 이벤트를 통해 합성된 과일 데이터 전달 (기존 로직 활용).
   - `ScoreManager`에서 이 이벤트를 구독하여 점수 계산.
3. **UI Toolkit 에셋 생성**:
   - `Assets/UI/` 폴더 생성.
   - `ScoreHUD.uxml`: `Label` 두 개(CurrentScore, BestScore) 배치.
   - `ScoreHUD.uss`: 텍스트 크기, 색상, 위치 설정 (상단 좌측).
4. **ScoreUI 구현**:
   - `UIDocument` 참조 및 레이블 바인딩.
   - `ScoreManager`의 점수 변경 이벤트를 구독하여 텍스트 업데이트.
5. **씬 구성**:
   - 빈 GameObject `ScoreSystem` 생성.
   - `ScoreManager`, `ScoreUI`, `UIDocument` 컴포넌트 추가.
   - `UIDocument`에 `ScoreHUD.uxml` 연결.

# Verification & Testing
1. **점수 획득 테스트**: 과일 합성 시 현재 점수가 즉시 증가하는지 확인.
2. **수박 보너스 테스트**: 수박(11티어) 두 개 합성 시 147점(66+81)이 가산되는지 확인.
3. **데이터 저장 테스트**: 게임 종료 후 재시작 시 최고 점수가 유지되는지 확인.
4. **UI 레이아웃 테스트**: 다양한 해상도에서 점수 UI가 좌측 상단에 올바르게 고정되는지 확인.
