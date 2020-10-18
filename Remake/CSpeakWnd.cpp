//
// ���ϸ� : CSpeakWnd.cpp
// 
// ���� : ��ȭâ Ŭ������ ����
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//


#include "CSpeakWnd.h"
#pragma warning(disable : 4996)
#define DIALOGFIRSTLINE_Y 590


//////////////////////////////////////////////////////////////////////////////////////////////////////
// CSpeakWnd(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CCursor* cursor, LPD3DXFONT pFont)
//////////////////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CSpeakWnd Ŭ������ ������.
//		  �ʱⵥ���͸� �����Ѵ�.
//		  �ʿ��� �ؽ��ĸ� ����Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////////////////

CSpeakWnd::CSpeakWnd(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CCursor* cursor, LPD3DXFONT pFont) : CEventSprite(pd3dDevice, sprite, pFont)
{
	this->cursor = cursor;
	this->doDraw = false;
	this->speakEnd = false;
	step = FADEIN;
	D3DXCreateTextureFromFile(pd3dDevice, "Texture\\Common\\SpeakWnd.tga", &tSpeakWnd);
	D3DXCreateTextureFromFile(pd3dDevice, "Texture\\Common\\Complete.tga", &tComplete);
	D3DXCreateTextureFromFile(pd3dDevice, "Texture\\Common\\NoneIllust.tga", &tIllust);

	//�ʱ�ȭ
	index = 0;
	ptr = 0;
	speakWndAlpha = 0;
	illustAlpha = 0;
	completeStep = 0;
}


///////////////////////////////////////////////////////////////////////////////////////
// ~CSpeakWnd()
///////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CSpeakWnd Ŭ������ �Ҹ���.
//		  ����� �ؽ��ĸ� �����Ѵ�.
//
// 2010.01.03 ��������
///////////////////////////////////////////////////////////////////////////////////////

CSpeakWnd::~CSpeakWnd()
{
	tSpeakWnd->Release();
	tComplete->Release();
	tIllust->Release();
}


///////////////////////////////////////////////////////////////////////////////////////
// void Draw()
///////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ��ü�� �������ϴ� �Լ�.
//		  �� �ܰ踶�� ������ ȿ���� �ش�.
//
// 2010.01.20 ��������
///////////////////////////////////////////////////////////////////////////////////////

void CSpeakWnd::Draw()
{
	if (doDraw)
	{
		int lineY = DIALOGFIRSTLINE_Y; //ù �������� Y��

		sprite->Begin(D3DXSPRITE_ALPHABLEND);

		//�Ϸ���Ʈ ������
		RECT rcIllust = {0, 0, 480, 640};
		D3DXVECTOR3 posIllust(0, 128, 0);
		sprite->Draw(tIllust, &rcIllust, NULL, &posIllust, D3DCOLOR_RGBA(255, 255, 255, illustAlpha));

		//��ȭâ ������
		RECT rcSpeak = {0, 0, 982, 200};
		D3DXVECTOR3 posSpeak(21, 555, 0);
		sprite->Draw(tSpeakWnd, &rcSpeak, NULL, &posSpeak, D3DCOLOR_RGBA(255, 255, 255, speakWndAlpha));

		sprite->End();

		//�̸� ���
		TextRender(name, 910, 565, 1024, 620, speakWndAlpha);

		if (step == FADEIN)
		{
			//���̵��� ȿ��
			speakWndAlpha += 8;
			if (speakWndAlpha > 128) illustAlpha += 8;
			if (speakWndAlpha > 255) speakWndAlpha = 255;
			if (illustAlpha > 255) illustAlpha = 255;

			//���̵��� ȿ���� ������
			if (speakWndAlpha == 255 && illustAlpha == 255) step = SPEAK;
		} 
		else if (step == SPEAK)
		{
			//��縦 �ѱ��ھ� ����ϴ� ȿ��
			
			//�ε������� �� ������ �״�� �� ����
			for (int i = 0; i < index; i++)
			{
				if (strcmp(dialog[i], "NONE") != 0) //�ش� ���ο� ��簡 ������ ��ũ��Ʈ�� NONE�� ǥ�õǾ��ִ�
					TextRender(dialog[i], 40, lineY + i * 25, 980, 740, 255);
			}

			//�ε��� ������ �ѱ��ھ� ���
			if (strcmp(dialog[index], "NONE") != 0)
			{
				char temp[256];

				strncpy(temp, dialog[index], ptr);
				temp[ptr+1] = '\0';
				TextRender(temp, 40, lineY + index * 25, 980, 740, 255);
				ptr++;

				//���� ó���ϴ� ������ �� ������
				if (ptr == strlen(dialog[index]))
				{
					ptr = 0;
					index++;

					//�̰� ������ �����̶�� ����
					if (index == 6 || strcmp(dialog[index], "NONE") == 0)
					{
						index = 0;
						step = SPEAKEND;
					}
				}
			}

			//������
			if (cursor->LButtonClick() || cursor->RButtonClick())
				Sleep(5); //Ŭ���ϰ�������� ���� ������ ��������
			else Sleep(50);

		}
		else if (step == SPEAKEND)
		{
			//��� ���
			for (int i = 0; i < 6; i++)
			{
				if (strcmp(dialog[i], "NONE") != 0)
					TextRender(dialog[i], 40, lineY + i * 25, 980, 740, 255);
			}

			//���Ϸ�ǥ�� ������
			RECT rect = {16, 0, 32, 16};
			if ((completeStep / 10) % 2)
			{
				rect.left -= 16;
				rect.right -= 16;
			}

			sprite->Begin(D3DXSPRITE_ALPHABLEND);
			
			D3DXVECTOR3 pos(960, 715, 0);
			sprite->Draw(tComplete, &rect, NULL, &pos, D3DCOLOR_RGBA(255, 255, 255, 255));
			sprite->End();

			completeStep++;

			if(cursor->LButtonClick() || completeStep > 600) //���콺 ���ʹ�ư�� �������ų� 600�������� ������ ��
			{
				completeStep = 0;
				step = FADEOUT;
				PlaySound("Sound\\TalkOk.wav", 0, SND_FILENAME | SND_ASYNC);
			}
		}
		else
		{
			cursor->SetCurState(NODRAW);

			//���̵�ƿ� ȿ��
			illustAlpha -= 8;
			if (illustAlpha < 128) speakWndAlpha -= 8;
			if (illustAlpha < 0) illustAlpha = 0;
			if (speakWndAlpha < 0) speakWndAlpha = 0;

			//��� ���
			for (int i = 0; i < 6; i++)
			{
				if (strcmp(dialog[i], "NONE") != 0)
					TextRender(dialog[i], 40, lineY + i * 25, 980, 740, illustAlpha);
			}

			//���̵�ƿ� ȿ���� ������
			if (speakWndAlpha == 0 && illustAlpha == 0)
			{
				speakEnd = true;

				//�ʱ�ȭ
				tIllust->Release();
				D3DXCreateTextureFromFile(pd3dDevice, "Texture\\Common\\NoneIllust.tga", &tIllust);
				doDraw = false;
				step = FADEIN;

				strcpy(name, "");
				for (int i = 0; i < 6; i++)
				{
					strcpy(dialog[i], "");
				}
			}
		}
	}
}


///////////////////////////////////////////////////////////////////////////////////////
// void SetIllustTexture(char* textureFilePath)
///////////////////////////////////////////////////////////////////////////////////////
//
// ���� : �Ϸ���Ʈ �̹����� ����ϴ� �Լ�
//		  �κл�������� �̷��ǰ�?
// ���� : char* textureFilePath(�Ϸ���Ʈ �̹����� ���� ��� ���ڿ�)
//
// 2010.01.03 ��������
///////////////////////////////////////////////////////////////////////////////////////

void CSpeakWnd::SetIllustTexture(char* textureFilePath)
{
	tIllust->Release();
	D3DXCreateTextureFromFile(pd3dDevice, textureFilePath, &tIllust);
}


///////////////////////////////////////////////////////////////////////////////////////
// void SetName(char* name)
///////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ��ȭ�ϴ� ĳ������ �̸��� ����ϴ� �Լ�
// ���� : char* name(ĳ������ �̸� ���ڿ�)
//
// 2010.01.03 ��������
///////////////////////////////////////////////////////////////////////////////////////

void CSpeakWnd::SetName(char* name)
{
	strcpy(this->name, name);
}


//////////////////////////////////////////////////////////////////////////////////////////////////////////////
// void SetDialog(char* dialog1, char* dialog2, char* dialog3, char* dialog4, char* dialog5, char* dialog6)
//////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ��縦 ����ϴ� �Լ�
// ���� : ���� 6���α��� �����ϸ� ���ڵ��� �ε����� ������ �����ȴ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////////////////////////

void CSpeakWnd::SetDialog(char* dialog1, char* dialog2, char* dialog3, char* dialog4, char* dialog5, char* dialog6)
{
	strcpy(this->dialog[0], dialog1);
	strcpy(this->dialog[1], dialog2);
	strcpy(this->dialog[2], dialog3);
	strcpy(this->dialog[3], dialog4);
	strcpy(this->dialog[4], dialog5);
	strcpy(this->dialog[5], dialog6);
}


///////////////////////////////////////////////////////////////////////////////////////
// void SetSpeakEnd(bool end)
///////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ��簡 ���������� �����ϴ� speakEnd������ �����ϴ� �Լ�
//		  ��簡 �� ������ CEvent Ŭ�������� ���� ��ɾ �����Ҷ�,
//		  �ٽ� ���� false�� ������������ ����Ѵ�.
// ���� : bool end
//
// 2010.01.03 ��������
///////////////////////////////////////////////////////////////////////////////////////

void CSpeakWnd::SetSpeakEnd(bool end)
{
	speakEnd = end;
}


///////////////////////////////////////////////////////////////////////////////////////
// bool GetSpeakEnd()
///////////////////////////////////////////////////////////////////////////////////////
//
// ��  �� : ��簡 ���������� �����ϴ� speakEnd������ ������ �Լ�
//			�� �Լ��� ���� CEvent Ŭ������ ����Ѵ�.
// ���ϰ� : bool speakEnd
//
// 2010.01.03 ��������
///////////////////////////////////////////////////////////////////////////////////////

bool CSpeakWnd::GetSpeakEnd()
{
	return speakEnd;
}