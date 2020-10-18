//
// ���ϸ� : CEventStaticImage.h
// 
// ���� : �̺�Ʈ���� ���Ǵ� ������(�ִϸ��̼ǵ����ʴ�) �̹��� ��ü�� ���� Ŭ����
//		  BOB(Bitmap OBject)�� �����̴�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//

#pragma once
#include "CEventSprite.h"

class CEventStaticImage : public CEventSprite
{
private:
	int step;
	int x;
	int y;
	int width;
	int height;
	int duration;
	LPDIRECT3DTEXTURE9 tImage;

protected:

public:
	CEventStaticImage(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, LPD3DXFONT pFont);
	~CEventStaticImage();

	void Draw();

	//ĸ��ȭ
	void SetX(int val);
	void SetY(int val);
	void SetWidth(int val);
	void SetHeight(int val);
	void SetDuration(int val);
	void SetImage(char* FilePath);
};
