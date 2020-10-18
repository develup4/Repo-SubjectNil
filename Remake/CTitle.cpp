//
// ���ϸ� : CTitle.cpp
// 
// ���� : Ÿ��Ʋ Ŭ������ ����
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//

#include <windows.h>
#include "CTitle.h"


//////////////////////////////////////////////////////////////////////////////////////////
// CTitle()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CTitle Ŭ������ ������.
//		  Ÿ��Ʋ ��ư ��ü���� �����Ѵ�.
//		  Ÿ��Ʋȭ�鿡���� BGM�� ����Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

CTitle::CTitle(SceneParamaterBundle sceneParam) : CScene(sceneParam)
{
	D3DXCreateTextureFromFile(pd3dDevice, "Texture\\Title\\BackGround.jpg", &tBackGround);
	newgameButton = new CTitleButton(480, 500, 150, 33, pd3dDevice, cursor, sprite, "Texture\\Title\\NewGameOn.jpg", "Texture\\Title\\NewGameOff.jpg");
	loadgameButton = new CTitleButton(480, 533, 150, 33, pd3dDevice, cursor, sprite, "Texture\\Title\\LoadGameOn.jpg", "Texture\\Title\\LoadGameOff.jpg");
	exitButton = new CTitleButton(480, 566, 150, 34, pd3dDevice, cursor, sprite, "Texture\\Title\\ExitOn.jpg", "Texture\\Title\\ExitOff.jpg");
	dshow->Initialize(hWnd, TITLEBGMPATH);
	dshow->Run();
	FadeOut();
}


//////////////////////////////////////////////////////////////////////////////////////////
// ~CTitle()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CTitle Ŭ������ �Ҹ���.
//		  �ؽ��ĸ� �����Ѵ�.
//		  Ÿ��Ʋ ��ư ��ü���� �����Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

CTitle::~CTitle()
{
	tBackGround->Release();
	delete newgameButton;
	delete loadgameButton;
	delete exitButton;
}


//////////////////////////////////////////////////////////////////////////////////////////
// void FrameMove()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ���� ������ ó���� �ϴ� �Լ�
//		  CSceneŬ������ �����Լ��� �������̵��Ѵ�.
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CTitle::FrameMove()
{
	BgmRepeatCheck();

	//��ư�� ���� ����
	if (newgameButton->MouseOnCheck() && cursor->LButtonClick())
	{
		PlaySound("Sound\\Click.wav", 0, SND_FILENAME | SND_ASYNC);
		FadeIn();
		sceneKind = EVENT;
	}
	else if (loadgameButton->MouseOnCheck() && cursor->LButtonClick())
	{
		PlaySound("Sound\\Click.wav", 0, SND_FILENAME | SND_ASYNC);
		FadeIn();
		//���� ���̺�, �ε��� ������. ���� ���۽ÿ� CDataŬ�������� �����͸� �����ͼ� �־��ִ� �������� ����
	}
	else if (exitButton->MouseOnCheck() && cursor->LButtonClick())
	{
		PlaySound("Sound\\Click.wav", 0, SND_FILENAME | SND_ASYNC);
		FadeIn();
		PostQuitMessage(0);
	}
}


//////////////////////////////////////////////////////////////////////////////////////////
// void Render()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ������ ó���� �ϴ� �Լ�
//		  CSceneŬ������ �����Լ��� �������̵��Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CTitle::Render()
{
	DrawTexture(tBackGround, 0, 0, 0, 0, screenWidth, screenHeight, 100);
	newgameButton->Draw();
	loadgameButton->Draw();
	exitButton->Draw();
}