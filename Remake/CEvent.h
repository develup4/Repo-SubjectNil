//
// ���ϸ� : CEvent.h
// 
// ���� : �̺�Ʈ ȭ���� ��Ʈ���ϴ� Ŭ����
//		  CEventSprite�� ��Ʈ�� ������Ʈ���� ������ ������,
//		  ��ũ��Ʈ���Ͽ��� ��ɾ �о�� �� ��ü���� �����ϰ� �����Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//


#pragma once

#include "CScene.h"
#include "CEventSprite.h"
#include "CEventMap.h"
#include "CSpeakWnd.h"
#include "CDivergeWnd.h"
#include "CEventStaticImage.h"
#include "CEventAnimImage.h"
#include "CEventChar.h"

typedef struct CommandNode
{
	char command[1024]; //��ũ��Ʈ ��ɾ� �� ������ 1024 ����Ʈ�� ���� �ʵ��� ��ũ��Ʈ ������ ���۽� ������ ���ϵ��� ����
	CommandNode* next;
} CommandNode;

typedef struct ParameterNode
{
	char* parameter;
	ParameterNode* next;
} ParameterNode;

//3���� ��ũ�帮��Ʈ�� �����ϰԵȴ�.
//��ɾ���� ��ũ�帮��Ʈ, �Ķ���� ��ũ�帮��Ʈ �׸��� BitmapOBject���� ����� ����Ʈ(��������� Draw�Ǵ� ������ ����)
//��������Ʈ ����Ʈ�� ��ο쿡�� �̿�

class CEvent : public CScene
{
private:
	//����Ʈ���
	CommandNode* cHead;
	CommandNode* cTail;
	CommandNode* ptr;
	ParameterNode* pHead;
	ParameterNode* pTail;

	//����� BOB�� ������ ��������� �������� ����̹Ƿ� �����ڿ��� �ٷ� ���ε���Ų��
	CEventMap* map;
	CEventChar* charHead;
	CEventChar* charTail;
	CEventAnimImage* animImageHead;
	CEventAnimImage* animImageTail;
	CEventStaticImage* staticImageHead;
	CEventStaticImage* staticImageTail;
	CDivergeWnd* divergeHead;
	CDivergeWnd* divergeTail;
	CSpeakWnd* speakwnd;

	//���� �̷� BooleanŸ�� ������ �þ�ٸ� MS���� �ϴ°�ó�� ��Ʈ�� ��� ��������?
	bool executeFlag; //���� ��ɾ ������������ ��Ÿ���� ����
	bool drawSkip; //�̹� �������� �������� ��ŵ�� �������� ��Ÿ���� ����
	bool flag; //����ó���� �ӽú���

	//�� Ŭ���� ���������θ� ����� �Լ�
	char* PARAM(int index); //��ũ��Ʈ ��ɾ�� ���ڷ� ���� ���ڹ�°�� �Ķ���͸� ���ڿ��� �޾ƿ��� �Լ�
	int NPARAM(int index); //��ũ��Ʈ ��ɾ�� ���ڷ� ���� ���ڹ�°�� �Ķ���͸� ���������� �޾ƿ��� �Լ�

protected:

public:
	CEvent(SceneParamaterBundle sceneParam);
	~CEvent();

	void FrameMove();
	void Render();

	void ReadScript(char* line); //���� ��ũ��Ʈ ��ɾ �ҷ��ͼ� �Ķ���� ������ �����ִ� �Լ�
	bool ExecuteScript(); //���� ����Ű���ִ� ��ũ��Ʈ ��ɾ �����Ű�� �Լ�
};