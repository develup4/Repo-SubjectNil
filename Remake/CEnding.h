//
// ���ϸ� : CEnding.h
// 
// ���� : ��������� �����ϴ� Ŭ����
//		  ���� �̱���
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//


#pragma once
#include "CScene.h"

class CEnding : public CScene
{
private:

protected:

public:
	CEnding(SceneParamaterBundle sceneParam);
	~CEnding();

	void FrameMove();
	void Render();
};