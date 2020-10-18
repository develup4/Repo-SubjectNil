//
// ���ϸ� : CD3D.h
// 
// ���� : DirectX 3D ���� Ŭ����
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//


#pragma once
#include <d3d9.h>
#include <d3dx9.h>

//��ũ�� �Լ�
#define SAFE_RELEASE(p) if(p){(p)->Release(); (p)=NULL;}
#define SAFE_DELETE(p) if(p){DELETE (p); (p)=NULL;}

class CD3D
{
private:
	LPDIRECT3D9 pD3D; //Direct3D ��ü
	LPDIRECT3DDEVICE9 pd3dDevice; //Direct3D Device ��ü
	LPD3DXFONT pFont;

protected:

public:
	CD3D(HWND hWnd);
	~CD3D();

	//ĸ��ȭ ����
	LPDIRECT3D9 GetD3DPointer() const;
	LPDIRECT3DDEVICE9 GetD3DDevicePointer() const;
	LPD3DXFONT GetFontPointer() const;
};