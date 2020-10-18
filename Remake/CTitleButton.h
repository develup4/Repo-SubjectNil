//
// ���ϸ� : CTitleButton.h
// 
// ���� : Ÿ��Ʋ ��ư Ŭ����.
//		  ���콺�� ��ü�� ��ġ ���� �ö������ �̹����� �ٲ۴�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//


#pragma once
#include <Windows.h>
#include <d3d9.h>
#include <d3dx9.h>
#include "CCursor.h"

class CTitleButton
{
private:
	int x;
	int y;
	int width;
	int height;
	CCursor* cursor;
	LPD3DXSPRITE sprite;
	LPDIRECT3DTEXTURE9 tButtonOn;
	LPDIRECT3DTEXTURE9 tButtonOff;

protected:

public:
	CTitleButton(int x, int y, int width, int height, LPDIRECT3DDEVICE9 pd3dDevice, CCursor* cursor, LPD3DXSPRITE sprite, char* onFilePath, char* offFilePath);
	~CTitleButton();

	bool MouseOnCheck();
	void Draw();
};
