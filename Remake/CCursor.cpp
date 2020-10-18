//
// ���ϸ� : CCursor.cpp
// 
// ���� : ���콺 Ŀ�� Ŭ������ ����
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//


#include "CCursor.h"


//////////////////////////////////////////////////////////////////////////////////////////
// CCursor(LPDIRECT3DDEVICE9 pd3dDevice, HWND hWnd, LPD3DXSPRITE sprite)
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CCursor Ŭ������ ������.
//		  ��ġ�����͸� �ʱ�ȭ�Ѵ�.
//		  �ؽ��ĸ� ����Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

CCursor::CCursor(LPDIRECT3DDEVICE9 pd3dDevice, HWND hWnd, LPD3DXSPRITE sprite)
{
	this->hWnd = hWnd;
	this->sprite = sprite;

	//���� Ŀ���� ũ�Ⱑ ���Ѵٰų� �ִϸ��̼� �ӵ��� ������ �����Ƿ� �����ڿ��� ���� �����ʾҴ�.
	curState = NCURSOR;
	cursorWidth = 40;
	cursorHeight = 40;
	animationSpeed = 10; //�� �������� �ݺ��Ǵ� Ƚ��
	step = 0;
	nFrame = 4; //��� Ŀ���� �ִϸ��̼� ��
	sFrame = 3; //���� Ŀ���� �ִϸ��̼� ��
	
	D3DXCreateTextureFromFile(pd3dDevice, "Texture\\Cursor\\NCursor.tga", &tNCursor);
	D3DXCreateTextureFromFile(pd3dDevice, "Texture\\Cursor\\SCursor.tga", &tSCursor);
}


//////////////////////////////////////////////////////////////////////////////////////////
// ~CCursor()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CCursor Ŭ������ �Ҹ���.
//		  �ؽ��ĸ� �����Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

CCursor::~CCursor()
{
	tNCursor->Release();
	tSCursor->Release();
}


//////////////////////////////////////////////////////////////////////////////////////////
// void DrawCursor()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : Ŀ�� �̹����� �׸��� �Լ�.
//		  ������� AnimSpeed�� ���� �ӵ��� �ִϸ��̼ǵǴ� �̹����� �׸���.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CCursor::DrawCursor()
{
	if (curState != NODRAW)
	{
		POINT ptCursor;
		GetCursorPos(&ptCursor);
		ScreenToClient(hWnd, &ptCursor);

		sprite->Begin(D3DXSPRITE_ALPHABLEND);
		RECT rect = {cursorWidth * (step / animationSpeed), 0, cursorWidth * (step / animationSpeed + 1), cursorHeight};
		D3DXVECTOR3 pos((float) ptCursor.x, (float) ptCursor.y, 0);
		sprite->Draw(curState == NCURSOR ? tNCursor : tSCursor, &rect, NULL, &pos, D3DCOLOR_RGBA(255, 255, 255, 255));
		sprite->End();
		step++;
		if (step >= (curState == NCURSOR ? nFrame * animationSpeed : sFrame * animationSpeed)) step = 0;
	}
}


//////////////////////////////////////////////////////////////////////////////////////////
// bool RButtonClick(), bool LButtonClick()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ��  �� : ���콺 (������, ����) ��ư�� Ŭ���Ǿ����� üũ�ϴ� �Լ�.
// ���ϰ� : boolean��(Ŭ������)
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

bool CCursor::RButtonClick()
{
	if (GetAsyncKeyState(VK_RBUTTON))
	{
		return true;
	}
	else
	{
		return false;
	}
}

bool CCursor::LButtonClick()
{
	if (GetAsyncKeyState(VK_LBUTTON))
	{
		return true;
	}
	else
	{
		return false;
	}
}


//////////////////////////////////////////////////////////////////////////////////////////
// void SetCursorPoint()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ���콺 Ŀ���� ��ġ�� �����ϴ� �Լ�.
// ���� : int x, int y(�̵��� Ŀ���� ��ġ ��ǥ)
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CCursor::SetCursorPoint(int x, int y)
{
	SetCursorPos(x, y);
}


//////////////////////////////////////////////////////////////////////////////////////////
// POINT GetCursorPoint()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ��  �� : ���콺 Ŀ���� ��ġ�� ���� �� �ִ� �Լ�.
// ���ϰ� : POINT ptCursor(���콺 Ŀ���� ��ġ ��ǥ)
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

POINT CCursor::GetCursorPoint() const
{
	POINT ptCursor;
	GetCursorPos(&ptCursor);
	ScreenToClient(hWnd, &ptCursor); //���� ������ ������ ��ǥȭ�Ѵ�.

	return ptCursor;
}


//////////////////////////////////////////////////////////////////////////////////////////
// void SetCursorState()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ���콺 Ŀ���� ���¸� �����ϴ� �Լ�.
// ���� : int val(�ٲ� ���콺 Ŀ���� ����)
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CCursor::SetCurState(int val)
{
	curState = val;
}


//////////////////////////////////////////////////////////////////////////////////////////
// int GetCursorState()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ��  �� : ���콺 Ŀ���� ���¸� ���� �� �ִ� �Լ�.
// ���ϰ� : int curState(���콺 Ŀ���� ����)
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

int CCursor::GetCurState() const
{
	return curState;
}