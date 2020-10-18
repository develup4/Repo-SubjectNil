//
// ���ϸ� : CEventMap.h
// 
// ���� : �̺�Ʈ���� ���Ǵ� �� ��ü�� ���� Ŭ����
//		  �� �̹��� ������ ������ �ִ�.
//		  ��ũ�� ������ ������ �ִ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//


#pragma once
#include "CEventSprite.h"

class CEventMap : public CEventSprite
{
private:
	LPDIRECT3DTEXTURE9 tMap;
	int scrollX;
	int scrollY;

	const int screenWidth;
	const int screenHeight;

protected:
	
public:
	CEventMap(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, LPD3DXFONT pFont, int screenWidth, int screenHeight);
	~CEventMap();
	
	//ĸ��ȭ ����
	void SetMapTexture(char* filePath);
	int GetScrollX() const;
	void SetScrollX(int val);
	int GetScrollY() const;
	void SetScrollY(int val);

	//�Ϲ��Լ�
	void Draw();
};
