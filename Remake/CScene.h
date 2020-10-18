//
// ���ϸ� : CScene.h
// 
// ���� : ���� ����� ����, �����ϴ� Ŭ����.
//		  �߻�Ŭ������ �̸� ����ϴ� ��üŬ�������� �� ��鿡 ���� Control Class�� �ش��Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//

#pragma once
#include <Windows.h>
#include <d3d9.h>
#include <d3dx9.h>
#include "CDXShow.h"
#include "CCursor.h"
#include "CData.h"

enum {TITLE, EVENT, BATTLE, ENDING, NEXTEVENT, NEXTBATTLE}; //����� ������ ��Ÿ���� ���

struct SceneParamaterBundle
{
	HWND hWnd;
	HINSTANCE hInst;
	LPDIRECT3D9 pD3D;
	LPDIRECT3DDEVICE9 pd3dDevice;
	LPD3DXFONT pFont;
	const int screenWidth;
	const int screenHeight;
	int sceneKind;
	CData* pData;
};

class CScene
{
private:

protected:
	//�ڵ�
	HWND hWnd;
	HINSTANCE hInst;
	LPDIRECT3D9 pD3D;
	LPDIRECT3DDEVICE9 pd3dDevice;

	const int screenWidth;
	const int screenHeight;
	int sceneKind; //���� �� ������ ��� ����

	//�����밴ü
	LPDIRECT3DTEXTURE9 tBlack;
	CCursor* cursor;
	LPD3DXSPRITE sprite;
	LPD3DXFONT pFont;
	CDXShow* dshow;
	CData* pData;

	//��ӹ޴� Ŭ�������� �������� ��밡���� �Լ�
	void BgmRepeatCheck(); //BGM �ݺ���� �Լ�
	void FadeIn();
	void FadeOut();
	void DrawTexture(LPDIRECT3DTEXTURE9 pTexture, int x, int y, int x1, int y1, int x2, int y2, int alpha);

public:
	CScene(SceneParamaterBundle sceneParam);
	~CScene();

	//ĸ��ȭ ����
	int GetSceneKind() const;
	CCursor* GetCursor() const;

	//���������Լ�(�������̽�)
	virtual void FrameMove() = 0;
	virtual void Render() = 0;
};
