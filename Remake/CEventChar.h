//
// ���ϸ� : CEventChar.h
// 
// ���� : �̺�Ʈ���� ���Ǵ� ĳ���� ��ü�� ���� Ŭ����.
//		  ĳ���� ������ ������ �ִ�.
//		  ĳ���� �ؽ��ĸ� ������ �ִ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//


#pragma once
#include "CEventSprite.h"

enum {DOWN, LEFT, RIGHT, UP}; //��������! �ؽ��� �̹����� ������ ������ ����

class CEventChar : public CEventSprite
{
private:
	char id[64];
	int x;
	int y;
	int direction;
	LPDIRECT3DTEXTURE9 tImage;
	char texturePath[128];
	int step;

protected:

public:
	CEventChar(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, LPD3DXFONT pFont);
	~CEventChar();

	void Draw();
	void Step();
	void ChangeDirection(int direction);

	//ĸ��ȭ ����
	void SetID(char* id);
	void SetX(int x);
	void SetY(int y);
	void SetDirection(char* direction);
	void SetTexturePath(char* path);
	char* GetID();
	int GetX();
	int GetY();
	int GetDirection();
	char* GetTexturePath();
};