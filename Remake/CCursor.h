//
// ���ϸ� : CCursor.h
// 
// ���� : ���콺 Ŀ���� �����ϴ� Ŭ����.
//		  Ŀ���� �׸���.
//		  Ŀ���� �̹����� ������ �� �ִ�.
//		  ���콺�� Ŭ���� �����Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//


#pragma once
#include <Windows.h>
#include <d3d9.h>
#include <d3dx9.h>

enum {NCURSOR, SCURSOR, NODRAW}; //Ŀ���� ������ ��Ÿ���� ���, ������� ���Ŀ��, ����Ŀ��, �׸�������

class CCursor
{
private:
	HWND hWnd;
	LPD3DXSPRITE sprite;
	int curState;
	int cursorWidth;
	int cursorHeight;
	int animationSpeed; //������ �������� �ݺ��Ǵ� Ƚ���� �������� ������ �ִϸ��̼� �ȴ�.
	int step;
	int nFrame; //��� Ŀ���� �ִϸ��̼� ��
	int sFrame; //���� Ŀ���� �ִϸ��̼� ��

	LPDIRECT3DTEXTURE9 tNCursor;
	LPDIRECT3DTEXTURE9 tSCursor;

protected:

public:
	CCursor(LPDIRECT3DDEVICE9 pd3dDevice, HWND hWnd, LPD3DXSPRITE sprite);
	~CCursor();

	void DrawCursor();
	bool RButtonClick();
	bool LButtonClick();

	//ĸ��ȭ ����
	void SetCursorPoint(int x, int y);
	POINT GetCursorPoint() const;
	void SetCurState(int val);
	int GetCurState() const;
};