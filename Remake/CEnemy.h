//
// ���ϸ� : CEnemy.h
// 
// ���� : �ΰ��������� �����̴� �� ĳ������ Ŭ����
//		  CBattleChar Ŭ������ ����Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//


#pragma once
#include "CPlayer.h"

class CEnemy : public CBattleChar
{
private:
	CPlayer* player; //�߰� Ȥ�� ������ �ؾ��ϹǷ� �÷��̾������� �ʿ�
	bool chase; //�߰��������� ��Ÿ���� ����
	int** distance; //�÷��̾���� �Ÿ��� ����ϴ� ������ �迭

protected:

public:
	CEnemy(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CBattleMap* bMap, int x, int y, int movableCell, CPlayer* player, bool chase);
	~CEnemy();

	void TakeAction();
	void Draw();

	void FindDistance(int x, int y, int n);
};