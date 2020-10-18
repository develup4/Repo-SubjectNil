//////////////////////////////////////////////////////////////////////////////
//																			//
//	�� ������Ʈ�� DirectX9 SDK August 2007�� �������� ���۵Ǿ����ϴ�.		//
//  ������ ���ؼ��� �̿� ���ϴ� DirectX ������ ��ġ�Ͽ� �ֽñ� �ٶ��ϴ�.	//
//  ���� '�޸վƹ�ü' ��Ʈ�� ��ġ�Ǿ� �־�� ��Ȱ�� ȭ�鱸���� �����մϴ�.  //
//																			//
//////////////////////////////////////////////////////////////////////////////

//
// ���ϸ� : Main.cpp
// 
// ���� : ���α׷��� ������.
//		  �ʿ��� ���̺귯���� ���Խ�Ų��.
//		  ������ Ŭ������ ��ü�� �����Ѵ�.
//		  ���ӷ����Լ��� ȣ���Ѵ�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//

#include "CWindow.h"
#pragma comment(lib, "d3d9.lib") 
#pragma comment(lib, "d3dx9.lib")

//DirectX Show ���� ���̺귯��
#pragma comment(lib, "winmm.lib")
#pragma comment(lib, "Strmiids.lib")


int APIENTRY WinMain(HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpszCmdParam, int nCmdShow)
{
	//������ ������ �ʿ��� �ּ����� ������ ���� ����ü ����
	WinArgsSetting setting = {
		"SubjectNil", //�������� �̸�
		CS_HREDRAW | CS_VREDRAW, WS_POPUPWINDOW | WS_SYSMENU, //������ �ɼ�
		1024, 768 //�������� ������
	};

	CWindow cWnd(setting, hInstance, lpszCmdParam, nCmdShow);
	cWnd.Loop(); //���ӷ���

	return 0;
}