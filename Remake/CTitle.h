//
// ���ϸ� : CTitle.h
// 
// ���� : Ÿ��Ʋ ȭ���� ��Ʈ���ϴ� Ŭ����
//		  Ÿ��Ʋ ��ư Ŭ������ �����Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//

#pragma once
#include "CScene.h"
#include "CTitleButton.h"

#define TITLEBGMPATH "Music\\Team_1_Sadness_Theme_sadness.mp3"

class CTitle : public CScene
{
private:
	LPDIRECT3DTEXTURE9 tBackGround;
	CTitleButton* newgameButton;
	CTitleButton* loadgameButton;
	CTitleButton* exitButton;

protected:

public:
	CTitle(SceneParamaterBundle sceneParam);
	~CTitle();

	void FrameMove();
	void Render();
};