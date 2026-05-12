# SuikaGame — Claude Code 참조 문서

## 프로젝트 개요

Unity 6 (URP 2D) 기반 수박 게임(Suika Game) 클론 프로젝트.

## 필수 참조 문서

구현 작업 전 반드시 읽어야 할 GDD:

- **[GDD.md](./GDD.md)** — 게임 규칙, 과일 계층, 점수·승패, UI 구성, 톤 & 분위기

구현 요청이 들어오면 GDD.md를 컨텍스트로 사용해 설계 일관성을 유지한다.

## 기술 스택

- Unity 6 (URP 2D)
- C# / Unity Physics 2D (Box2D)
- Input System 패키지

## 도구 & 자동화

- **CSharpier** — C# 파일 저장 시 자동 포맷 (PostToolUse 훅, `.claude/hooks/format-cs.ps1`)
- **Unity MCP** — Unity Editor와 직접 연동 (`.mcp.json` → `relay_win.exe`)
  - 씬 캡처, 콘솔 로그 조회, 에디터 명령 실행 가능

## 코드 컨벤션

- C# 포맷은 CSharpier 기본 설정을 따른다.
- 주석은 WHY가 명확히 필요할 때만 한 줄로 작성한다.
