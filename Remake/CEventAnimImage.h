//
// ���ϸ� : CEventAnimImage.h
// 
// ���� : �̺�Ʈ���� ���Ǵ� �ִϸ��̼� �̹��� ��ü�� ���� Ŭ����.
//		  �ִϸ��̼� �̹��� ������ ������ �ִ�.
//		  �ִϸ��̼� �̹��� �ؽ��ĸ� ������ �ִ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//


#pragma once
#include "CEventSprite.h"

class CEventAnimImage : public CEventSprite
{
private:
	int step;
	int x;
	int y;
	int frameWidth;
	int frameHeight;
	int numFrame; //�ִϸ��̼� ��������Ʈ �������� ����
	int animSpeed; //������ �������� �ݺ��ؼ� ����Ǵ� Ƚ��(��, �������� ������)
	LPDIRECT3DTEXTURE9 tImage;
protected:

public:
	CEventAnimImage(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, LPD3DXFONT pFont);
	~CEventAnimImage();

	void Draw();

	//ĸ��ȭ
	void SetX(int val);
	void SetY(int val);
	void SetFrameWidth(int val);
	void SetFrameHeight(int val);
	void SetNumFrame(int val);
	void SetAnimSpeed(int val);
	void SetImage(char* FilePath);
};