//
// ���ϸ� : CEventSprite.h
// 
// ���� : �̺�Ʈ���� ���Ǵ� BOB(Bitmap OBject)���� Base Ŭ����
//		  ���� �������� �����ϴ� ������ �������� �̿��ؼ� �������� �̵��� ��� ���ؼ��̴�.
//		  ��ӹ޴� Ŭ�������� �������� ���� ��ҵ��� ������ �ִ�.
//		  ��ӹ޴� Ŭ�������� �������� ����ϴ� �Լ����� ������ �ִ�.
//		  next��� �̸��� �����͸� ������ �־ �� ��ü�� ��ũ�帮��Ʈ�� �ȴ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//

#pragma once
#include <d3d9.h>
#include <d3dx9.h>
#include <Windows.h>

class CEventSprite
{
private:
	
protected:
	LPDIRECT3DDEVICE9 pd3dDevice;
	LPD3DXSPRITE sprite;
	LPD3DXFONT pFont;

	bool doDraw;
	
public:
	CEventSprite* next; //CEventSprite������ ����Ʈ�� ����� �� �ִ�

	CEventSprite(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, LPD3DXFONT pFont);
	~CEventSprite();

	void SetDoDraw(bool doing);
	void TextRender(char* text, int x1, int y1, int x2, int y2, int alpha);

	//���������Լ�(�������̽�)
	virtual void Draw() = 0;
};

// ������ ���� �ؽ��İ� �ߺ��Ǿ� ���Ǵ� ��찡 ���� ��� ���������
// ���࿡ �׷� ����� ��ü�� ���� �ؽ��ĸ� ���°ź��� �ؽ��ĵ鸸 ��Ƴ��� ��ü�� ���� �ҷ��ٰ� ����
// �޸𸮰� ������� �ʴ´�. �������� Ŀ�� �ɰ������� ����� ������ �ȵɼ��� ����. ���߿� ����غ���
// -> Texture Pool