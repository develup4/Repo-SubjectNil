//
// ���ϸ� : CEvent.cpp
// 
// ���� : �̺�Ʈ Ŭ������ ����
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//


#include "CEvent.h"
#pragma warning(disable : 4800)
#pragma warning(disable : 4996)



//////////////////////////////////////////////////////////////////////////////////////////
// CEvent()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CEvent Ŭ������ ������.
//		  ������� ����� 3���� ��ũ�帮��Ʈ�� �ʱ�ȭ�Ѵ�.
//		  ��ũ��Ʈ ������ �о�ͼ� ��ɾ� ��ũ�帮��Ʈ�� �����Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

CEvent::CEvent(SceneParamaterBundle sceneParam) : CScene(sceneParam)
{
	//���߿� STL���ԵǸ� STL�� �� �ٲ���

	executeFlag = false;
	drawSkip = false;
	flag = false;
	cursor->SetCurState(NODRAW);

	//��ɾ� ���δ����� �޾ƿ���
	cHead = new CommandNode;
	cTail = new CommandNode;
	cHead->next = cTail;
	cTail->next = cTail;
	ptr = cHead;

	FILE* pScriptFile = fopen(pData->GetScriptFilePath(), "r");
	while(feof(pScriptFile) == 0)
	{	
		CommandNode* line = new CommandNode;
		fgets(line->command, sizeof(line->command), pScriptFile); //��ũ��Ʈ ���Ͽ��� �� ������ ��ɾ� �Ѷ������� ������
		line->next = ptr->next;
		ptr->next = line;
		ptr = line;
	}
	fclose(pScriptFile);
	ptr = cHead->next;

	//�Ķ���� ����Ʈ �ʱ�ȭ
	pHead = new ParameterNode;
	pTail = new ParameterNode;
	pHead->next = pTail;
	pTail->next = pTail;

	//������� ���ε�
	map = new CEventMap(pd3dDevice, sprite, pFont, screenWidth, screenHeight);
	charHead = new CEventChar(pd3dDevice, sprite, pFont);
	charTail = new CEventChar(pd3dDevice, sprite, pFont);
	animImageHead = new CEventAnimImage(pd3dDevice, sprite, pFont);
	animImageTail = new CEventAnimImage(pd3dDevice, sprite, pFont);
	staticImageHead = new CEventStaticImage(pd3dDevice, sprite, pFont);
	staticImageTail = new CEventStaticImage(pd3dDevice, sprite, pFont);
	divergeHead = new CDivergeWnd(pd3dDevice, sprite, cursor, pFont, &sceneKind, pData);
	divergeTail = new CDivergeWnd(pd3dDevice, sprite, cursor, pFont, &sceneKind, pData);
	speakwnd= new CSpeakWnd(pd3dDevice, sprite, cursor, pFont);
	
	map->next = charHead;
	charHead->next = charTail;
	charTail->next = animImageHead;
	animImageHead->next = animImageTail;
	animImageTail->next = staticImageHead;
	staticImageHead->next = staticImageTail;
	staticImageTail->next = speakwnd;
	speakwnd->next = divergeHead;
	divergeHead->next = divergeTail;
	divergeTail->next = NULL;

	//������Ҵ� �� ��ü�� �������ϻ� ������ ��ο���� �����Ƿ� �׷������ʵ��� �����صд�
	CEventSprite* ptr = charHead;
	while(ptr != NULL)
	{
		ptr->SetDoDraw(false);
		ptr = ptr->next;
	}

	dshow->Initialize(hWnd, "Music\\Nil.mp3");
	dshow->Run(); //�ϴ� ������ ����ǰ��ؼ� BgmRepeatCheck()�� �̷������ �ֵ��� �Ѵ�
}


//////////////////////////////////////////////////////////////////////////////////////////
// ~CEvent()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CEvent Ŭ������ �Ҹ���.
//		  ��ũ�帮��Ʈ���� �����Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

CEvent::~CEvent()
{
	//��ɾ���� ����
	CommandNode* cTempStore;
	CommandNode* cTempPtr;

	cTempPtr = cHead->next;
	while (cTempPtr != cTail)
	{
		cTempStore = cTempPtr;
		cTempPtr = cTempPtr->next;
		delete cTempStore;
	}
	delete cHead;
	delete cTail;

	//�Ķ���� ����Ʈ ����
	ParameterNode* pTempStore;
	ParameterNode* pTempPtr;

	pTempPtr = pHead->next;
	while (pTempPtr != pTail)
	{
		pTempStore = pTempPtr;
		pTempPtr = pTempPtr->next;
		delete pTempStore;
	}
	delete pHead;
	delete pTail;

	//��������Ʈ ����
	CEventSprite* tempStore;
	CEventSprite* tempPtr = map; //map�� Head

	while (tempPtr != NULL)
	{
		tempStore = tempPtr;
		tempPtr = tempPtr->next;
		delete tempStore;
	}
}


//////////////////////////////////////////////////////////////////////////////////////////
// void FrameMove()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ���� ������ ó���� �ϴ� �Լ�
//		  CSceneŬ������ �����Լ��� �������̵��Ѵ�.
//		  ��������� �ݺ��ؼ� ����ǵ��� BgmRepeatCheck()�� ȣ���Ѵ�.
//		  ���� ��ɾ �� ����Ǿ����� executeFlag�� �����ѵ� ���� ��ɾ �о�´�.
//		  ���� ��ɾ �����Ű�� ������ �Ϸ�Ǿ������� executeFlag�� ��ȯ�Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CEvent::FrameMove()
{
	BgmRepeatCheck();
	if (!executeFlag) ReadScript(ptr->command);
	executeFlag = ExecuteScript();
}


//////////////////////////////////////////////////////////////////////////////////////////
// void Render()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ������ ó���� �ϴ� �Լ�
//		  CSceneŬ������ �����Լ��� �������̵��Ѵ�.
//		  drawSkip �÷��װ� ���� �ƴѰ�쿡 �������Ѵ�.
//		  �׷��� ��ü���� ��� CEventSprite�� ��ӹ����Ƿ� �������� �̿��Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CEvent::Render()
{
	if (!drawSkip)
	{
		//Draw
		CEventSprite* ptr = map;
		while (ptr != NULL)
		{
			ptr->Draw();
			ptr = ptr->next;
		}
	}
	else
	{
		drawSkip = false;
	}
}


//////////////////////////////////////////////////////////////////////////////////////////
// void ReadScript(char* line)
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ��ɾ� �� ������ �Ķ���� ������ �и��� �Ķ���� ����Ʈ�� �����ϴ� �Լ�
// ���� : char* line(��ɾ� ����Ʈ���� �޾ƿ�)
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CEvent::ReadScript(char* line)
{
	ParameterNode* pointer;
	ParameterNode* tempStore;

	//�ϴ� ���� �Ķ���� ���� ����(��ũ�� ����Ʈ ����)
	pointer = pHead->next;
	while (pointer != pTail)
	{
		tempStore = pointer;
		pointer = pointer->next;
		delete tempStore;
	}
	pHead->next = pTail;
	pTail->next = pTail;

	//��ū���� ���� ����
	ParameterNode* command = new ParameterNode;
	command->parameter = strtok(line, "#");
	pHead->next = command;
	command->next = pTail;
	pointer = command;

	char* chunk = "initialize"; //�̷��� ���ϸ� �Ʒ� �ݺ����� �����ϰ� �ǹǷ�
	while (chunk != NULL)
	{
		chunk = strtok(NULL, "#");
		if(chunk != NULL)
		{
			ParameterNode* param = new ParameterNode;
			param->parameter = chunk;
			param->next = pointer->next;
			pointer->next = param;
			pointer = param;
		}
	}
	ptr = ptr->next;
}


//////////////////////////////////////////////////////////////////////////////////////////
// char* PARAM(int index)
//////////////////////////////////////////////////////////////////////////////////////////
//
// ��  �� : ���ڷ� ���� �ε����� ��ũ��Ʈ ��ɾ� �Ķ���͸� ���ڿ��� ��ȯ�Ѵ�.
// ��  �� : int index(��ũ��Ʈ ��ɾ��� �Ķ���� �ε���)
// ���ϰ� : ��ũ��Ʈ ��ɾ��� �Ķ���� ���ڿ�
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

char* CEvent::PARAM(int index)
{
	ParameterNode* temp = pHead;
	for(int i = 0; i < index; i++)
	{
		temp = temp->next;
	}
	return temp->parameter;
}


//////////////////////////////////////////////////////////////////////////////////////////
// int NPARAM(int index)
//////////////////////////////////////////////////////////////////////////////////////////
//
// ��  �� : ���ڷ� ���� �ε����� ��ũ��Ʈ ��ɾ� �Ķ���͸� ���������� ��ȯ�Ѵ�.
// ��  �� : int index(��ũ��Ʈ ��ɾ��� �Ķ���� �ε���)
// ���ϰ� : ��ũ��Ʈ ��ɾ��� �Ķ���� ���ڿ��� ����ȭ ��Ų ����
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

int CEvent::NPARAM(int index)
{
	ParameterNode* temp = pHead;
	for(int i = 0; i < index; i++)
	{
		temp = temp->next;
	}
	return atoi(temp->parameter);
}


//////////////////////////////////////////////////////////////////////////////////////////
// bool ExecuteScript()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ��  �� : ���� ����Ű�� ��ũ��Ʈ ��ɾ �����Ų��.
// ���ϰ� : ���� ��ɾ �� ����Ŭ�� ������� false�� ��ȯ
//			�� ����Ŭ�� �� ������ �ʴ� ��� true�� ��ȯ
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////

bool CEvent::ExecuteScript()
{
	//����ؼ� �����ؾ��ϴ°��(��, �� ������ ������ �� ������ �ʴ� ���) true�� ��ȯ�Ѵ�.
	//���� ���Ǵ� ������ ��������(����ȭ)

	//�Ʒ������� �ּ�����
	// #��ũ��Ʈ ��ɾ� ����
	// ��ũ��Ʈ ��ɾ� ����


	// #LoadMap#ImageFilePath#ScrollX#ScrollY#
	// ���� �ҷ����� ��ũ��Ʈ ��ɾ�
	if (strcmp(PARAM(1), "LoadMap") == 0) 
	{
		map->SetMapTexture(PARAM(2));
		map->SetScrollX(NPARAM(3));
		map->SetScrollY(NPARAM(4));
		drawSkip = true; //������ ���� ����

		return false;
	}

	// #FadeIn#
	// ȭ���� ���̵��� ��Ű�� ��ũ��Ʈ ��ɾ�
	else if (strcmp(PARAM(1), "FadeIn") == 0)
	{
		FadeIn();
		drawSkip = true;

		return false;
	}

	// #FadeOut#
	// ȭ���� ���̵�ƿ� ��Ű�� ��ũ��Ʈ ��ɾ�
	else if (strcmp(PARAM(1), "FadeOut") == 0)
	{
		FadeOut();

		return false;
	}

	// #PlayMusic#MusicFilePath#
	// ��������� �����Ų��.
	else if (strcmp(PARAM(1), "PlayMusic") == 0)
	{
		dshow->Stop();
		dshow->Initialize(hWnd, PARAM(2));
		dshow->Run();

		return false;
	}

	// #StopMusic#
	// ��������� ������Ų��.
	else if (strcmp(PARAM(1), "StopMusic") == 0)
	{
		dshow->Stop();
		drawSkip = true;

		return false;
	}

	// #PlaySound#SoundFilePath#
	// ȿ������ �����Ų��.
	else if (strcmp(PARAM(1), "PlaySound") == 0)
	{
		PlaySound(PARAM(2), 0, SND_FILENAME | SND_ASYNC);

		return false;
	}

	// #Delay#MiliSec#
	// ���ڷ� �޾ƿ� ���ڸ�ŭ�� �и��ʸ�ŭ ����Ѵ�.
	else if (strcmp(PARAM(1), "Delay") == 0)
	{
		Sleep(NPARAM(2));

		return false;
	}

	// #FrameSkip#NumberOfFrames#
	// ���ڷ� �޾ƿ� ���ڸ�ŭ�� �����Ӹ�ŭ ����Ѵ�.
	else if (strcmp(PARAM(1), "FrameSkip") == 0)
	{
		static int count = 0;
		if (count < NPARAM(2))
		{
			count++;

			return true;
		}
		count = 0;

		return false;
	}

	// #Scroll#NewScrollX#NewScrollY#
	// ���� ��ũ�� ��Ų��.
	else if (strcmp(PARAM(1), "Scroll") == 0)
	{
		if (NPARAM(2) > map->GetScrollX()) map->SetScrollX(1);
		else if (NPARAM(2) < map->GetScrollX()) map->SetScrollX(-1);
		if (NPARAM(3) > map->GetScrollY()) map->SetScrollY(1);
		else if (NPARAM(3) < map->GetScrollY()) map->SetScrollY(-1);
			
		if (NPARAM(2) == map->GetScrollX() && NPARAM(3) == map->GetScrollY()) return false;

		return true;
	}

	// #ExitScript#NextScriptFilePath#KindOfScript(BSorES)#
	// ���� ��ũ��Ʈ�� �����Ű�� ���ڷ� �޾ƿ� ����� ��ũ��Ʈ�� �����Ų��.
	// BS�� Battle Script�� ���Ӹ�, ES�� Event Script�� ���Ӹ��̴�.
	else if (strcmp(PARAM(1), "ExitScript") == 0)
	{
		FadeIn();
		pData->SetScriptFilePath(PARAM(2));
		(strcmp(PARAM(3), "BS") == 0) ? sceneKind = NEXTBATTLE : sceneKind = NEXTEVENT;
		Sleep(1000);

		return false;
	}

	// #StaticImage#X#Y#Width#Height#NumberOfDurationFrame(60FramePer1Sec)#ImageFilePath#
	// �ִϸ��̼� �����ʴ� �̹����� �׷��ش�.
	else if (strcmp(PARAM(1), "StaticImage") == 0)
	{
		CEventStaticImage* temp = new CEventStaticImage(pd3dDevice, sprite, pFont);
		CEventSprite* ptr = staticImageHead;
		temp->SetX(NPARAM(2));
		temp->SetY(NPARAM(3));
		temp->SetWidth(NPARAM(4));
		temp->SetHeight(NPARAM(5));
		temp->SetDuration(NPARAM(6));
		temp->SetImage(PARAM(7));

		while(ptr->next != staticImageTail)
		{
			ptr = ptr->next;
		}
		ptr->next = temp;
		temp->next = staticImageTail;

		return false;
	}

	// #AnimImage#X#Y#FrameWidth#FrameHeight#NumberOfFrame#AnimSpeed#ImageFilePath#
	// �ִϸ��̼ǵǴ� �̹����� �׷��ش�.
	else if (strcmp(PARAM(1), "AnimImage") == 0)
	{
		CEventAnimImage* temp = new CEventAnimImage(pd3dDevice, sprite, pFont);
		CEventSprite* ptr = animImageHead;
		temp->SetX(NPARAM(2));
		temp->SetY(NPARAM(3));
		temp->SetFrameWidth(NPARAM(4));
		temp->SetFrameHeight(NPARAM(5));
		temp->SetNumFrame(NPARAM(6));
		temp->SetAnimSpeed(NPARAM(7));
		temp->SetImage(PARAM(8));

		while(ptr->next != animImageTail)
		{
			ptr = ptr->next;
		}
		ptr->next = temp;
		temp->next = animImageTail;

		return false;
	}

	// #Speak#Name#illustImageFilePath(NONE)#Dialog1#Dialog2(NONE)#Dialog3(NONE)#Dialog4(NONE)#Dialog5(NONE)#Dialog6(NONE)#
	// ĳ������ ��縦 ��½�Ų��.
	else if (strcmp(PARAM(1), "Speak") == 0)
	{
		if(!flag) //ù ���������� ���
		{
			cursor->SetCurState(NCURSOR);
			speakwnd->SetName(PARAM(2));
			if (strcmp(PARAM(3), "NONE") != 0) speakwnd->SetIllustTexture(PARAM(3));
			speakwnd->SetDialog(PARAM(4), PARAM(5), PARAM(6), PARAM(7), PARAM(8), PARAM(9));
			speakwnd->SetDoDraw(true);
			flag = true;

			return true;
		}
		else //�ι�° �������ʹ� ���ุ�Ѵ�
		{
			if (speakwnd->GetSpeakEnd())
			{
				speakwnd->SetSpeakEnd(false);
				flag = false;
				return false;
			}
			return true;
		}
	}

	// #Diverge#Number#Text1#Script1FilePath#KindOfScript1#Text2#Script2FilePath#KindOfScript2#......#
	// �б⸦ �߻���Ų��.
	else if (strcmp(PARAM(1), "Diverge") == 0)
	{
		if(!flag) //ù ���������� ���
		{
			cursor->SetCurState(SCURSOR);
			for (int i = 0; i < NPARAM(2); i++)
			{
				CDivergeWnd* temp = new CDivergeWnd(pd3dDevice, sprite, cursor, pFont, &sceneKind, pData);
				temp->SetX((screenWidth - 400) / 2); //400�� ����â�� �ʺ�
				temp->SetY((screenHeight - NPARAM(2) * 100) / 2 + i * 100);
				temp->SetText(PARAM(3 * i + 3)); //3, 6, 9,...��°�� �Ķ���͵�
				temp->SetScriptFilePath(PARAM(3 * i + 4)); //4, 7, 10,...��°�� �Ķ���͵�
				(strcmp(PARAM(3 * i + 5), "BS") == 0) ? temp->SetKindScript(NEXTBATTLE) : temp->SetKindScript(NEXTEVENT); //5, 8, 11,...��°�� �Ķ���͵�
				temp->next = divergeHead->next;
				divergeHead->next = temp;
			}
			flag = true;

			return true;
		}
		else //�ι�° �������ʹ� ���ุ�Ѵ�.
		{
			CDivergeWnd* ptr = divergeHead;
			while (ptr->next != divergeTail)
			{
				ptr = (CDivergeWnd*)ptr->next;

				if (ptr->MouseOnCheck() && cursor->LButtonClick())
				{
					FadeIn();
					pData->SetScriptFilePath(ptr->GetScriptFilePath());
					sceneKind = ptr->GetKindScript();
					Sleep(1000);
					return false;
				}	
			}
			return true;
		}
	}

	// #RegChar#ID(String)#X#Y#Direction(RIGHT, LEFT, UP, DOWN)#SpriteFilePath#
	// ĳ���͸� ��Ͻ�Ų��.
	else if(strcmp(PARAM(1), "RegChar") == 0)
	{
		CEventChar* dummy = new CEventChar(pd3dDevice, sprite, pFont);
		dummy->SetID(PARAM(2));
		dummy->SetX(NPARAM(3));
		dummy->SetY(NPARAM(4));
		dummy->SetDirection(PARAM(5));
		dummy->SetTexturePath(PARAM(6));
		dummy->next = charHead->next;
		charHead->next = dummy;

		drawSkip = true;

		return false;
	}

	// #RemoveChar#ID(String)#
	// ĳ���͸� �����Ѵ�.
	else if(strcmp(PARAM(1), "RemoveChar") == 0)
	{
		CEventChar* ptr = charHead;
		while (ptr->next != charTail)
		{
			ptr = (CEventChar*)ptr->next; //next�� CEventSprite���̹Ƿ� ����ȯ�� �ʿ��ϴ�
			
			if (strcmp(PARAM(2), ptr->GetID()) == 0) //ID�� �ش��ϴ� ĳ���͸� ã�Ƽ�
				ptr->SetDoDraw(false);
		}
		drawSkip = true; //������ ���� ����

		return false;
	}

	// #ChangeCharDir#Number#ID1(String)#Direction1(RIGHT, LEFT, UP, DOWN)#ID2(String)#Direction2(RIGHT, LEFT, UP, DOWN)#......#
	// ĳ���Ͱ� �ٶ󺸴� ������ �����Ѵ�.
	else if(strcmp(PARAM(1), "ChangeCharDir") == 0)
	{
		for (int i = 0; i < NPARAM(2); i++)
		{
			CEventChar* ptr = charHead;
			while (ptr->next != charTail)
			{
				ptr = (CEventChar*)ptr->next;

				if (strcmp(PARAM(2*i+3), ptr->GetID()) == 0) //ID�� �ش��ϴ� ĳ���͸� ã�Ƽ�
					ptr->SetDirection(PARAM(2*i+4));
			}
		}
		return false;
	}

	// #MoveChar#Number#ID1(String)#Direction1(RIGHT, LEFT, UP, DOWN)#ID2(String)#Direction2(RIGHT, LEFT, UP, DOWN)#......#
	// ĳ���͸� �̵���Ų��.
	else if(strcmp(PARAM(1), "MoveChar") == 0)
	{
		static int moveStep = 0;
		for (int i = 0; i < NPARAM(2); i++)
		{
			CEventChar* ptr = charHead;
			while (ptr->next != charTail)
			{
				ptr = (CEventChar*)ptr->next;
				if (strcmp(PARAM(2*i+3), ptr->GetID()) == 0) //ID�� �ش��ϴ� ĳ���͸� ã�Ƽ�
				{
					ptr->SetDirection(PARAM(2*i+4)); //��������
					ptr->Step();
				}
			}
		}
		moveStep++;
		if (moveStep == 4 * 10) //AnimSpeed : ĳ���� ��������Ʈ�� ���� ���������� �Ѿ�������� �������� ���⼭�� 10�� �����ִ�.
		{
			moveStep = 0;
			return false;
		}
		return true;
	}

	// #RelocateChar#ID(String)#X#Y#Direction(RIGHT, LEFT, UP, DOWN)
	// ĳ������ ��ġ�� �����Ѵ�.
	else if(strcmp(PARAM(1), "RelocateChar") == 0)
	{
		CEventChar* ptr = charHead;
		while (ptr->next != charTail)
		{
			ptr = (CEventChar*)ptr->next;
			if (strcmp(PARAM(2), ptr->GetID()) == 0) //ID�� �ش��ϴ� ĳ���͸� ã�Ƽ�
			{
				ptr->SetX(NPARAM(3));
				ptr->SetY(NPARAM(4));
				ptr->SetDirection(PARAM(5));
			}
		}
		drawSkip = true;

		return false;
	}

	// �� ��� ��쿡 �ش����� �ʴ°�� ����� �ۼ����� ���� ��ũ��Ʈ ����̹Ƿ� �����Ų��.
	// �̶� �߸��� ��ũ��Ʈ���ϸ�� �߸��� ������ �α׷� ����ϵ��� ���� �߰���Ű��.
	else
	{
		MessageBox(hWnd, "�߸��� ��ũ��Ʈ��ɾ�� ���� �����մϴ�.", "Error!", MB_OK);
		PostQuitMessage(0);

		return false;
	}
}