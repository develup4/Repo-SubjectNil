//
// ���ϸ� : CWindow.h
// 
// ���� : �����츦 ����, �����ϴ� Ŭ����. ��� Ŭ���� �� ���� ������ �����Ѵ�.
//		  CD3DŬ������ ��ü�� �����Ѵ�. 
//		  ������ ���� ����� �����ϴ� CSceneŬ������ ��ü�� �����Ѵ�.
//		  ���ӳ� �����͸� �����ϴ� CDataŬ������ ��ü�� �����Ѵ�.
//		  ������ ���ν��� �Լ����� ������â�� �̵��� ó���Ѵ�.
//		  FPS�� ����� ǥ���ϴ� ��ɵ� �Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//

#pragma once
#include <Windows.h>
#include "CD3D.h"
#include "CData.h"
#include "CScene.h"
#include "CTitle.h"
#include "CEvent.h"
#include "CBattle.h"
#include "CEnding.h"

struct WinArgsSetting
{
	LPCTSTR winName;
	UINT winStyle;
	DWORD winForm;

	const int screenWidth;
	const int screenHeight;
};

class CWindow
{

private:
	HWND hWnd;
	HINSTANCE hInst;
	LPCTSTR winName;
	CD3D* d3d; //DirectX �������� ���� ��ü

	const int screenWidth;
	const int screenHeight;

	//���ӳ��� ����
	CScene* scene; //���� ����� �����ϴ� ��ü�� �߻�Ŭ������ CScene�� ���ε��ϴ� Ŭ������ ���� ����� �����ȴ�.
	CData* pData;

protected:

public:
	CWindow(WinArgsSetting setting, HINSTANCE hInstance, LPSTR CmdLine, int CmdShow);
	~CWindow();

	static LRESULT CALLBACK CWindow::WndProc(HWND hWnd, UINT iMessage, WPARAM wParam, LPARAM lParam);
	void CheckFPS(HWND hWnd); //Frame Per Second�� ����Ͽ� ǥ���ϴ� �Լ�
	void Loop();

	//���ӳ��� ����
	void ChangeScene(int scene); //����� ���Ҷ� scene��ü�� ������Ű�� ���� ���ε���Ű�� �Լ�

	//ĸ��ȭ ����
	HWND GetHWnd() const;
	HINSTANCE GetHInst() const;
};