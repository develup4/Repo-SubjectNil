//
// ���ϸ� : CBattleMap.h
// 
// ���� : �������� �����ϴ� Ŭ����
//		  �浹�迭 �� �����ʿ� �ʿ��� �����͵��� ������ �ִ�.
//		  �ʽ�ũ���� ó���� �� �ִ�.
//		  �� �ؽ��� �̹����� �������Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//


#pragma once
#include <d3d9.h>
#include <d3dx9.h>
#include "CCursor.h"

class CBattleMap
{
private:
	LPDIRECT3DDEVICE9 pd3dDevice;
	LPD3DXSPRITE sprite;
	LPDIRECT3DTEXTURE9 map;
	CCursor* cursor;
	int sx; //���� ���η� ��ũ�ѵ� ����
	int sy; //���� ���η� ��ũ�ѵ� ����
	int mapX; //���� ���α���
	int mapY; //���� ���α���
	int** collideMap; //�浹�迭�� �� �� �ִ� ���� �� �� ���� ���� �����س��� �������迭
	const int screenWidth;
	const int screenHeight;

protected:

public:
	CBattleMap(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CCursor* cursor, int mapX, int mapY, int screenWidth, int screenHeight, char* battleMapTexture);
	~CBattleMap();

	void TakeAction();
	void Draw();

	void SetCollideMap(int x, int y, int value);
	int GetSX();
	int GetSY();
	int GetMapX();
	int GetMapY();
	bool GetMovable(int x, int y);
};