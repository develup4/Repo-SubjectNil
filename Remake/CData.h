//
// ���ϸ� : CData.h
// 
// ���� : ���ӳ� ������ �����ϴ� Entity Ŭ����
//		  ����� ������� ��ũ��Ʈ������ ��ιۿ� ���������ʰ�������
//		  ������ ĳ������ ������ �̺�Ʈ �����Ȳ ���� �����ص� Ŭ�����̴�.
//
// �輺��(lonkth@naver.com)
// 2010.01.03 ���� ����
//


#pragma once

class CData
{
private:
	char* curScriptFilePath;

protected:

public:
	CData(char* scriptFilePath);
	~CData();

	//ĸ��ȭ ����
	void SetScriptFilePath(char* FilePath);
	char* GetScriptFilePath() const;
};
