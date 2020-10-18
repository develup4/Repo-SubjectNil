//
// ���ϸ� : CBattleChar.cpp
// 
// ���� : CBattleChar Ŭ������ ����
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//

#include "CBattleChar.h"


/////////////////////////////////////////////////////////////////////////////////////////
// CBattleChar()
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CBattleChar Ŭ������ ������
//		  ĳ������ ����κ��� �ʱ�ȭ�ϴ� �Լ�
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

CBattleChar::CBattleChar(LPDIRECT3DDEVICE9 pd3dDevice, LPD3DXSPRITE sprite, CBattleMap* bMap, int x, int y, int movableCell)
{
	this->pd3dDevice = pd3dDevice;
	this->sprite = sprite;
	this->bMap = bMap;
	this->x = x;
	this->y = y;
	this->movableCell = movableCell;
	tp = 0;
	step = 0;
	direction = DOWN;
	state = FILLTP;
	D3DXCreateTextureFromFile(pd3dDevice, "Texture\\Common\\TPGage.jpg", &tTPGage);
	D3DXCreateTextureFromFile(pd3dDevice, "Texture\\Common\\TPG.jpg", &tTPG);

	//�迭 �ʱ�ȭ
	moveOrder = new int[movableCell];
	for (int i = 0; i < movableCell; i++)
	{
		moveOrder[i] = -1;
	}
	movableMap = new int*[bMap->GetMapY()];
	for (int i = 0; i < bMap->GetMapY() ; i++)
	{
		movableMap[i] = new int[bMap->GetMapX()];
	}
	for(int i = 0; i < bMap->GetMapX(); i++)
	{
		for(int j = 0; j < bMap->GetMapY(); j++)
		{
			movableMap[j][i] = 0;
		}
	}
	ptr = 0;
}


/////////////////////////////////////////////////////////////////////////////////////////
// ~CBattleChar()
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CBattleChar Ŭ������ �Ҹ���
//		  ĳ������ ����κ��� �����ϴ� �Լ�
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

CBattleChar::~CBattleChar()
{
	for (int i = 0; i < bMap->GetMapY(); i++)
	{
		delete[] movableMap[i];
	}
	delete[] movableMap;
	delete moveOrder;
	tTPGage->Release();
	tTPG->Release();
}


/////////////////////////////////////////////////////////////////////////////////////////
// void MoveCharacter(int objX, int objY, int n)
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : �ִܰ�θ� ����ϴ� �Լ�. ���ȣ���� �̿��Ѵ�.
//		  ��ǥ�������κ��� FindDistance�Լ����� ���� �����븦 �i��
//		  ����������� ���ƿ��� �� ����� ���� �ִܰ���̴�.
//		  ���⼭�� �� �̵����� ������ moveOrder�� �ݴ�� �����ذ���.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

void CBattleChar::MoveCharacter(int objX, int objY, int n)
{
	if (x == objX && y == objY) //��� �������� ���ƿ���
	{
		return;
	}

	n++;

	if (objY - 1 >= 0 && movableMap[objY-1][objX] == n)
	{
		MoveCharacter(objX, objY - 1, n);
		moveOrder[ptr] = DOWN;
		ptr++;
	}
	else if (objY + 1 < bMap->GetMapY() && movableMap[objY+1][objX] == n)
	{
		MoveCharacter(objX, objY + 1, n);
		moveOrder[ptr] = UP;
		ptr++;
	}
	else if (objX - 1 >= 0 && movableMap[objY][objX-1] == n)
	{
		MoveCharacter(objX - 1, objY, n);
		moveOrder[ptr] = RIGHT;
		ptr++;
	}
	else if (objX + 1 < bMap->GetMapX() && movableMap[objY][objX+1] == n)
	{
		MoveCharacter(objX + 1, objY, n);
		moveOrder[ptr] = LEFT;
		ptr++;
	}
}


/////////////////////////////////////////////////////////////////////////////////////////
// bool Step()
/////////////////////////////////////////////////////////////////////////////////////////
//
// ��  �� : ĳ���� �̵��� �ӵ��� �����ذ��� �̵��ϴ� �Լ��̴�.
// ���ϰ� : �� ���� �� �̵��� ��� true��, �ƴѰ�� false�� �����Ѵ�.
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

bool CBattleChar::Step()
{
	step++;
	if (step == 4 * 10) //10�� �ִϸ��̼� ���ǵ�� ������ �������� �ݺ��Ǵ� Ƚ���� �ǹ��Ѵ�(��, �������� ������ �ִϸ��̼�)
	{
		switch(direction)
		{
		case DOWN:
			{
				y++;
				break;
			}
		case LEFT:
			{
				x--;
				break;
			}
		case RIGHT:
			{
				x++;
				break;
			}
		case UP:
			{
				y--;
				break;
			}
		}
		step = 0;
		return true;
	}
	return false;
}


/////////////////////////////////////////////////////////////////////////////////////////
// int GetCharX(), int GetCharY()
/////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ĳ������ ��ǥ�� ��ȯ�ϴ� �Լ�
//
// 2010.01.20 ��������
/////////////////////////////////////////////////////////////////////////////////////////

int CBattleChar::GetCharX()
{
	return x;
}

int CBattleChar::GetCharY()
{
	return y;
}