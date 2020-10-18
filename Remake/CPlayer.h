//
// ���ϸ� : CPlayer.h
// 
// ���� : ����ڰ� �����ϴ� �÷��̾� ĳ������ Ŭ����
//		  CBattleChar Ŭ������ ����Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//


#pragma once
#include "CBattleChar.h"
#include "CCursor.h"

class CPlayer : public CBattleChar
{
private:
	CCursor* cursor;
	LPDIRECT3DTEXTURE9 tBlueCell;

protected:

public:
	CPlayer(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CBattleMap* bMap, int x, int y, int movableCell, CCursor* cursor);
	~CPlayer();

	void TakeAction();
	void Draw();

	void FindDistance(int x, int y, int n);
};