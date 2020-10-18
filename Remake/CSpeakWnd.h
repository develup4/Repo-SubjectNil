//
// ���ϸ� : CSpeakWnd.h
// 
// ���� : �̺�Ʈ���� ���Ǵ� ��ȭâ ��ü�� ���� Ŭ�����̴�.
//		  BOB(Bitmap OBject)�� �����̴�.
//		  ��ȭâ�� �Ϸ���Ʈ, ���Ϸ��̹����� ����Ѵ�.
//		  ��系���� �帣���� ����Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//

#pragma once
#include <string.h>
#include "CEventSprite.h"
#include "CCursor.h"

enum {FADEIN, SPEAK, SPEAKEND, FADEOUT}; //����ܰ踦 ��Ÿ���� ���

class CSpeakWnd: public CEventSprite
{
private:
	LPDIRECT3DTEXTURE9 tSpeakWnd;
	LPDIRECT3DTEXTURE9 tComplete;
	LPDIRECT3DTEXTURE9 tIllust;
	CCursor* cursor;

	int step; //�߰��ӽú���
	bool speakEnd; //CEvent Ŭ������ ����ϱ����� ����ϴ� ������ ��簡 ���������� �����ϴ� ����
	char name[32];
	char dialog[6][256]; //��� �� ������ 256����Ʈ�� ���� �ʵ��� ��ũ��Ʈ ������ ������ ���Ѵ�.

	//��� ó�� ���� �Լ�
	int speakWndAlpha;
	int illustAlpha;
	int index; //���� ��µ� ������ġ�� ����Ŵ
	int ptr; //��µ� ��ġ�� ����Ű�� ����
	int completeStep;

protected:
	
public:
	CSpeakWnd(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CCursor* cursor, LPD3DXFONT pFont);
	~CSpeakWnd();

	void Draw();

	bool GetSpeakEnd();
	void SetIllustTexture(char* textureFilePath);
	void SetName(char* name);
	void SetDialog(char* dialog1, char* dialog2, char* dialog3, char* dialog4, char* dialog5, char* dialog6);
	void SetSpeakEnd(bool end);
};

//�ٳѱ��� �����ؼ� �ڵ����� ���̿� ���� ��縦 �ڸ��� ����� �����ߴµ�
//Ư���� �ٳѱ涧�� \n ó�� Ư�� ���ڷ� ó���ϸ� �ǹǷ� ���߿� ����� ��������