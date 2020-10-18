//
// ���ϸ� : CBattle.h
// 
// ���� : ������ �����ϴ� Ŭ����
//		  ���� �ʰ� �÷��̾�, �߰���, ������ ������ ��ü�� ������,
//		  �÷��̾�� �����ڸ� ������(��ǥ�� ��������) �¸��ϰ�,
//		  �߰��ڰ� �÷��̾ ������(��ǥ�� ��������) �й��ϴ� ������ �������� ��ü�Ǿ��ִ� �����̴�.
//		  �� Ŭ������ ���� �� ��ü�� �����ϰ� �ִ�.
//		  �� Ŭ������ �¸� Ȥ�� �й踦 üũ�Ѵ�.
//		  
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//


#pragma once
#include <stdio.h>
#include "CScene.h"
#include "CPlayer.h"
#include "CBattleMap.h"
#include "CEnemy.h"

class CBattle : public CScene
{
private:
	CBattleMap* bMap;
	CPlayer* player;
	CEnemy* bChaser;
	CEnemy* bFugitive;
protected:

public:
	CBattle(SceneParamaterBundle sceneParam);
	~CBattle();

	void FrameMove();
	void Render();

	void CheckWin();
	void CheckLose();
};