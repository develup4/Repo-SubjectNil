//
// ���ϸ� : CBattleChar.h
// 
// ���� : ����ĳ���͵��� ����κ��� ���� Ŭ����.
//		  ����ĳ���͵��� �� Ŭ������ ��ӹ޾Ƽ� ����Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//


#pragma once
#include <d3d9.h>
#include <d3dx9.h>
#include "CBattleMap.h"

class CBattleChar
{
private:

protected:
	enum {FILLTP, MOVABLE, MOVE};
	enum {DOWN, LEFT, RIGHT, UP};

	LPDIRECT3DDEVICE9 pd3dDevice;
	LPD3DXSPRITE sprite;
	LPDIRECT3DTEXTURE9 tChar;
	LPDIRECT3DTEXTURE9 tTPGage; //TP�������� ��ü
	LPDIRECT3DTEXTURE9 tTPG; //TP��������

	CBattleMap* bMap;

	//ĳ���� ���� ������
	int x;
	int y;
	int tp;
	int movableCell;
	int step;
	int state;
	int direction;

	//ĳ���� ���� ���� �迭
	int** movableMap; //��ֹ� ������ ������� 2�����迭
	int* moveOrder; //�̵���θ� �����ϱ� ���� �迭
	int ptr;

	//���� ��� �Լ�
	void MoveCharacter(int objX, int objY, int n); //�ִܰ�� ����Լ�
	bool Step(); //ĳ���� �̵��ܰ� ó��

public:
	CBattleChar(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CBattleMap* bMap, int x, int y, int movableCell);
	~CBattleChar();

	int GetCharX();
	int GetCharY();

	//�������̽�
	virtual void TakeAction() = 0;
	virtual void Draw() = 0;
	virtual void FindDistance(int x, int y, int n) = 0; //�̵����ɹ����� ����ϴ� �Լ�
};