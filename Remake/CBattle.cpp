//
// ���ϸ� : CBattle.cpp
// 
// ���� : CBattle Ŭ������ ����
//		  
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//


#include "CBattle.h"
#pragma warning(disable : 4996)


//////////////////////////////////////////////////////////////////////////////////////////
// CBattle(SceneParamaterBundle sceneParam)
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CBattle Ŭ������ ������.
//		  �� ��ü, �÷��̾� ��ü, �߰��� ��ü, ������ ��ü�� ���ε��Ѵ�.
//		  ��ũ��Ʈ������ �о�ͼ� �������� ������ �о�´�.
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////

CBattle::CBattle(SceneParamaterBundle sceneParam) : CScene(sceneParam)
{
	char buffer[128];
	FILE* fp = fopen(pData->GetScriptFilePath(), "r");

	//������ ����
	fscanf(fp, "%s", buffer);
	int x = atoi(buffer);
	fscanf(fp, "%s", buffer);
	int y = atoi(buffer);
	fscanf(fp, "%s", buffer);
	bMap = new CBattleMap(pd3dDevice, sprite, cursor, x, y, screenWidth, screenHeight, buffer);

	//�浹�迭 �Է�
	for (int i = 0; i < x; i++)
	{
		for (int j = 0; j < y; j++)
		{
			fscanf(fp, "%s", buffer);
			bMap->SetCollideMap(j, i, atoi(buffer));
		}
	}

	//�÷��̾� ���� �Է�
	fscanf(fp, "%s", buffer);
	x = atoi(buffer);
	fscanf(fp, "%s", buffer);
	y = atoi(buffer);
	fscanf(fp, "%s", buffer);
	int m = atoi(buffer);
	player = new CPlayer(pd3dDevice, sprite, bMap, x, y, m, cursor);

	//�߰��� ���� �Է�
	fscanf(fp, "%s", buffer);
	x = atoi(buffer);
	fscanf(fp, "%s", buffer);
	y = atoi(buffer);
	fscanf(fp, "%s", buffer);
	m = atoi(buffer);
	bChaser = new CEnemy(pd3dDevice, sprite, bMap, x, y, m, player, true);

	//������ ���� �Է�
	fscanf(fp, "%s", buffer);
	x = atoi(buffer);
	fscanf(fp, "%s", buffer);
	y = atoi(buffer);
	fscanf(fp, "%s", buffer);
	m = atoi(buffer);
	bFugitive = new CEnemy(pd3dDevice, sprite, bMap, x, y, m, player, false);

	fclose(fp);

	dshow->Initialize(hWnd, "Music\\Team_2_Battle_Theme_Track08.mp3"); //���� ������ �ϳ��ۿ� ��� �׳� �������� ���� ��ũ��Ʈ���� ����������ϵ��� ����
	dshow->Run();
}


//////////////////////////////////////////////////////////////////////////////////////////
// ~CBattle()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CBattle Ŭ������ �Ҹ���.
//		  ��ü���� �����Ѵ�.
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////

CBattle::~CBattle()
{
	delete bMap;
	delete player;
	delete bChaser;
	delete bFugitive;
}


//////////////////////////////////////////////////////////////////////////////////////////
// void FrameMove()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ���� ������ ó���� �ϴ� �Լ�.
//		  CSceneŬ������ �����Լ��� �������̵��Ѵ�.
//		  �� ��ü���� �ൿ�Լ��� ȣ���Ѵ�.
//		  �¸��� �й� üũ�� �Ѵ�.
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CBattle::FrameMove()
{
	BgmRepeatCheck();

	player->TakeAction();
	bChaser->TakeAction();
	bFugitive->TakeAction();
	bMap->TakeAction();
	CheckWin();
	CheckLose();
}


//////////////////////////////////////////////////////////////////////////////////////////
// void Render()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ���� ������ ó���� �ϴ� �Լ�.
//		  CSceneŬ������ �����Լ��� �������̵��Ѵ�.
//		  �� ��ü���� ������ �Լ��� ȣ���Ѵ�.
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CBattle::Render()
{
	bMap->Draw();
	player->Draw();
	bChaser->Draw();
	bFugitive->Draw();
}


//////////////////////////////////////////////////////////////////////////////////////////
// void CheckWin()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ���ӿ��� �¸��ߴ����� üũ�ϴ� �Լ�.
//		  �� �̴ϰ��ӿ��� �¸��� �÷��̾�� �������� ��ǥ�� �������� ������ ���Ѵ�.
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CBattle::CheckWin()
{
	if (player->GetCharX() == bChaser->GetCharX() && player->GetCharY() == bChaser->GetCharY())
	{
		//����Ŭ����
		MessageBox(hWnd, "���ӿ��� �й��Ͽ����ϴ�!", "�й�!", MB_OK);
		PostQuitMessage(0);
	}
}


//////////////////////////////////////////////////////////////////////////////////////////
// void CheckLose()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ���ӿ��� �й��ߴ����� üũ�ϴ� �Լ�.
//		  �� �̴ϰ��ӿ��� �й�� �÷��̾�� �߰����� ��ǥ�� �������� ������ ���Ѵ�.
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CBattle::CheckLose()
{
	if (player->GetCharX() == bFugitive->GetCharX() && player->GetCharY() == bFugitive->GetCharY())
	{
		//���ӿ���
		MessageBox(hWnd, "���ӿ��� �¸��Ͽ����ϴ�!", "�¸�!", MB_OK);
		PostQuitMessage(0);
	}
}