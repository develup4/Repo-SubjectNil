//
// ���ϸ� : CPlayer.cpp
// 
// ���� : CPlayer Ŭ������ ����
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//


#include "CPlayer.h"
#define MOVABLECOUNT 200 //����� �ƴ����� ���߿� �÷��̾��� �ɷ�ġ �� �ϳ��� �ǹǷ� ��������� �ȴ�. HP ��� ���� �ɷ�ġ�� ���鶧 �������.


/////////////////////////////////////////////////////////////////////////////////////////
// CPlayer()
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CPlayer Ŭ������ ������
//		  �÷��̾�� �����͸� �ʱ�ȭ��Ų��.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

CPlayer::CPlayer(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CBattleMap* bMap, int x, int y, int movableCell, CCursor* cursor) : CBattleChar(pd3dDevice, sprite, bMap, x, y, movableCell)
{
	this->cursor = cursor;
	D3DXCreateTextureFromFile(pd3dDevice, "Texture\\Common\\BlueCell.jpg", &tBlueCell);
	D3DXCreateTextureFromFile(pd3dDevice, "Texture\\BattleChar\\Syu.tga", &tChar);
}


/////////////////////////////////////////////////////////////////////////////////////////
// ~CPlayer()
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CPlayer Ŭ������ �Ҹ���
//		  �÷��̾�� �����͸� ������Ų��.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

CPlayer::~CPlayer()
{
	tBlueCell->Release();
	tChar->Release();
}


/////////////////////////////////////////////////////////////////////////////////////////
// void TakeAction()
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ��ü�� �ൿ�Լ��̴�.
//		  �������� ������ ��ü�� ��� ó���� �� �Լ����� �Ѵ�.
//		  �� ��ü������ �÷��̾��� �̵� ���ݿ� ���� �κ��� ó���Ѵ�.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

void CPlayer::TakeAction()
{
	switch(state)
	{
	case FILLTP:
		{
			tp++;
			if (tp == MOVABLECOUNT)
			{
				FindDistance(x, y, movableCell);
				state = MOVABLE;
			}
			break;
		}
	case MOVABLE:
		{
			//���� ���콺 Ŀ���� ����Ű�� ����ǥ�� ����
			int i = (cursor->GetCursorPoint().x + bMap->GetSX()) / 32;
			int j = (cursor->GetCursorPoint().y + bMap->GetSY()) / 32;

			//���ó��
			if (i < 0) i = 0;
			if (j < 0) j = 0;
			if (i >= bMap->GetMapX()) i = bMap->GetMapX() - 1;
			if (j >= bMap->GetMapY()) j = bMap->GetMapY() - 1;
			if (movableMap[j][i] > 0) //�� ���� �̵������� ���̶��
			{
				cursor->SetCurState(SCURSOR);
				if (cursor->LButtonClick())
				{
					//���콺 ���ʹ�ư�� �������� �� ���� �̵���ǥ�������� �Ͽ� �ִܰ�� ���
					MoveCharacter(i, j, movableMap[j][i]);

					//�ʱ�ȭ
					ptr = 0;
					for(int i = 0; i < bMap->GetMapX(); i++)
					{
						for(int j = 0; j < bMap->GetMapY(); j++)
						{
							movableMap[j][i] = 0;
						}
					}
					state = MOVE;
				}
			}
			else
			{
				cursor->SetCurState(NCURSOR);
			}
			break;
		}
	case MOVE:
		{
			cursor->SetCurState(NCURSOR);
			direction = moveOrder[ptr]; //moveOrder���� �ִܰ���� �� �̵������� ����ȴ�
			if (Step()) //Step�� 1���̵��� ������ true�� ��ȯ�Ѵ�
			{
				ptr++;
			}
			
			if (moveOrder[ptr] == -1) //���������� �����̸�
			{
				//�ʱ�ȭ
				for (int i = 0; i < movableCell; i++)
				{
					moveOrder[i] = -1;
				}
				ptr = 0;
				tp = 0;
				state = FILLTP;
			}
			break;
		}
	}
}


/////////////////////////////////////////////////////////////////////////////////////////
// void Draw()
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ��ü�� ������ �Լ��̴�.
//		  ��ü�� �ؽ��� �̹����� �������Ѵ�.
//		  ĳ������ �̵� �ִϸ��̼��� �������Ѵ�.
//		  �̵����ɹ����� Ǫ�� �̹����� ���ٿ� �������ϰ� �������Ѵ�.
//		  TP �������� �������Ѵ�.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

void CPlayer::Draw()
{
	//��ü �ʿ����� ��ǥ���� ��ũ�� ��ǥ�� ���� ���� ��ũ�������� ��ǥ�� ���� �� �ִ�.

	//�̹����� ������
	if (state == MOVABLE)
	{
		for(int i = 0; i < bMap->GetMapX(); i++)
		{
			for(int j = 0; j < bMap->GetMapY(); j++)
			{
				if (movableMap[j][i] > 0)
				{
					sprite->Begin(D3DXSPRITE_ALPHABLEND);
					RECT rect = {0, 0, 32, 32};
					D3DXVECTOR3 pos((float)i * 32 - bMap->GetSX(), (float)j * 32 - bMap->GetSY(), 0.0f);
					sprite->Draw(tBlueCell, &rect, NULL, &pos, D3DCOLOR_RGBA(255, 255, 255, 100));
					sprite->End();
				}
			}
		}
	}

	//ĳ���� ������
	sprite->Begin(D3DXSPRITE_ALPHABLEND);
	RECT rc = {(step / 10) * 32, direction * 48, (step / 10) * 32 + 32, direction * 48 + 48};
	D3DXVECTOR3 pos;
	pos.z = 0.0f;
	switch(direction)
	{
	case DOWN:
		{
			pos.x = (float)x * 32 - bMap->GetSX();
			pos.y = (float)y * 32 + ((step / 10) * 8) - 20 - bMap->GetSY(); //20 : ĳ���Ͱ� ���η� �� ��� ����
			break;
		}
	case LEFT:
		{
			pos.x = (float)x * 32 - ((step / 10) * 8) - bMap->GetSX();
			pos.y = (float)y * 32 - 20 - bMap->GetSY();
			break;
		}
	case RIGHT:
		{
			pos.x = (float)x * 32 + ((step / 10) * 8) - bMap->GetSX();
			pos.y = (float)y * 32 - 20 - bMap->GetSY();
			break;
		}
	case UP:
		{
			pos.x = (float)x * 32 - bMap->GetSX();
			pos.y = (float)y * 32 - ((step / 10) * 8) - 20 - bMap->GetSY();
			break;
		}
	}
	sprite->Draw(tChar, &rc, NULL, &pos, D3DCOLOR_RGBA(255, 255, 255, 255));
	sprite->End();

	//TP������ ������
	sprite->Begin(D3DXSPRITE_ALPHABLEND);
	RECT rctp = {0, 0, 52, 5};
	pos.x -= 10;
	pos.y -= 10;
	sprite->Draw(tTPGage, &rctp, NULL, &pos, D3DCOLOR_RGBA(255, 255, 255, 255));
	RECT rct = {0, 0, (int)(50 * ((float)tp / 200.0f)), 3};
	pos.x++;
	pos.y++;
	sprite->Draw(tTPG, &rct, NULL, &pos, D3DCOLOR_RGBA(255, 255, 255, 255));
	sprite->End();
}


/////////////////////////////////////////////////////////////////////////////////////////
// void FindDistance(int x, int y, int n)
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : �̵����ɹ����� ����ϴ� �Լ�. ���ȣ���� �̿��Ѵ�.
//		  ����ϸ鼭 ���ÿ� �������迭�� ���븦 ���� ���߿� �ִܰ�� ��꿡 �̿��ϰ��Ѵ�.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

void CPlayer::FindDistance(int x, int y, int n)
{
	movableMap[y][x] = n; //���븦 �����

	if (n == 0)
	{
		return;
	}

	if (y - 1 >= 0 && bMap->GetMovable(x, y - 1) && movableMap[y-1][x] < n - 1) //���� �̵��� �� �ִ�
	{
		FindDistance(x, y - 1, n - 1); //�������� �Ǵ�
	}
	if (y + 1 < bMap->GetMapY() && bMap->GetMovable(x, y + 1) && movableMap[y+1][x] < n - 1) //�Ʒ��� �̵��� �� �ִ�
	{
		FindDistance(x, y + 1, n - 1); //�������� �Ǵ�
	}
	if (x + 1 < bMap->GetMapX() && bMap->GetMovable(x + 1, y) && movableMap[y][x+1] < n - 1) //���������� �̵��� �� �ִ�
	{
		FindDistance(x + 1, y, n - 1); //�������� �Ǵ�
	}
	if (x - 1 >= 0 && bMap->GetMovable(x - 1, y) && movableMap[y][x-1] < n - 1) //�������� �̵��� �� �ִ�
	{
		FindDistance(x - 1, y, n - 1); //�������� �Ǵ�
	}
}
