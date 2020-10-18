//
// ���ϸ� : CDXShow.h
// 
// ���� : DirectX Show ���� Ŭ����
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//

#pragma once
#include "DShow.h"

class CDXShow
{
private:

protected:

public:
	IBasicAudio *pVolume;
	IMediaPosition *pPosition;
	IGraphBuilder * pGraph;
	IMediaControl *pControl;
	IMediaEvent *pEvent;

	CDXShow();
	virtual ~CDXShow();

	bool Initialize(HWND hWnd, char* FilePath);
	bool Pause();
	bool Stop();
	bool Run();
	bool SetBalance(float Balance); //������ �¿������ ����, ���� -10000 ~ 10000
	bool SetVolume(float Volume); //���� -10000 ~ 0
};