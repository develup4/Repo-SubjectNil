//
// ���ϸ� : CDivergeWnd.h
// 
// ���� : �̺�Ʈ���� ���Ǵ� �б�â ��ü�� ���� Ŭ����.
//		  �б⿡ ���õ� ������(�бⰳ���� ���� ��)�� ������ �ִ�.
//		  �б�â �̹��� �ؽ��ĸ� ������ �ִ�.
//		  �б⿡ ���� ���ο� ��ũ��Ʈ�� CData Ŭ������ ������ �����Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//


#pragma once
#include "CEventSprite.h"
#include "CData.h"
#include "CCursor.h"

class CDivergeWnd : public CEventSprite
{
private:
	int x;
	int y;
	char* text;
	char* scriptFilePath;
	int kindScript;

	LPDIRECT3DTEXTURE9 tDiverge;
	LPDIRECT3DTEXTURE9 tDivergeSelect;

	CCursor* cursor;
	LPD3DXFONT pFont;
	int* sceneKind;
	CData* pData;

protected:

public:
	CDivergeWnd(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CCursor* cursor, LPD3DXFONT pFont, int* sceneKind, CData* pData);
	~CDivergeWnd();

	void Draw();
	bool MouseOnCheck();

	//ĸ��ȭ
	void SetX(int val);
	void SetY(int val);
	void SetText(char* str);
	void SetScriptFilePath(char* path);
	void SetKindScript(int kind);
	char* GetScriptFilePath();
	int GetKindScript();
};