# Project Overview
- Game Title: Suika Game (수박 게임)
- UI System: uGUI (Canvas) - 사용자 요청으로 UI Toolkit에서 변경.

# Game Mechanics
## Core Gameplay Loop
- 과일 합성 → 점수 획득 → 최고 점수 저장.

# UI
- **Canvas Score HUD**:
  - `Canvas` (Screen Space - Overlay)
  - `Canvas Scaler` (Scale with Screen Size, 1920x1080)
  - `TextMeshPro`를 이용한 점수 표시 (상단 좌측).

# Key Assets & Context
- `Assets/Scripts/ScoreUI_Canvas.cs`: uGUI (TMP) 전용 점수 표시 스크립트.
- `Assets/Scripts/ScoreManager.cs`: 기존 점수 관리 로직 유지.

# Implementation Steps
1. **ScoreUI_Canvas 구현**:
   - `TextMeshProUGUI`를 참조하여 현재 점수와 최고 점수 업데이트.
   - `ScoreManager`의 이벤트를 구독.
2. **씬 구성 (uGUI)**:
   - 기존 `ScoreSystem`에서 `UIDocument`, `ScoreUI` 제거.
   - `Canvas`, `CanvasScaler`, `GraphicRaycaster` 추가.
   - `TextMeshPro - Text` 오브젝트 2개 생성 (Score, Best).
   - `ScoreUI_Canvas` 컴포넌트 추가 및 텍스트 연결.
3. **에셋 정리**:
   - 사용하지 않는 `Assets/UI` (UITK) 폴더 및 `Assets/Scripts/ScoreUI.cs` 삭제 가능성 검토 (일단 비활성화).

# Verification & Testing
1. **UI 가독성 테스트**: 글자 크기 및 위치가 캔버스 시스템에서 올바르게 표시되는지 확인.
2. **기능 테스트**: 합성 시 점수 반영 확인.
