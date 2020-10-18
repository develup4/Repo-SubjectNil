#include "CEnemy.h"
#define MOVABLECOUNT 300 //����� �ƴ����� ���߿� �÷��̾��� �ɷ�ġ �� �ϳ��� �ǹǷ� ��������� �ȴ�. HP ��� ���� �ɷ�ġ�� ���鶧 �������.



//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// CEnemy(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CBattleMap* bMap, int x, int y, int movableCell, CPlayer* player, bool chase)
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CEnemy Ŭ������ ������
//		  �� ĳ���͸��� �����͸� ��Ͻ�Ų��.
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

CEnemy::CEnemy(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CBattleMap* bMap, int x, int y, int movableCell, CPlayer* player, bool chase) : CBattleChar(pd3dDevice, sprite, bMap, x, y, movableCell)
{
	this->player = player;
	this->chase = chase;
	chase ? D3DXCreateTextureFromFile(pd3dDevice, "Texture\\BattleChar\\StrangeGuy.tga", &tChar) : D3DXCreateTextureFromFile(pd3dDevice, "Texture\\BattleChar\\Riel.tga", &tChar);
	distance = new int*[bMap->GetMapY()];
	for (int i = 0; i < bMap->GetMapY() ; i++)
	{
		distance[i] = new int[bMap->GetMapX()];
	}
	for(int i = 0; i < bMap->GetMapX(); i++)
	{
		for(int j = 0; j < bMap->GetMapY(); j++)
		{
			distance[j][i] = 0;
		}
	}
}


/////////////////////////////////////////////////////////////////////////////////////////
// ~CEnemy()
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CEnemy Ŭ������ �Ҹ���
//		  �� ĳ���͸��� �����͸� ������Ų��.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

CEnemy::~CEnemy()
{
	for (int i = 0; i < bMap->GetMapY(); i++)
	{
		delete[] distance[i];
	}
	delete[] distance;
}


/////////////////////////////////////////////////////////////////////////////////////////
// void TakeAction()
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ��ü�� �ൿ�Լ��̴�.
//		  �������� ������ ��ü�� ��� ó���� �� �Լ����� �Ѵ�.
//		  �� ��ü������ �� ĳ������ �̵� ���ݿ� ���� �κ��� ó���Ѵ�.
//		  �� ĳ������ �ΰ������� �� �Լ����� �ô´�.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

void CEnemy::TakeAction()
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
			int objX = 0;
			int objY = 0;
			if (chase)
			{
				//�߰��ڶ�� �÷��̾�� ���� ����� ������ ��ǥ�� �����ڶ�� ���� �� ������ ��ǥ�� �Ѵ�.
				//���� �̵������� ����� �÷��̾��� ��ġ������ �Ÿ��� �����Ѵ�.

				int min = 999999;

				for(int i = 0; i < bMap->GetMapX(); i++)
				{
					for(int j = 0; j < bMap->GetMapY(); j++)
					{
						if (distance[j][i] > 0 && distance[j][i] < min)
						{
							min = distance[j][i];
							objX = i;
							objY = j;
						}
					}
				}
			}
			else
			{
				int max = 0;

				for(int i = 0; i < bMap->GetMapX(); i++)
				{
					for(int j = 0; j < bMap->GetMapY(); j++)
					{
						if (distance[j][i] > max)
						{
							max = distance[j][i];
							objX = i;
							objY = j;
						}
					}
				}
			}

			//�ִܰ�� ���ϱ�
			MoveCharacter(objX, objY, movableMap[objY][objX]);

			//�ʱ�ȭ
			ptr = 0;
			for(int i = 0; i < bMap->GetMapX(); i++)
			{
				for(int j = 0; j < bMap->GetMapY(); j++)
				{
					movableMap[j][i] = 0;
					distance[j][i] = 0;
				}
			}
			state = MOVE;
			break;
		}
	case MOVE:
		{
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
//		  TP �������� �������Ѵ�.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

void CEnemy::Draw()
{
	//��ü �ʿ����� ��ǥ���� ��ũ�� ��ǥ�� ���� ���� ��ũ�������� ��ǥ�� ���� �� �ִ�.

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
	RECT rct = {0, 0, (int)(50 * ((float)tp / 300.0f)), 3};
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
//		  �Ÿ��� ����ϴ� �������迭�� �� ���� �÷��̾��� �Ÿ��� ������ ����Ѵ�.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

void CEnemy::FindDistance(int x, int y, int n)
{
	movableMap[y][x] = n; //���븦 �����
	distance[y][x] = (x - player->GetCharX()) * (x - player->GetCharX()) + (y - player->GetCharY()) * (y - player->GetCharY());

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