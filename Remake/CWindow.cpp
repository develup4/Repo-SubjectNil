//
// ���ϸ� : CWindow.cpp
// 
// ���� : CWindow Ŭ���� ����
//
// �輺��(lonkth@naver.com)
// 2010.01.20 ���� ����
//

#include "CWindow.h"
#pragma warning(disable : 4995)


//////////////////////////////////////////////////////////////////////////////////////////
// CWindow(WinArgsSetting setting, HINSTANCE hInstance, LPSTR CmdLine, int CmdShow)
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CWindow Ŭ������ ������.
//		  �����츦 �����Ѵ�.
//		  d3d��ü�� scene��ü, pData��ü�� �ʱ�ȭ��Ų��.
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////

CWindow::CWindow(WinArgsSetting setting, HINSTANCE hInstance, LPSTR CmdLine, int CmdShow) : screenWidth(setting.screenWidth), screenHeight(setting.screenHeight)
{
	//������ ���
	WNDCLASS WndClass;
	WndClass.cbClsExtra = 0;
	WndClass.cbWndExtra = 0;
	WndClass.hbrBackground = (HBRUSH) GetStockObject(WHITE_BRUSH);
	WndClass.hCursor = LoadCursor(NULL, IDC_ARROW);
	WndClass.hIcon = LoadIcon(NULL, IDI_APPLICATION);
	WndClass.hInstance = hInstance;
	WndClass.lpfnWndProc = CWindow::WndProc;
	WndClass.lpszClassName = setting.winName;
	WndClass.lpszMenuName = NULL;
	WndClass.style = setting.winStyle;
	RegisterClass(&WndClass);

	hWnd = CreateWindow(setting.winName, setting.winName, setting.winForm,
		(GetSystemMetrics(SM_CXSCREEN) - screenWidth) / 2, (GetSystemMetrics(SM_CYSCREEN) - screenHeight) / 2, //�������� �ʱ���ġ�� ȭ���߾�����
		setting.screenWidth, setting.screenHeight, NULL, (HMENU) NULL, hInstance, NULL);

	hInst = hInstance;
	winName = setting.winName;

	ShowWindow(hWnd, CmdShow);
	UpdateWindow(hWnd);
	ShowCursor(false); //������ �⺻ Ŀ���� ����� �� ��ġ�� Ŀ���̹����� ���

	//�ʱ�ȭ
	d3d = new CD3D(hWnd);
	pData = new CData("Script\\ES\\e001.es"); //������ ��ũ��Ʈ

	SceneParamaterBundle sceneParam = {
		hWnd, hInst, //������ ���� �ڵ�
		d3d->GetD3DPointer(), d3d->GetD3DDevicePointer(), d3d->GetFontPointer(), //D3D ���� ������
		screenWidth, screenHeight, //��ũ���� ũ��
		TITLE, //���� ����� ����
		pData //�����͸� ������ ��ü�� ������
	};

	scene = new CTitle(sceneParam); //������ ����� Ÿ��Ʋ ȭ���̹Ƿ� scene��ü�� CTitleŬ������ ���ε���Ų��.
}


//////////////////////////////////////////////////////////////////////////////////////////
// ~CWindow()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : CWindow Ŭ������ �Ҹ���.
//		  �����츦 �ı��Ѵ�.
//		  d3d��ü�� scene��ü, pData��ü�� ������Ų��.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

CWindow::~CWindow()
{
	delete d3d;
	delete scene;
	delete pData;
	UnregisterClass(winName, hInst);
}


//////////////////////////////////////////////////////////////////////////////////////////
// void CheckFPS(HWND hWnd)
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : FramePerSecond�� �����Ѵ�.
//		  ������ FPS�� ǥ���Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CWindow::CheckFPS(HWND hWnd)
{
	static DWORD tick = 0;
	static DWORD fps = 0;
	TCHAR temp[32];

	fps++;
	if (GetTickCount() - tick > 1000)
	{
		tick = GetTickCount();
		wsprintf(temp, "FPS : %d", fps);
		SetWindowText(hWnd, temp);
		fps = 0;
		return;
	}
}


//////////////////////////////////////////////////////////////////////////////////////////
// void Loop()
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ���ӷ����Լ�
//		  ������ ����Ǵ��� ����ؼ� �����Ѵ�.
//		  �� �������� FrameMove()���� �������� ������ ���ε����͵��� ó���Ѵ�.
//		  �� �������� Render()���� ���� ���������� ó���Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CWindow::Loop()
{
	int prevScene = TITLE; //��� ��ȭ�� �����ϱ� ���Ͽ� ���� ����� ������ �����ϴ� ������, ù ����� Ÿ��Ʋȭ���̹Ƿ� TITLE�� �ʱ�ȭ
	MSG Message;
	ZeroMemory(&Message, sizeof(Message));

	//������ ����
	while(Message.message != WM_QUIT)
	{
		if (PeekMessage(&Message, NULL, 0, 0, PM_REMOVE))
		{
			TranslateMessage(&Message); 
			DispatchMessage(&Message);
		}
		else //�޽����� ������ ó��
		{
			CheckFPS(hWnd);

			if (hWnd == GetFocus()) //���� �����쿡 ��Ŀ���� �Ǿ��ִٸ�
				scene->FrameMove(); //���ε�����ó��

			if (prevScene != scene->GetSceneKind()) //��麯ȭ�� �־��ٸ�
				ChangeScene(scene->GetSceneKind()); //���� ������� scene��ü�� ����

			prevScene = scene->GetSceneKind(); //���� ����� ���� �������� ���� ����� �ǹǷ� ����

			d3d->GetD3DDevicePointer()->Clear(0, NULL, D3DCLEAR_TARGET, D3DCOLOR_XRGB(0, 0, 0), 1.0f, 0);
			if (SUCCEEDED(d3d->GetD3DDevicePointer()->BeginScene()))
			{
				scene->Render(); //������
				scene->GetCursor()->DrawCursor(); //���콺 Ŀ�� ������
				d3d->GetD3DDevicePointer()->EndScene();
			}
			d3d->GetD3DDevicePointer()->Present(NULL, NULL, NULL, NULL);
		}
	}
}


//////////////////////////////////////////////////////////////////////////////////////////
// void ChangeScene(int sceneKind)
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ����� �ٲٴ� �Լ�
//		  ������ scene��ü�� ������Ű�� ���ο� ��� Ŭ������ ���ε��Ѵ�.
//		  �Ķ���ͷ� �ѱ�� ������ ����Ǿ�� �ȵǹǷ� Call By Value�� ó���Ѵ�.
//
// 2010.01.20 ��������
//////////////////////////////////////////////////////////////////////////////////////////

void CWindow::ChangeScene(int sceneKind)
{
	SceneParamaterBundle sceneParam = {
		hWnd, hInst, //������ ���� �ڵ�
		d3d->GetD3DPointer(), d3d->GetD3DDevicePointer(), d3d->GetFontPointer(), //D3D ���� ������
		screenWidth, screenHeight, //��ũ���� ũ��
		TITLE, //���� ����� ����(���⼱ �ܼ��� �ʱ�ȭ)
		pData //�����͸� ������ ��ü�� ������
	};

	switch(sceneKind)
	{
		case TITLE:
			{
				sceneParam.sceneKind = TITLE;

				delete scene;
				scene = new CTitle(sceneParam);
				break;
			}
		case EVENT:
		case NEXTEVENT:
			{
				sceneParam.sceneKind = EVENT;

				delete scene;
				scene = new CEvent(sceneParam);
				break;
			}
		case BATTLE:
		case NEXTBATTLE:
			{
				sceneParam.sceneKind = BATTLE;

				delete scene;
				scene = new CBattle(sceneParam);
				break;
			}
		case ENDING:
			{
				sceneParam.sceneKind = ENDING;

				delete scene;
				scene = new CEnding(sceneParam);
				break;
			}
	}
}


//////////////////////////////////////////////////////////////////////////////////////////
// LRESULT WndProc(HWND hWnd, UINT iMessage, WPARAM wParam, LPARAM lParam)
//////////////////////////////////////////////////////////////////////////////////////////
//
// ���� : ������ ���ν��� �Լ�
//		  ���콺 �̺�Ʈ�� �˻��Ͽ� ������â�� �̵��� �����Ѵ�.
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

LRESULT CWindow::WndProc(HWND hWnd, UINT iMessage, WPARAM wParam, LPARAM lParam)
{
	static bool rBtnDown = false;
	static int mouseX = 0;
	static int mouseY = 0;

	switch (iMessage)
	{
	case WM_DESTROY:
		{
			PostQuitMessage(0);
			return 0;
		}

	case WM_RBUTTONDOWN:
		{
			rBtnDown = true;
			mouseX = LOWORD(lParam);
			mouseY = HIWORD(lParam);
			return 0;
		}
	case WM_RBUTTONUP:
		{
			rBtnDown = false;
			return 0;
		}
	case WM_MOUSEMOVE:
		{
			RECT rect;

			if(rBtnDown && GetWindowRect(hWnd, &rect) == TRUE)
			{
				int offsetX;
				int offsetY;

				offsetX = LOWORD(lParam) - mouseX;
				offsetY = HIWORD(lParam) - mouseY;
				MoveWindow(hWnd, rect.left + offsetX, rect.top + offsetY, rect.right - rect.left, rect.bottom - rect.top, TRUE);
			}
			return 0;
		}
	}
	return(DefWindowProc(hWnd, iMessage, wParam, lParam));
}


//////////////////////////////////////////////////////////////////////////////////////////
// HWND GetHWnd() const
//////////////////////////////////////////////////////////////////////////////////////////
//
// ��  �� : ������ �ڵ��� ���Ϲ޴� �Լ�
// ���ϰ� : HWND hWnd(������ �ڵ�)
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

HWND CWindow::GetHWnd() const
{
	return hWnd;
}


//////////////////////////////////////////////////////////////////////////////////////////
// HINSTANCE GetHInst() const
//////////////////////////////////////////////////////////////////////////////////////////
//
// ��  �� : ������ �ν��Ͻ��� ���Ϲ޴� �Լ�
// ���ϰ� : HINSTANCE hInst(������ �ν��Ͻ�)
//
// 2010.01.03 ��������
//////////////////////////////////////////////////////////////////////////////////////////

HINSTANCE CWindow::GetHInst() const
{
	return hInst;
}