Option Strict Off
Option Explicit On
'UPGRADE_NOTE: Module��(��) Module_Renamed(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
Imports VB = Microsoft.VisualBasic
Module Module_Renamed
	'API����
	Public Declare Function GetTickCount Lib "kernel32" () As Integer
	Public Declare Function ShowCursor Lib "user32" (ByVal bShow As Integer) As Integer
	Declare Function SetCursorPos Lib "user32" (ByVal x As Integer, ByVal y As Integer) As Integer
	'UPGRADE_WARNING: POINTAPI ����ü���� �� Declare ���� �μ��� ������ ������ Ư���� �־�� �մϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="C429C3A5-5D47-4CD9-8F51-74A1616405DC"'
	Declare Function GetCursorPos Lib "user32" (ByRef lpPoint As POINTAPI) As Integer
	Public Declare Function GetKeyState Lib "user32" (ByVal nVirtKey As Integer) As Short
	Public Declare Function GetAsyncKeyState Lib "user32" (ByVal vKey As Integer) As Short
	Public Structure PAK_Item
		Dim FileName As String
		Dim Data() As Byte
	End Structure
	Public Structure POINTAPI
		Dim curX As Integer
		Dim curY As Integer
	End Structure
	Const MOUSEEVENTF_LEFTDOWN As Short = &H2s
	Const MOUSEEVENTF_LEFTUP As Short = &H4s
	Const MOUSEEVENTF_MIDDLEDOWN As Short = &H20s
	Const MOUSEEVENTF_MIDDLEUP As Short = &H40s
	Const MOUSEEVENTF_MOVE As Short = &H1s
	Const MOUSEEVENTF_ABSOLUTE As Short = &H8000s
	Const MOUSEEVENTF_RIGHTDOWN As Short = &H8s
	Const MOUSEEVENTF_RIGHTUP As Short = &H10s
	Public Structure BITMAPFILEHEADER
		Dim bfType As Short
		Dim bfSize As Integer
		Dim bfReserved1 As Short
		Dim bfReserved2 As Short
		Dim bfOffBits As Integer
	End Structure
	Public Structure BITMAPINFOHEADER
		Dim biSize As Integer
		Dim biWidth As Integer
		Dim biHeight As Integer
		Dim biPlanes As Short
		Dim biBitCount As Short
		Dim biCompression As Integer
		Dim biSizeImage As Integer
		Dim biXPelsPerMeter As Integer
		Dim biYPelsPerMeter As Integer
		Dim biClrUsed As Integer
		Dim biClrImportant As Integer
	End Structure
	Dim aTickCount(50) As Single
	Public dx As New DxVBLib.DirectX7
	'�⺻ ���̷�Ʈ ��ο� ��ü
	Public DD As DxVBLib.DirectDraw7
	Public ddsPrim As DxVBLib.DirectDrawSurface7
	Public ddsBack As DxVBLib.DirectDrawSurface7
	Public ddsd1 As DxVBLib.DDSURFACEDESC2
	Public ddsd2 As DxVBLib.DDSURFACEDESC2
	Public ddCaps As DxVBLib.DDSCAPS2
	'���ҽ�
	Public Black As DxVBLib.DirectDrawSurface7
	Public Riel As DxVBLib.DirectDrawSurface7
	Public GoiHan As DxVBLib.DirectDrawSurface7
	Public Map1 As DxVBLib.DirectDrawSurface7
	Public Emotion As DxVBLib.DirectDrawSurface7
	Public RielCast As DxVBLib.DirectDrawSurface7
	Public Gone As DxVBLib.DirectDrawSurface7
	Public BigTalk As DxVBLib.DirectDrawSurface7
	Public BackScreen As DxVBLib.DirectDrawSurface7
	Public BackScreen1 As DxVBLib.DirectDrawSurface7
	Public BackScreen2 As DxVBLib.DirectDrawSurface7
	Public BackScreen3 As DxVBLib.DirectDrawSurface7
	Public BackScreen4 As DxVBLib.DirectDrawSurface7
	Public Tclick As DxVBLib.DirectDrawSurface7
	Public Map11 As DxVBLib.DirectDrawSurface7
	Public StoreMan As DxVBLib.DirectDrawSurface7
	Public Syurin As DxVBLib.DirectDrawSurface7
	Public Map2 As DxVBLib.DirectDrawSurface7
	Public Effectb As DxVBLib.DirectDrawSurface7
	Public Chapter1 As DxVBLib.DirectDrawSurface7
	Public GameName As DxVBLib.DirectDrawSurface7
	Public Develop As DxVBLib.DirectDrawSurface7
	Public Map3 As DxVBLib.DirectDrawSurface7
	Public Map4 As DxVBLib.DirectDrawSurface7
	Public ISyu As DxVBLib.DirectDrawSurface7
	Public IRiel As DxVBLib.DirectDrawSurface7
	Public Extra As DxVBLib.DirectDrawSurface7
	Public Visual As DxVBLib.DirectDrawSurface7
	Public Cursor As DxVBLib.DirectDrawSurface7
	Public TPGage As DxVBLib.DirectDrawSurface7
	Public TPG As DxVBLib.DirectDrawSurface7
	Public BMap As DxVBLib.DirectDrawSurface7
	Public BMapM As DxVBLib.DirectDrawSurface7
	Public BMapS As DxVBLib.DirectDrawSurface7
	Public Menu As DxVBLib.DirectDrawSurface7
	Public Scur As DxVBLib.DirectDrawSurface7
	Public Omenu As DxVBLib.DirectDrawSurface7
	Public One As DxVBLib.DirectDrawSurface7
	Public Two As DxVBLib.DirectDrawSurface7
	Public Three As DxVBLib.DirectDrawSurface7
	Public Four As DxVBLib.DirectDrawSurface7
	'�⺻ ���̷�Ʈ �ȷ�Ʈ ��ü
	Public ddPal(255) As DxVBLib.PALETTEENTRY
	Public ddPalette As DxVBLib.DirectDrawPalette
	'����
	Public ds As DxVBLib.DirectSound
	Public Effect(7) As DxVBLib.DirectSoundBuffer
	Public BGM As DxVBLib.DirectSoundBuffer
	Dim pnt As POINTAPI
	'UPGRADE_NOTE: char��(��) char_Renamed(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Dim char_Renamed(5) As Integer
	Dim xx As Integer
	Dim yy As Integer
	'UPGRADE_WARNING: Sub Main()�� ������ ���� ���α׷��� ����˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="E08DDC71-66BA-424F-A612-80AF11498FF8"'
	Public Sub Main()
		dx = New DxVBLib.DirectX7
		ShowCursor(False)
		Dim Forders As String
		InitDDraw()
		'�ӽÿ���
		DDCreateNo(BackScreen, False)
		DDCreateNo(BackScreen1, False)
		DDCreateNo(BackScreen2, False)
		DDCreateNo(BackScreen3, False)
		DDCreateNo(BackScreen4, False)
		ExportFromPAK(AppPath & "\Library\Effect.pak", AppPath & "\Library\Sudden")
		InitDSound(frmMain)
		Kill(AppPath & "\Library\Sudden\*.*")
		Event1()
		'Battle
	End Sub
	Public Sub UnloadDx()
		Dim n As Integer
		'���������� ����ϴ� ǥ�鸸 ���⼭ �ʱ�ȭ
		ShowCursor(True)
		'UPGRADE_NOTE: ddPalette ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ddPalette = Nothing
		'UPGRADE_NOTE: ddsBack ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ddsBack = Nothing
		'UPGRADE_NOTE: ddsPrim ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ddsPrim = Nothing
		'UPGRADE_NOTE: DD ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		DD = Nothing
		'UPGRADE_NOTE: dx ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		dx = Nothing
		For n = 0 To 7
			'UPGRADE_NOTE: Effect() ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
			Effect(n) = Nothing
		Next 
	End Sub
	Public Sub InitDSound(ByRef vForm As System.Windows.Forms.Form)
		ds = dx.DirectSoundCreate("")
		ds.SetCooperativeLevel(vForm.Handle.ToInt32, DxVBLib.CONST_DSSCLFLAGS.DSSCL_NORMAL)
		'ȿ���� ���
		LoadWave(Effect(0), AppPath & "\Library\Sudden\Click.wav")
		LoadWave(Effect(1), AppPath & "\Library\Sudden\Door.wav")
		LoadWave(Effect(2), AppPath & "\Library\Sudden\DoorC.wav")
		LoadWave(Effect(3), AppPath & "\Library\Sudden\Walk.wav")
		LoadWave(Effect(4), AppPath & "\Library\Sudden\TalkOk.wav")
		LoadWave(Effect(5), AppPath & "\Library\Sudden\Break.wav")
		LoadWave(Effect(6), AppPath & "\Library\Sudden\BreakS.wav")
		LoadWave(Effect(7), AppPath & "\Library\Sudden\Get.wav")
	End Sub
	Public Sub BGMSound(ByVal cFileName As String)
		'������� �뷮�� �����Ƿ� �Ѱ��� �ε�
		LoadWave(BGM, cFileName)
	End Sub
	'UPGRADE_NOTE: char��(��) char_Renamed(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub EDrawChar(ByVal char_Renamed As DxVBLib.DirectDrawSurface7, ByVal x As Integer, ByVal y As Integer, ByVal pos As Integer)
		'�̺�Ʈ ĳ���� ��´�.
		Dim Temrect As DxVBLib.RECT
		With Temrect
			.Left = pos * 55
			.Right = .Left + 55
			.Top = 0
			.Bottom = 90
		End With
		DDBltFast(char_Renamed, x, y, Temrect, True, True)
	End Sub
	Public Sub Event1()
		'�ʱ�ȭ
		ExportFromPAK(AppPath & "\Library\Map1.pak", AppPath & "\Library\Sudden")
		InitPalatteFromBmpFile(AppPath & "\Library\Sudden\Pallette.bmp", ddsPrim)
		DDCreate(BigTalk, AppPath & "\Library\Sudden\Talk.bmp")
		DDCreate(Riel, AppPath & "\Library\Sudden\Riel.bmp")
		DDCreate(GoiHan, AppPath & "\Library\Sudden\GoiHan.bmp")
		DDCreate(Map1, AppPath & "\Library\Sudden\Map1.bmp")
		DDCreate(Map11, AppPath & "\Library\Sudden\Map1-1.bmp")
		DDCreate(Tclick, AppPath & "\Library\Sudden\Tclick.bmp")
		DDCreate(Effectb, AppPath & "\Library\Sudden\Effect.bmp")
		DDCreate(GameName, AppPath & "\Library\Sudden\GameName.bmp")
		DDCreate(Develop, AppPath & "\Library\Sudden\Develop.bmp")
		DDCreate(Chapter1, AppPath & "\Library\Sudden\Chapter1.bmp")
		DDCreate(IRiel, AppPath & "\Library\Sudden\IRiel.bmp")
		DDCreate(Visual, AppPath & "\Library\Sudden\Visual.bmp")
		DDCreate(Black, AppPath & "\Library\Sudden\Black.bmp")
		DDCreate(Emotion, AppPath & "\Library\Sudden\Emotion.bmp")
		DDCreate(RielCast, AppPath & "\Library\Sudden\RielCast.bmp")
		Kill(AppPath & "\Library\Sudden\*.*")
		BGMSound("C:\SubjectNil\Bgm\Tension1.wav")
		'���뺯��
		Dim n As Integer
		'����ۿ� �齺ũ��2, �����̽��� ���������� �ʱ�ȭ
		Dim vTem As DxVBLib.RECT
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		DDCopy(ddsBack, BackScreen2)
		DDFlip()
		DDBltFast(Black, 0, 0, vTem, True)
		Delay(3) : Effect(1).SetVolume(0) : Effect(1).SetPan(10000) : PlayWave(Effect(1))
		Delay(2) : Effect(3).SetVolume(0) : Effect(3).SetPan(10000) : PlayWave(Effect(3))
		Delay(3) : StopWave(Effect(3)) : Effect(3).SetPan(-10000) : PlayWave(Effect(3))
		Delay(3) : Effect(2).SetVolume(0) : Effect(2).SetPan(10000) : PlayWave(Effect(2))
		Delay(5)
		Speak("����", True, "��...���� �ҳ��� ���ƿ��� �ʾұ���.", "", "", "", "", "", "", IRiel)
		Delay(3)
		Speak("����", True, "��, � ���� ���� �ϵ��� ����.", "", "", "", "", "", "", IRiel)
		Delay(2) : FadeInOut() : Effect(3).SetPan(10000) : PlayWave(Effect(3))
		Delay(2) : Effect(3).SetPan(-10000) : PlayWave(Effect(3))
		Delay(2) : Effect(3).SetPan(10000) : PlayWave(Effect(3))
		Delay(2) : Effect(5).SetVolume(0) : Effect(5).SetPan(10000) : PlayWave(Effect(5))
		Delay(2) : Effect(3).SetPan(-10000) : PlayWave(Effect(3))
		Delay(2) : Effect(5).SetPan(-10000) : PlayWave(Effect(5))
		Delay(2) : Effect(3).SetPan(10000) : PlayWave(Effect(3))
		Delay(3)
		'�����̴� ĳ������ �ʱ���ǥ�� ����
		Dim position(3, 2) As Integer
		position(0, 0) = 555
		position(0, 1) = 115
		position(0, 2) = 2
		position(1, 0) = 635
		position(1, 1) = 330
		position(1, 2) = 1
		position(2, 0) = 175
		position(2, 1) = 485
		position(2, 2) = 0
		position(3, 0) = 365
		position(3, 1) = 555
		position(3, 2) = 2
		DDBltFast(Black, 0, 0, vTem, False)
		'�޹���� ĥ������ �ʵ��� ������ ������ �׽� ĥ�ؾ� �Ѵ�.
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map1)
		DDBltFast(Map1, 0, 0, vTem, True)
		EDrawChar(GoiHan, 600, 555, 0)
		EDrawChar(GoiHan, 660, 555, 0)
		EDrawChar(GoiHan, 720, 555, 0)
		EDrawChar(Riel, position(0, 0), position(0, 1), 4)
		EDrawChar(GoiHan, position(1, 0), position(1, 1), 4)
		EDrawChar(GoiHan, position(2, 0), position(2, 1), 8)
		EDrawChar(GoiHan, position(3, 0), position(3, 1), 0)
		DDCopy(ddsBack, BackScreen2)
		DDFlip()
		BGM.SetVolume(-1000)
		PlayWave(BGM, True)
		FadeInOut(False)
		DDCopy(BackScreen2, ddsBack)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Emotion)
		DDBltFast(Emotion, 620, 300, vTem, True)
		DDFlip()
		Delay(1)
		Speak("����", False, "......")
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map1)
		DDBltFast(Map1, 0, 0, vTem, True)
		EDrawChar(GoiHan, 600, 555, 0)
		EDrawChar(GoiHan, 660, 555, 0)
		EDrawChar(GoiHan, 720, 555, 0)
		DDCopy(ddsBack, BackScreen2)
		For n = 0 To 7
			Delay(0.15)
			DDCopy(BackScreen2, ddsBack)
			EDrawChar(GoiHan, position(1, 0), position(1, 1), 4)
			EDrawChar(GoiHan, position(2, 0), position(2, 1), 12)
			EDrawChar(GoiHan, position(3, 0), position(3, 1), 4)
			EWalk(Riel, position(0, 0), position(0, 1), 0, position(0, 2))
			DDFlip()
		Next 
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(GoiHan, position(1, 0), position(1, 1), 4)
		EDrawChar(GoiHan, position(2, 0), position(2, 1), 12)
		EDrawChar(GoiHan, position(3, 0), position(3, 1), 4)
		EDrawChar(Riel, position(0, 0), position(0, 1), 0)
		DDCopy(ddsBack, BackScreen2)
		DDFlip()
		Delay(1)
		Speak("����", True, "��, �����ֱ���...�̷�! �̷���  ������ ������ �Žǿ� ��ġ �� �δٴ�...", "", "", "", "", "", "", IRiel)
		Delay(1)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map1)
		DDBltFast(Map1, 0, 0, vTem, True)
		EDrawChar(GoiHan, 600, 555, 0)
		EDrawChar(GoiHan, 660, 555, 0)
		EDrawChar(GoiHan, 720, 555, 0)
		EDrawChar(GoiHan, position(1, 0), position(1, 1), 4)
		EDrawChar(GoiHan, position(2, 0), position(2, 1), 12)
		EDrawChar(GoiHan, position(3, 0), position(3, 1), 4)
		EDrawChar(RielCast, position(0, 0), position(0, 1), 0)
		DDCopy(ddsBack, BackScreen2)
		DDFlip()
		Delay(2)
		'����Ʈ
		Effect(6).SetVolume(0)
		Effect(6).SetPan(0)
		PlayWave(Effect(6))
		For n = 0 To 23
			DDCopy(BackScreen2, ddsBack)
			With vTem
				.Left = n * 234
				.Right = .Left + 234
				.Top = 0
				.Bottom = 214
			End With
			DDBltFast(Effectb, position(0, 0) - 65, position(0, 1) - 40, vTem, True)
			DDFlip()
			Delay(0.1)
		Next 
		Delay(1)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map1)
		DDBltFast(Map1, 0, 0, vTem, True)
		EDrawChar(GoiHan, 600, 555, 0)
		EDrawChar(GoiHan, 660, 555, 0)
		EDrawChar(GoiHan, 720, 555, 0)
		EDrawChar(GoiHan, position(1, 0), position(1, 1), 4)
		EDrawChar(GoiHan, position(2, 0), position(2, 1), 12)
		EDrawChar(GoiHan, position(3, 0), position(3, 1), 4)
		EDrawChar(Riel, position(0, 0), position(0, 1), 0)
		DDCopy(ddsBack, BackScreen2)
		DDFlip()
		Speak("����", True, "�Դٰ�...��ġ���� ã�� ������ ������...", "�� ���� ��ȣ������ ���ٴ�...�ʹ� ����� �ɿ�?......����!", "", "", "", "", "", IRiel)
		Delay(1)
		Effect(7).SetVolume(0)
		Effect(7).SetPan(0)
		PlayWave(Effect(7))
		'UPGRADE_NOTE: Map1 ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Map1 = Nothing
		'���Ʈ ���� ������ ����
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map11)
		DDBltFast(Map11, 0, 0, vTem, True)
		EDrawChar(GoiHan, 720, 555, 0)
		EDrawChar(GoiHan, 660, 555, 0)
		EDrawChar(GoiHan, 600, 555, 0)
		EDrawChar(GoiHan, position(1, 0), position(1, 1), 4)
		EDrawChar(GoiHan, position(2, 0), position(2, 1), 12)
		EDrawChar(GoiHan, position(3, 0), position(3, 1), 4)
		EDrawChar(Riel, position(0, 0), position(0, 1), 0)
		DDCopy(ddsBack, BackScreen2)
		DDFlip()
		Delay(1)
		VisualF("����, ���Ʈ ȹ��!")
		FadeInOut()
		Delay(3)
		'�������� ��ġ ����
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map11)
		DDBltFast(Map11, 0, 0, vTem, True)
		EDrawChar(GoiHan, 600, 435, 12)
		EDrawChar(GoiHan, 600, 495, 12)
		EDrawChar(GoiHan, 600, 555, 12)
		position(0, 0) = 690
		position(0, 1) = 535
		position(0, 2) = 2
		position(1, 0) = 710
		position(1, 1) = 455
		position(1, 2) = 1
		position(2, 0) = 710
		position(2, 1) = 395
		position(2, 2) = 0
		position(3, 0) = 710
		position(3, 1) = 335
		position(3, 2) = 2
		EDrawChar(GoiHan, position(3, 0), position(3, 1), 0)
		EDrawChar(GoiHan, position(2, 0), position(2, 1), 0)
		EDrawChar(GoiHan, position(1, 0), position(1, 1), 0)
		EDrawChar(Riel, position(0, 0), position(0, 1), 0)
		'�ϴ��� �������� �����Ƿ� �� ��� ����
		DDCopy(ddsBack, BackScreen2)
		DDFlip()
		FadeInOut(False)
		Delay(2)
		Speak("����", True, "��...�������� �ϼž� �� �Ͽ� ���ؼ��� �� �˰� ��ð�����?", "", "", "", "", "", "", IRiel)
		Delay(2)
		Speak("����", False, "......")
		Delay(2)
		Speak("����", True, "��·�ų� ���� ����� ������ �ֽñ�...", "�״��� ��������� ������ ������. ������...", "", "", "", "", "", IRiel)
		Delay(2)
		Speak("����", False, "......")
		'�ٽ� �� ����
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map11)
		DDBltFast(Map11, 0, 0, vTem, True)
		EDrawChar(GoiHan, 600, 435, 12)
		EDrawChar(GoiHan, 600, 495, 12)
		EDrawChar(GoiHan, 600, 555, 12)
		DDCopy(ddsBack, BackScreen2)
		'�Ʒ��� ���� �ɾ��.
		For n = 0 To 9
			Delay(0.15)
			DDCopy(BackScreen2, ddsBack)
			If n > 1 Then
				EWalk(GoiHan, position(3, 0), position(3, 1), 0, position(1, 2))
			Else
				EDrawChar(GoiHan, position(3, 0), position(3, 1), 0)
			End If
			If n > 1 Then
				EWalk(GoiHan, position(2, 0), position(2, 1), 0, position(2, 2))
			Else
				EDrawChar(GoiHan, position(2, 0), position(2, 1), 0)
			End If
			If n > 1 Then
				EWalk(GoiHan, position(1, 0), position(1, 1), 0, position(3, 2))
			Else
				EDrawChar(GoiHan, position(1, 0), position(1, 1), 0)
			End If
			If n < 8 Then
				EWalk(Riel, position(0, 0), position(0, 1), 0, position(0, 2))
			Else
				EDrawChar(Riel, position(0, 0), position(0, 1), 0)
			End If
			DDFlip()
		Next 
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map11)
		DDBltFast(Map11, 0, 0, vTem, True)
		EDrawChar(GoiHan, 600, 435, 12)
		EDrawChar(GoiHan, 600, 495, 12)
		EDrawChar(GoiHan, 600, 555, 12)
		EDrawChar(GoiHan, position(3, 0), position(3, 1), 0)
		EDrawChar(GoiHan, position(2, 0), position(2, 1), 0)
		EDrawChar(GoiHan, position(1, 0), position(1, 1), 0)
		EDrawChar(Riel, position(0, 0), position(0, 1), 0)
		DDCopy(ddsBack, BackScreen2)
		Speak("����", True, "��! ������ ��� ���� �༮���̱�!", "", "", "", "", "", "", IRiel)
		Effect(1).SetVolume(0)
		Effect(1).SetPan(10000)
		PlayWave(Effect(1))
		FadeInOut()
		Delay(2)
		Effect(3).SetVolume(0)
		Effect(3).SetPan(10000)
		PlayWave(Effect(3))
		Delay(3)
		Effect(2).SetVolume(0)
		Effect(2).SetPan(10000)
		PlayWave(Effect(2))
		Delay(4)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Develop)
		DDBltFast(Develop, 360, 360, vTem, False)
		DDFlip()
		FadeInOut(False)
		Delay(1)
		FadeInOut()
		Delay(1)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(GameName)
		DDBltFast(GameName, 360, 360, vTem, False)
		DDFlip()
		FadeInOut(False)
		Delay(1)
		WhiteInOut()
		Delay(1)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Chapter1)
		DDBltFast(Chapter1, 0, 0, vTem, False)
		DDFlip()
		WhiteInOut(False)
		Delay(8)
		FadeInOut()
		Delay(3)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		DDFlip()
		FadeInOut(False)
		Delay(5)
		StopWave(BGM)
		Event2()
	End Sub
	Public Sub Event2()
		Delay(2)
		'UPGRADE_NOTE: Black ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Black = Nothing
		'UPGRADE_NOTE: BigTalk ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		BigTalk = Nothing
		'UPGRADE_NOTE: Riel ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Riel = Nothing
		'UPGRADE_NOTE: GoiHan ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		GoiHan = Nothing
		'UPGRADE_NOTE: Map1 ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Map1 = Nothing
		'UPGRADE_NOTE: Map11 ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Map11 = Nothing
		'UPGRADE_NOTE: Tclick ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Tclick = Nothing
		'UPGRADE_NOTE: RielCast ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		RielCast = Nothing
		'UPGRADE_NOTE: Effectb ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Effectb = Nothing
		'UPGRADE_NOTE: Emotion ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Emotion = Nothing
		'UPGRADE_NOTE: Develop ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Develop = Nothing
		'UPGRADE_NOTE: Chapter1 ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Chapter1 = Nothing
		'UPGRADE_NOTE: GameName ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		GameName = Nothing
		'UPGRADE_NOTE: IRiel ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		IRiel = Nothing
		'UPGRADE_NOTE: Visual ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Visual = Nothing
		ExportFromPAK(AppPath & "\Library\Map2.pak", AppPath & "\Library\Sudden")
		InitPalatteFromBmpFile(AppPath & "\Library\Sudden\Pallette.bmp", ddsPrim)
		DDCreate(Black, AppPath & "\Library\Sudden\Black.bmp")
		DDCreate(BigTalk, AppPath & "\Library\Sudden\Talk.bmp")
		DDCreate(Tclick, AppPath & "\Library\Sudden\Tclick.bmp")
		DDCreate(StoreMan, AppPath & "\Library\Sudden\MerchantFront.bmp")
		DDCreate(Syurin, AppPath & "\Library\Sudden\Syu.bmp")
		DDCreate(Map2, AppPath & "\Library\Sudden\Map2.bmp")
		DDCreate(ISyu, AppPath & "\Library\Sudden\ISyu.bmp")
		DDCreate(Extra, AppPath & "\Library\Sudden\GirlFront.bmp")
		DDCreate(Emotion, AppPath & "\Library\Sudden\Emotion.bmp")
		DDCreate(Visual, AppPath & "\Library\Sudden\Visual.bmp")
		DDCreate(Gone, AppPath & "\Library\Sudden\Gone.bmp")
		Kill(AppPath & "\Library\Sudden\*.*")
		BGMSound("C:\SubjectNil\Bgm\Town.wav")
		BGM.SetVolume(-1000)
		PlayWave(BGM, True)
		Dim n As Integer
		Dim vTem As DxVBLib.RECT
		FadeInOut()
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map2)
		DDBltFast(Map2, 0, 0, vTem, True)
		EDrawChar(StoreMan, 530, 240, 0)
		EDrawChar(Extra, 725, 140, 0)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Emotion)
		DDBltFast(Emotion, 710, 105, vTem, True)
		DDCopy(ddsBack, BackScreen2)
		Dim position(2) As Integer
		position(0) = 830
		position(1) = 900
		position(2) = 1
		EWalk(Syurin, position(0), position(1), 1, position(2))
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Gone)
		DDBltFast(Gone, 1024, 50, vTem, True)
		DDFlip()
		FadeInOut(False)
		For n = 0 To 272
			DDCopy(BackScreen2, ddsBack)
			'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			vTem = DDGetRect(Gone)
			DDBltFast(Gone, 1024 - n, 50, vTem, True, True)
			DDFlip()
			Delay(0.0001)
		Next 
		For n = 0 To 272
			DDCopy(BackScreen2, ddsBack)
			'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			vTem = DDGetRect(Gone)
			DDBltFast(Gone, 752 + n, 50, vTem, True, True)
			DDFlip()
			Delay(0.0001)
		Next 
		For n = 0 To 48
			DDCopy(BackScreen2, ddsBack)
			EWalk(Syurin, position(0), position(1), 1, position(2))
			DDFlip()
			Delay(0.15)
		Next 
		For n = 0 To 27
			DDCopy(BackScreen2, ddsBack)
			EWalk(Syurin, position(0), position(1), 2, position(2))
			DDFlip()
			Delay(0.15)
		Next 
		For n = 0 To 8
			DDCopy(BackScreen2, ddsBack)
			EWalk(Syurin, position(0), position(1), 1, position(2))
			DDFlip()
			Delay(0.15)
		Next 
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(Syurin, position(0), position(1), 4)
		DDCopy(ddsBack, BackScreen2)
		Speak("������", True, "������ ����ǰ�� ������ ���̶�� �˰� �ֽ��ϴ�.", "", "", "", "", "", "", ISyu)
		Delay(1)
		Speak("����", False, "��, �׷� �� û�� �ڳٰ�? �̽��״Ͼƻ� ��Ʈ�� ã�� ����? �̸������̸� �׻� ���Լ� ��Ʈ�� �簡�� �ʾҴ°�?", "�׷��� ������ ���� �ʰ� �Ա���, �� �ܰ�մ��� �ƴϾ��ٸ� ���浵 ����������! ��������")
		Delay(1)
		VisualF("������, ���� �ǳ޴�.")
		'������ �ڵ��� ��
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map2)
		DDBltFast(Map2, 0, 0, vTem, True)
		EDrawChar(StoreMan, 530, 240, 0)
		EDrawChar(Extra, 725, 140, 0)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Emotion)
		DDBltFast(Emotion, 710, 105, vTem, True)
		DDCopy(ddsBack, BackScreen2)
		EDrawChar(Syurin, position(0), position(1), 0)
		DDFlip()
		For n = 0 To 8
			DDCopy(BackScreen2, ddsBack)
			EWalk(Syurin, position(0), position(1), 0, position(2))
			DDFlip()
			Delay(0.15)
		Next 
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map2)
		DDBltFast(Map2, 0, 0, vTem, True)
		EDrawChar(StoreMan, 530, 240, 0)
		EDrawChar(Extra, 725, 140, 0)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Emotion)
		DDBltFast(Emotion, 710, 105, vTem, True)
		EDrawChar(Syurin, position(0), position(1), 0)
		DDCopy(ddsBack, BackScreen2)
		Speak("����", False, "��������!! ������ ���� ���� û���̷α�, ��ø� ��ٷ� ���Գ�!")
		Delay(1)
		Speak("������", True, "......", "", "", "", "", "", "", ISyu)
		Delay(1)
		Speak("����", False, "�Ƹ���...�̹��� ���������� �̽��״Ͼƻ� ��Ʈ�� ã�ƺ��� ��ư� �� �ɼ�...", "�׸��� �ٸ� ���ε鿡�Լ��� ã�ƺ���� ����Ծ�...")
		Delay(1)
		'�ٽ� ���Ƽ�
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map2)
		DDBltFast(Map2, 0, 0, vTem, True)
		EDrawChar(StoreMan, 530, 240, 0)
		EDrawChar(Extra, 725, 140, 0)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Emotion)
		DDBltFast(Emotion, 710, 105, vTem, True)
		DDCopy(ddsBack, BackScreen2)
		EDrawChar(Syurin, position(0), position(1), 4)
		DDFlip()
		For n = 0 To 8
			DDCopy(BackScreen2, ddsBack)
			EWalk(Syurin, position(0), position(1), 1, position(2))
			DDFlip()
			Delay(0.15)
		Next 
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map2)
		DDBltFast(Map2, 0, 0, vTem, True)
		EDrawChar(StoreMan, 530, 240, 0)
		EDrawChar(Extra, 725, 140, 0)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Emotion)
		DDBltFast(Emotion, 710, 105, vTem, True)
		EDrawChar(Syurin, position(0), position(1), 4)
		DDCopy(ddsBack, BackScreen2)
		DDCopy(BackScreen2, ddsBack)
		Speak("������", True, "......?", "", "", "", "", "", "", ISyu)
		Delay(1)
		Speak("����", False, "����, �׷� ǥ�� ���� ����. ����� ���ڴ°�? ���� ��� ��Ȳ�� ���� �ƴϰŵ�... ������ �߽����� �̰� �������� �������� �⼺�� �θ��ٱ�...", "�Դٰ� �̽��״Ͼƻ� ��Ʈ���� ������ ���� ����� �ϳ� �� �Ѿ�� �ϴ� ��ǰ�� ��ҿ��� ���ϱ� ��������� ���� ���� ������ ���� ���������.")
		Delay(1)
		Speak("����", False, "�Դٰ� �� �ȸ��� ��ǰ�� �ƴϴ�...... ��! �׸��� �̰� �����ϼ�. ���� �� ������ ã�� �͵� �̰� �������� �� �״ϱ�.")
		Delay(1)
		VisualF("������, ��Ʈ�� �޾Ҵ�!")
		Speak("������", True, "�����մϴ�. �׷� �� �̸�...", "", "", "", "", "", "", ISyu)
		Delay(1)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map2)
		DDBltFast(Map2, 0, 0, vTem, True)
		EDrawChar(StoreMan, 530, 240, 0)
		EDrawChar(Extra, 725, 140, 0)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Emotion)
		DDBltFast(Emotion, 710, 105, vTem, True)
		DDCopy(ddsBack, BackScreen2)
		For n = 0 To 13
			DDCopy(BackScreen2, ddsBack)
			EWalk(Syurin, position(0), position(1), 0, position(2))
			DDFlip()
			Delay(0.15)
		Next 
		For n = 0 To 61
			DDCopy(BackScreen2, ddsBack)
			EWalk(Syurin, position(0), position(1), 2, position(2))
			DDFlip()
			Delay(0.15)
		Next 
		DDCopy(ddsPrim, BackScreen2)
		Delay(2)
		Speak("����", False, "�� �̻��� �ڳ׿��� ����� ��ų�� ���ڱ�...", "������ �����ڸ� �� ������ �ڳװ� ������ �����ϴ��� �� ���� ����...����! �ڳ� �� ���� �����Ѵ�����... ������ �� ���̿��� �װ��� ����������...")
		Delay(1)
		Speak("����", False, "�̷�! �ð��� ���� �̷��� �Ƴ�...�׷� ���� ���� ���ư� ���߰ڱ�...", "���� �̰��� �������ΰ�?")
		Delay(2)
		FadeInOut()
		Delay(3)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		DDFlip()
		FadeInOut(False)
		StopWave(BGM)
		Event3()
	End Sub
	Public Sub Event3()
		Delay(2)
		'UPGRADE_NOTE: Black ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Black = Nothing
		'UPGRADE_NOTE: BigTalk ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		BigTalk = Nothing
		'UPGRADE_NOTE: StoreMan ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		StoreMan = Nothing
		'UPGRADE_NOTE: Syurin ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Syurin = Nothing
		'UPGRADE_NOTE: Map2 ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Map2 = Nothing
		'UPGRADE_NOTE: ISyu ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ISyu = Nothing
		'UPGRADE_NOTE: Tclick ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Tclick = Nothing
		'UPGRADE_NOTE: Emotion ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Emotion = Nothing
		'UPGRADE_NOTE: Extra ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Extra = Nothing
		'UPGRADE_NOTE: Gone ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Gone = Nothing
		'UPGRADE_NOTE: Visual ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Visual = Nothing
		ExportFromPAK(AppPath & "\Library\Map3.pak", AppPath & "\Library\Sudden")
		InitPalatteFromBmpFile(AppPath & "\Library\Sudden\Pallette.bmp", ddsPrim)
		DDCreate(BigTalk, AppPath & "\Library\Sudden\Talk.bmp")
		DDCreate(Black, AppPath & "\Library\Sudden\Black.bmp")
		DDCreate(Tclick, AppPath & "\Library\Sudden\Tclick.bmp")
		DDCreate(Riel, AppPath & "\Library\Sudden\Riel.bmp")
		DDCreate(Syurin, AppPath & "\Library\Sudden\Syurin.bmp")
		DDCreate(Map3, AppPath & "\Library\Sudden\Map3.bmp")
		DDCreate(IRiel, AppPath & "\Library\Sudden\IRiel.bmp")
		DDCreate(ISyu, AppPath & "\Library\Sudden\IRiel.bmp")
		DDCreate(GoiHan, AppPath & "\Library\Sudden\GoiHan.bmp")
		DDCreate(Gone, AppPath & "\Library\Sudden\Gone.bmp")
		Kill(AppPath & "\Library\Sudden\*.*")
		BGMSound("C:\SubjectNil\Bgm\Tension2.wav")
		BGM.SetVolume(0)
		PlayWave(BGM, True)
		FadeInOut()
		Dim position(4, 2) As Integer
		position(0, 0) = 460
		position(0, 1) = -170
		position(0, 2) = 1
		position(1, 0) = 400
		position(1, 1) = -275
		position(1, 2) = 2
		position(2, 0) = 460
		position(2, 1) = -275
		position(2, 2) = 0
		position(3, 0) = 520
		position(3, 1) = -275
		position(3, 2) = 2
		position(4, 0) = 610
		position(4, 1) = 840
		position(4, 2) = 1
		Dim n As Integer
		Dim vTem As DxVBLib.RECT
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map3)
		DDBltFast(Map3, 0, 0, vTem, True)
		EWalk(Syurin, position(4, 0), position(4, 1), 1, position(4, 2))
		EWalk(Riel, position(0, 0), position(0, 1), 0, position(0, 2))
		EWalk(GoiHan, position(1, 0), position(1, 1), 0, position(1, 2))
		EWalk(GoiHan, position(2, 0), position(2, 1), 0, position(2, 2))
		EWalk(GoiHan, position(3, 0), position(3, 1), 0, position(3, 2))
		DDCopy(ddsBack, BackScreen2)
		DDFlip()
		FadeInOut(False)
		For n = 0 To 272
			DDCopy(BackScreen2, ddsBack)
			'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			vTem = DDGetRect(Gone)
			DDBltFast(Gone, 1024 - n, 50, vTem, True, True)
			DDFlip()
			Delay(0.0001)
		Next 
		For n = 0 To 272
			DDCopy(BackScreen2, ddsBack)
			'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			vTem = DDGetRect(Gone)
			DDBltFast(Gone, 752 + n, 50, vTem, True, True)
			DDFlip()
			Delay(0.0001)
		Next 
		Speak("����1", False, "�� ������� ������? ó�����µ�...������� �� ������ ���ΰ�?")
		Delay(1)
		Speak("����2", False, "�۽��...�༼�� ���Ƽ��� �����ڴ� �ƴ� �� ����...���ϴ� ������ϱ�?")
		Delay(1)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map3)
		DDBltFast(Map3, 0, 0, vTem, True)
		DDCopy(ddsBack, BackScreen2)
		For n = 0 To 47
			Delay(0.15)
			DDCopy(BackScreen2, ddsBack)
			EWalk(GoiHan, position(1, 0), position(1, 1), 0, position(1, 2))
			EWalk(GoiHan, position(2, 0), position(2, 1), 0, position(2, 2))
			EWalk(GoiHan, position(3, 0), position(3, 1), 0, position(3, 2))
			EWalk(Riel, position(0, 0), position(0, 1), 0, position(0, 2))
			EWalk(Syurin, position(4, 0), position(4, 1), 1, position(4, 2))
			DDFlip()
		Next 
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(GoiHan, position(1, 0), position(1, 1), 0)
		EDrawChar(GoiHan, position(2, 0), position(2, 1), 0)
		EDrawChar(GoiHan, position(3, 0), position(3, 1), 0)
		EDrawChar(Riel, position(0, 0), position(0, 1), 0)
		EDrawChar(Syurin, position(4, 0), position(4, 1), 4)
		DDFlip()
		Delay(3)
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(GoiHan, position(1, 0), position(1, 1), 0)
		EDrawChar(GoiHan, position(2, 0), position(2, 1), 0)
		EDrawChar(GoiHan, position(3, 0), position(3, 1), 0)
		EDrawChar(Riel, position(0, 0), position(0, 1), 12)
		EDrawChar(Syurin, position(4, 0), position(4, 1), 8)
		DDFlip()
		Delay(4)
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(GoiHan, position(1, 0), position(1, 1), 0)
		EDrawChar(GoiHan, position(2, 0), position(2, 1), 0)
		EDrawChar(GoiHan, position(3, 0), position(3, 1), 0)
		EDrawChar(Riel, position(0, 0), position(0, 1), 0)
		EDrawChar(Syurin, position(4, 0), position(4, 1), 8)
		DDFlip()
		DDCopy(ddsPrim, BackScreen2)
		Speak("����", True, "�̷��̷�...��ð��� �ʾ������ ��������ٱ���.", "��ȹ�� Ʋ������ ���� � ���ѷ��� �ڱ���.", "", "", "", "", "", IRiel)
		Delay(1)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map3)
		DDBltFast(Map3, 0, 0, vTem, True)
		EDrawChar(Syurin, position(4, 0), position(4, 1), 8)
		DDCopy(ddsBack, BackScreen2)
		For n = 0 To 15
			Delay(0.15)
			DDCopy(BackScreen2, ddsBack)
			EWalk(GoiHan, position(1, 0), position(1, 1), 0, position(1, 2))
			EWalk(GoiHan, position(2, 0), position(2, 1), 0, position(2, 2))
			EWalk(GoiHan, position(3, 0), position(3, 1), 0, position(3, 2))
			EWalk(Riel, position(0, 0), position(0, 1), 0, position(0, 2))
			DDFlip()
		Next 
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map3)
		DDBltFast(Map3, 0, 0, vTem, True)
		EDrawChar(Syurin, position(4, 0), position(4, 1), 0)
		DDCopy(ddsBack, BackScreen2)
		For n = 0 To 40
			Delay(0.15)
			DDCopy(BackScreen2, ddsBack)
			EWalk(GoiHan, position(1, 0), position(1, 1), 0, position(1, 2))
			EWalk(GoiHan, position(2, 0), position(2, 1), 0, position(2, 2))
			EWalk(GoiHan, position(3, 0), position(3, 1), 0, position(3, 2))
			EWalk(Riel, position(0, 0), position(0, 1), 0, position(0, 2))
			DDFlip()
		Next 
		Delay(1)
		DDCopy(ddsPrim, BackScreen2)
		Speak("������", True, "�̻��� ����̱�......", "", "", "", "", "", "", ISyu)
		Delay(1)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map3)
		DDBltFast(Map3, 0, 0, vTem, True)
		DDCopy(ddsBack, BackScreen2)
		For n = 0 To 45
			Delay(0.15)
			DDCopy(BackScreen2, ddsBack)
			EWalk(GoiHan, position(1, 0), position(1, 1), 0, position(1, 2))
			EWalk(GoiHan, position(2, 0), position(2, 1), 0, position(2, 2))
			EWalk(GoiHan, position(3, 0), position(3, 1), 0, position(3, 2))
			EWalk(Riel, position(0, 0), position(0, 1), 0, position(0, 2))
			EWalk(Syurin, position(4, 0), position(4, 1), 1, position(4, 2))
			DDFlip()
		Next 
		FadeInOut()
		Delay(3)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		DDFlip()
		FadeInOut(False)
		StopWave(BGM)
		Event4()
	End Sub
	Public Sub Event4()
		'UPGRADE_NOTE: Black ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Black = Nothing
		'UPGRADE_NOTE: BigTalk ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		BigTalk = Nothing
		'UPGRADE_NOTE: Riel ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Riel = Nothing
		'UPGRADE_NOTE: Syurin ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Syurin = Nothing
		'UPGRADE_NOTE: Map3 ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Map3 = Nothing
		'UPGRADE_NOTE: ISyu ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ISyu = Nothing
		'UPGRADE_NOTE: Tclick ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Tclick = Nothing
		'UPGRADE_NOTE: GoiHan ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		GoiHan = Nothing
		'UPGRADE_NOTE: Gone ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Gone = Nothing
		'UPGRADE_NOTE: IRiel ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		IRiel = Nothing
		ExportFromPAK(AppPath & "\Library\Map4.pak", AppPath & "\Library\Sudden")
		InitPalatteFromBmpFile(AppPath & "\Library\Sudden\Pallette.bmp", ddsPrim)
		DDCreate(BigTalk, AppPath & "\Library\Sudden\Talk.bmp")
		DDCreate(Black, AppPath & "\Library\Sudden\Black.bmp")
		DDCreate(Tclick, AppPath & "\Library\Sudden\Tclick.bmp")
		DDCreate(Syurin, AppPath & "\Library\Sudden\Syurin.bmp")
		DDCreate(Map4, AppPath & "\Library\Sudden\Map4.bmp")
		DDCreate(Emotion, AppPath & "\Library\Sudden\Emotion.bmp")
		DDCreate(ISyu, AppPath & "\Library\Sudden\ISyu.bmp")
		DDCreate(GoiHan, AppPath & "\Library\Sudden\GoiHan.bmp")
		DDCreate(Gone, AppPath & "\Library\Sudden\Gone.bmp")
		Kill(AppPath & "\Library\Sudden\*.*")
		BGMSound("C:\SubjectNil\Bgm\Tension1.wav")
		Delay(2)
		Dim vTem As DxVBLib.RECT
		FadeInOut()
		BGM.SetVolume(0)
		PlayWave(BGM, True)
		Dim n As Integer
		Dim position(2) As Integer
		position(0) = 438
		position(1) = 800
		position(2) = 0
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map4)
		DDBltFast(Map4, 0, 0, vTem, True)
		DDCopy(ddsBack, BackScreen2)
		DDFlip()
		FadeInOut(False)
		For n = 0 To 300
			DDCopy(BackScreen2, ddsBack)
			'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			vTem = DDGetRect(Gone)
			DDBltFast(Gone, 1024 - n, 50, vTem, True, True)
			DDFlip()
			Delay(0.0001)
		Next 
		For n = 0 To 300
			DDCopy(BackScreen2, ddsBack)
			'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			vTem = DDGetRect(Gone)
			DDBltFast(Gone, 724 + n, 50, vTem, True, True)
			DDFlip()
			Delay(0.0001)
		Next 
		Delay(2)
		For n = 0 To 30
			Delay(0.15)
			DDCopy(BackScreen2, ddsBack)
			EWalk(Syurin, position(0), position(1), 1, position(2))
			DDFlip()
		Next 
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(Syurin, position(0), position(1), 4)
		DDFlip()
		Delay(2)
		DDCopy(ddsPrim, BackScreen2)
		Speak("������", True, "......", "", "", "", "", "", "", ISyu)
		Delay(1)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map4)
		DDBltFast(Map4, 0, 0, vTem, True)
		DDCopy(ddsBack, BackScreen2)
		EDrawChar(Syurin, position(0), position(1), 8)
		DDFlip()
		Delay(1)
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(Syurin, position(0), position(1), 12)
		DDFlip()
		Delay(1)
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(Syurin, position(0), position(1), 4)
		DDFlip()
		Delay(1)
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(Syurin, position(0), position(1), 4)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Emotion)
		DDBltFast(Emotion, 430, 470, vTem, True)
		DDFlip()
		Delay(1)
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(Syurin, position(0), position(1), 4)
		DDFlip()
		Delay(1)
		For n = 0 To 15
			Delay(0.2)
			DDCopy(BackScreen2, ddsBack)
			EWalk(Syurin, position(0), position(1), 1, position(2))
			DDFlip()
		Next 
		DDCopy(BackScreen2, ddsBack)
		EDrawChar(Syurin, position(0), position(1), 4)
		DDFlip()
		Delay(1)
		DDCopy(ddsPrim, BackScreen2)
		Speak("������", True, "��ü�� �巯����.", "", "", "", "", "", "", ISyu)
		FadeInOut()
		Delay(2)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Map4)
		DDBltFast(Map4, 0, 0, vTem, True)
		EDrawChar(Syurin, position(0), position(1), 4)
		EDrawChar(GoiHan, 342, 175, 0)
		EDrawChar(GoiHan, 438, 105, 0)
		EDrawChar(GoiHan, 539, 186, 0)
		DDCopy(ddsBack, BackScreen2)
		DDFlip()
		FadeInOut(False)
		Delay(2)
		Speak("���ѵ�", False, "......")
		Delay(1)
		Speak("������", True, "�����ΰ�.", "", "", "", "", "", "", ISyu)
		Delay(1)
		FadeInOut()
		Delay(3)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		DDFlip()
		FadeInOut(False)
		StopWave(BGM)
		Battle()
	End Sub
	Public Sub Credit()
		'UPGRADE_NOTE: TPGage ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		TPGage = Nothing
		'UPGRADE_NOTE: TPG ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		TPG = Nothing
		'UPGRADE_NOTE: BMap ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		BMap = Nothing
		'UPGRADE_NOTE: BMap ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		BMap = Nothing
		'UPGRADE_NOTE: BMapM ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		BMapM = Nothing
		'UPGRADE_NOTE: Cursor ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Cursor = Nothing
		'UPGRADE_NOTE: Syurin ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Syurin = Nothing
		'UPGRADE_NOTE: Menu ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Menu = Nothing
		'UPGRADE_NOTE: BMapS ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		BMapS = Nothing
		'UPGRADE_NOTE: Scur ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Scur = Nothing
		'UPGRADE_NOTE: Omenu ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Omenu = Nothing
		'UPGRADE_NOTE: Black ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Black = Nothing
		ExportFromPAK(AppPath & "\Library\Credit.pak", AppPath & "\Library\Sudden")
		InitPalatteFromBmpFile(AppPath & "\Library\Sudden\Pallette.bmp", ddsPrim)
		DDCreate(Black, AppPath & "\Library\Sudden\Black.bmp")
		DDCreate(One, AppPath & "\Library\Sudden\1.bmp")
		DDCreate(Two, AppPath & "\Library\Sudden\2.bmp")
		DDCreate(Three, AppPath & "\Library\Sudden\3.bmp")
		DDCreate(Four, AppPath & "\Library\Sudden\4.bmp")
		Kill(AppPath & "\Library\Sudden\*.*")
		FadeInOut()
		Dim vTem As DxVBLib.RECT
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		DDFlip()
		FadeInOut(False)
		BGMSound("C:\SubjectNil\Bgm\Credit.wav")
		BGM.SetVolume(0)
		PlayWave(BGM, True)
		Delay(15)
		FadeInOut()
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(One)
		DDBltFast(One, 0, 0, vTem, True)
		DDFlip()
		FadeInOut(False)
		Delay(50)
		FadeInOut()
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Two)
		DDBltFast(Two, 0, 0, vTem, True)
		DDFlip()
		FadeInOut(False)
		Delay(50)
		FadeInOut()
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Three)
		DDBltFast(Three, 0, 0, vTem, True)
		DDFlip()
		FadeInOut(False)
		Delay(50)
		FadeInOut()
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Four)
		DDBltFast(Four, 0, 0, vTem, True)
		DDFlip()
		FadeInOut(False)
		Delay(10)
		FadeInOut()
		Delay(2)
		'UPGRADE_NOTE: Black ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Black = Nothing
		'UPGRADE_NOTE: One ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		One = Nothing
		'UPGRADE_NOTE: Two ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Two = Nothing
		'UPGRADE_NOTE: Three ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Three = Nothing
		'UPGRADE_NOTE: Four ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Four = Nothing
		UnloadDx()
		End
	End Sub
	Public Function Battle() As Object
		'UPGRADE_NOTE: Black ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Black = Nothing
		'UPGRADE_NOTE: BigTalk ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		BigTalk = Nothing
		'UPGRADE_NOTE: Tclick ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Tclick = Nothing
		'UPGRADE_NOTE: Syurin ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Syurin = Nothing
		'UPGRADE_NOTE: Map4 ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Map4 = Nothing
		'UPGRADE_NOTE: Emotion ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Emotion = Nothing
		'UPGRADE_NOTE: ISyu ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		ISyu = Nothing
		'UPGRADE_NOTE: GoiHan ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		GoiHan = Nothing
		'UPGRADE_NOTE: Gone ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		Gone = Nothing
		Dim map(20, 30) As Integer : Dim i As Integer : Dim j As Integer
		For i = 0 To 5
			For j = 0 To 30
				map(i, j) = -99
			Next 
		Next 
		For i = 13 To 20
			For j = 0 To 30
				map(i, j) = -99
			Next 
		Next 
		For i = 6 To 12
			For j = 0 To 30
				map(i, j) = 0
			Next 
		Next 
		ExportFromPAK(AppPath & "\Library\Battle.pak", AppPath & "\Library\Sudden")
		InitPalatteFromBmpFile(AppPath & "\Library\Sudden\Pallette.bmp", ddsPrim)
		DDCreate(TPGage, AppPath & "\Library\Sudden\TPGage.bmp")
		DDCreate(TPG, AppPath & "\Library\Sudden\TPG.bmp")
		DDCreate(BMap, AppPath & "\Library\Sudden\Map.bmp")
		DDCreate(BMapM, AppPath & "\Library\Sudden\Move.bmp")
		DDCreate(Cursor, AppPath & "\Library\Sudden\Cursor.bmp")
		DDCreate(Syurin, AppPath & "\Library\Sudden\Syurin.bmp")
		DDCreate(Menu, AppPath & "\Library\Sudden\Menu.bmp")
		DDCreate(BMapS, AppPath & "\Library\Sudden\Select.bmp")
		DDCreate(Scur, AppPath & "\Library\Sudden\Scur.bmp")
		DDCreate(Black, AppPath & "\Library\Sudden\Black.bmp")
		DDCreate(Omenu, AppPath & "\Library\Sudden\Omenu.bmp")
		Kill(AppPath & "\Library\Sudden\*.*")
		FadeInOut()
		BGMSound("C:\SubjectNil\Bgm\Chaos.wav")
		BGM.SetVolume(-1000)
		Dim sx As Integer : sx = 0 : Dim sy As Integer : sy = 0
		Dim mapx As Integer : mapx = 1200 : Dim mapy As Integer : mapy = 1440
		Dim mx As Integer : mx = mapx / 60
		'UPGRADE_NOTE: my��(��) my_Renamed(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
		Dim my_Renamed As Integer : my_Renamed = mapy / 48
		Dim bw(20, 30) As Integer '�̵��� ���迭
		char_Renamed(0) = 0 'STATE
		char_Renamed(1) = 9 'X
		char_Renamed(2) = 7 'Y
		char_Renamed(3) = 800 'TP
		char_Renamed(4) = 0 'FootN
		char_Renamed(5) = 0 'SPEED
		xx = 0 '�����鼭 �����̵���
		yy = 0
		'�ʱ⼳��
		PlayWave(BGM, True)
		Dim vTem As DxVBLib.RECT
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		Dim mapR As DxVBLib.RECT
		With mapR
			.Left = sx
			.Right = .Left + 1024
			.Top = sy
			.Bottom = .Top + 768
		End With
		DDBltFast(BMap, 0, 0, mapR, True)
		EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, 0)
		DDFlip()
		FadeInOut(False)
		Static CurIndex As Short 'CurAni
		Dim menuN As Integer
		Dim k As Integer : k = 0 '����
		Dim s As Integer : s = 9 '����
		Dim lp As Integer '������
		Dim lx As Integer '��ũ��
		Dim ly As Integer '��ũ��
		Dim n As Integer 'for����
		Dim l As Integer '����
		Dim avg As Integer '���
		Dim returnK As Boolean : returnK = False '�޴�����
		Static mox As Integer
		Static moy As Integer
		Static obx As Integer
		Static oby As Integer
		Dim run As Boolean
		run = True
		Static lStart As Integer '�ӵ�����
		Dim m As Integer
		Do While run
			'������ 0.01��
			lStart = GetTickCount
			Do Until (lStart + 10 < GetTickCount)
			Loop 
			scrollB(sx, sy, mapx, mapy)
			If char_Renamed(0) = 0 Then '��ָ��
				GetCursorPos(pnt)
				If char_Renamed(3) = 1000 Then
					If LButton Then
						char_Renamed(0) = 1 '��ũ�Ѹ���
						lx = System.Math.Abs(sx + 512 - char_Renamed(1) * 60)
						ly = System.Math.Abs(sy + 384 - char_Renamed(2) * 48)
					End If
				Else
					char_Renamed(3) = char_Renamed(3) + 1
					If char_Renamed(3) > 1000 Then char_Renamed(3) = 1000
				End If
				EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, char_Renamed(5) * 4)
				'TP������ǥ��
				'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				vTem = DDGetRect(TPGage)
				DDBltFast(TPGage, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - 65 - sy, vTem, True, True)
				avg = Val(VB6.Format(char_Renamed(3) / 20, CStr(0)))
				With vTem
					.Left = 0
					.Right = avg
					.Top = 0
					.Bottom = 3
				End With
				DDBltFast(TPG, char_Renamed(1) * 60 + 6 - sx, char_Renamed(2) * 48 - 64 - sy, vTem, False, True)
				CursorDraw()
			ElseIf char_Renamed(0) = 1 Then  '��ũ�Ѹ��
				scrollE(sx, sy, mapx, mapy, System.Math.Abs(sx + 512 - char_Renamed(1) * 60), System.Math.Abs(sy + 384 - char_Renamed(2) * 48), lx, ly)
				EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, char_Renamed(5) * 4)
				'TP������ǥ��
				'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				vTem = DDGetRect(TPGage)
				DDBltFast(TPGage, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - 65 - sy, vTem, True, True)
				avg = Val(VB6.Format(char_Renamed(3) / 20, CStr(0)))
				With vTem
					.Left = 0
					.Right = avg
					.Top = 0
					.Bottom = 3
				End With
				DDBltFast(TPG, char_Renamed(1) * 60 + 6 - sx, char_Renamed(2) * 48 - 64 - sy, vTem, False, True)
				CursorDraw()
			ElseIf char_Renamed(0) = 2 Then  '�޴��������
				EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, char_Renamed(5) * 4)
				'TP������ǥ��
				'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				vTem = DDGetRect(TPGage)
				DDBltFast(TPGage, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - 65 - sy, vTem, True, True)
				avg = Val(VB6.Format(char_Renamed(3) / 20, CStr(0)))
				With vTem
					.Left = 0
					.Right = avg
					.Top = 0
					.Bottom = 3
				End With
				DDBltFast(TPG, char_Renamed(1) * 60 + 6 - sx, char_Renamed(2) * 48 - 64 - sy, vTem, False, True)
				With vTem
					.Left = k * 297
					.Right = .Left + 297
					.Top = 0
					.Bottom = 152
				End With
				DDBltFast(Omenu, 363, 308, vTem, True)
				k = k + 1
				If k >= 10 Then
					char_Renamed(0) = 3
					k = 0
				End If
				CursorDraw()
			ElseIf char_Renamed(0) = 3 Then  '�޴����
				EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, char_Renamed(5) * 4)
				'TP������ǥ��
				'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				vTem = DDGetRect(TPGage)
				DDBltFast(TPGage, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - 65 - sy, vTem, True, True)
				avg = Val(VB6.Format(char_Renamed(3) / 20, CStr(0)))
				With vTem
					.Left = 0
					.Right = avg
					.Top = 0
					.Bottom = 3
				End With
				DDBltFast(TPG, char_Renamed(1) * 60 + 6 - sx, char_Renamed(2) * 48 - 64 - sy, vTem, False, True)
				If pnt.curX >= 378 And pnt.curY >= 335 And pnt.curX <= 449 And pnt.curY <= 383 Then
					menuN = 1
				ElseIf pnt.curX >= 456 And pnt.curY >= 335 And pnt.curX <= 527 And pnt.curY <= 383 Then 
					menuN = 2
				ElseIf pnt.curX >= 534 And pnt.curY >= 335 And pnt.curX <= 605 And pnt.curY <= 383 Then 
					menuN = 3
				ElseIf pnt.curX >= 411 And pnt.curY >= 390 And pnt.curX <= 483 And pnt.curY <= 438 Then 
					menuN = 4
				ElseIf pnt.curX >= 489 And pnt.curY >= 390 And pnt.curX <= 561 And pnt.curY <= 438 Then 
					menuN = 5
				ElseIf pnt.curX >= 567 And pnt.curY >= 390 And pnt.curX <= 639 And pnt.curY <= 438 Then 
					menuN = 6
				Else : menuN = 0
				End If
				With vTem
					.Left = menuN * 297
					.Right = .Left + 297
					.Top = 0
					.Bottom = 152
				End With
				DDBltFast(Menu, 363, 308, vTem, True, True)
				If LButton Then 'ȿ������ �ִ´�
					If pnt.curX >= 378 And pnt.curY >= 335 And pnt.curX <= 449 And pnt.curY <= 383 Then
						'���ݸ�������� �����ȹ�� ���׿�.
						Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
					ElseIf pnt.curX >= 456 And pnt.curY >= 335 And pnt.curX <= 527 And pnt.curY <= 383 Then 
						'��ų��������� �����ȹ�� ���׿�.
						'��ųâ �ڵ巯���� ������µ� �Ʊ��� -_-+
						Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
					ElseIf pnt.curX >= 534 And pnt.curY >= 335 And pnt.curX <= 605 And pnt.curY <= 383 Then 
						'����������� �����ȹ�� ���׿�.
						Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
					ElseIf pnt.curX >= 411 And pnt.curY >= 390 And pnt.curX <= 483 And pnt.curY <= 438 Then 
						'�޽ĸ�������� �����ȹ�� ���׿�.
						Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
					ElseIf pnt.curX >= 489 And pnt.curY >= 390 And pnt.curX <= 561 And pnt.curY <= 438 Then 
						'������
						Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
						FadeInOut()
						Delay(2)
						UnloadDx()
						End
					ElseIf pnt.curX >= 567 And pnt.curY >= 390 And pnt.curX <= 639 And pnt.curY <= 438 Then 
						'ĵ�����
						Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
						returnK = True
						char_Renamed(0) = 4
					Else
					End If
				ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.A) <> 0 Then 
					Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
				ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.S) <> 0 Then 
					Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
				ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.D) <> 0 Then 
					Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
				ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.Z) <> 0 Then 
					Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
				ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.X) <> 0 Then 
					Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
					FadeInOut()
					Delay(2)
					UnloadDx()
					End
				ElseIf GetAsyncKeyState(System.Windows.Forms.Keys.C) <> 0 Then 
					Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
					returnK = True
					char_Renamed(0) = 4
				End If
				If RButton Then char_Renamed(0) = 4
				CursorDraw()
			ElseIf char_Renamed(0) = 4 Then  '�޴����Ÿ��
				EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, char_Renamed(5) * 4)
				'TP������ǥ��
				'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
				vTem = DDGetRect(TPGage)
				DDBltFast(TPGage, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - 65 - sy, vTem, True, True)
				avg = Val(VB6.Format(char_Renamed(3) / 20, CStr(0)))
				With vTem
					.Left = 0
					.Right = avg
					.Top = 0
					.Bottom = 3
				End With
				DDBltFast(TPG, char_Renamed(1) * 60 + 6 - sx, char_Renamed(2) * 48 - 64 - sy, vTem, False, True)
				With vTem
					.Left = s * 297
					.Right = .Left + 297
					.Top = 0
					.Bottom = 152
				End With
				DDBltFast(Omenu, 363, 308, vTem, True)
				s = s - 1
				If s <= -1 Then
					s = 9
					If returnK = False Then
						char_Renamed(0) = 5
					Else
						char_Renamed(0) = 0
						returnK = False
					End If
				End If
				CursorDraw()
			ElseIf char_Renamed(0) = 5 Then  '�̵��������
				If GetAsyncKeyState(System.Windows.Forms.Keys.Escape) <> 0 Then
					Effect(0).SetVolume(0) : Effect(0).SetPan(0) : PlayWave(Effect(0))
					char_Renamed(0) = 0
				End If
				GetCursorPos(pnt)
				map(char_Renamed(1), char_Renamed(2)) = 4
				For l = 4 To 2 Step -1
					For j = 1 To mx
						For k = 1 To my_Renamed
							If map(j, k) = l Then
								If map(j - 1, k) = 0 And j - 1 > 0 And k > 0 And j - 1 < mx + 1 And k < my_Renamed + 1 Then map(j - 1, k) = l - 1
								If map(j - 1, k) >= 0 Then
									With vTem
										.Left = (j - 1) * 60
										.Right = .Left + 60
										.Top = k * 48
										.Bottom = .Top + 48
									End With
									DDBltFast(BMapM, (j - 1) * 60 - sx, k * 48 - sy, vTem, False, True)
								End If
								If map(j + 1, k) = 0 And j + 1 > 0 And k > 0 And j + 1 < mx + 1 And k < my_Renamed + 1 Then map(j + 1, k) = l - 1
								If map(j + 1, k) >= 0 Then
									With vTem
										.Left = (j + 1) * 60
										.Right = .Left + 60
										.Top = k * 48
										.Bottom = .Top + 48
									End With
									DDBltFast(BMapM, (j + 1) * 60 - sx, k * 48 - sy, vTem, False, True)
								End If
								If map(j, k - 1) = 0 And j > 0 And k - 1 > 0 And j < mx + 1 And k - 1 < my_Renamed + 1 Then map(j, k - 1) = l - 1
								If map(j, k - 1) >= 0 Then
									With vTem
										.Left = j * 60
										.Right = .Left + 60
										.Top = (k - 1) * 48
										.Bottom = .Top + 48
									End With
									DDBltFast(BMapM, j * 60 - sx, (k - 1) * 48 - sy, vTem, False, True)
								End If
								If map(j, k + 1) = 0 And j > 0 And k + 1 > 0 And j < mx And k + 1 < my_Renamed Then map(j, k + 1) = l - 1
								If map(j, k + 1) >= 0 Then
									With vTem
										.Left = j * 60
										.Right = .Left + 60
										.Top = (k + 1) * 48
										.Bottom = .Top + 48
									End With
									DDBltFast(BMapM, j * 60 - sx, (k + 1) * 48 - sy, vTem, False, True)
								End If
							End If
						Next 
					Next 
				Next 
				'��������ϱ�
				m = 1
				If LButton And map(Val(VB6.Format((pnt.curX + sx) / 60, CStr(0))), Val(VB6.Format((pnt.curY + sy) / 48, CStr(0)))) > 0 Then
					obx = Val(VB6.Format((pnt.curX + sx) / 60, CStr(0)))
					oby = Val(VB6.Format((pnt.curY + sy) / 48, CStr(0)))
					mox = obx
					moy = oby
					If map(mox, moy) > 0 Then
						For n = 1 To 4
							bw(mox, moy) = 7 '�������� ����-_-;
							If map(mox + 1, moy) = m + 1 Then
								mox = mox + 1
								m = m + 1
							ElseIf map(mox - 1, moy) = m + 1 Then 
								mox = mox - 1
								m = m + 1
							ElseIf map(mox, moy + 1) = m + 1 Then 
								moy = moy + 1
								m = m + 1
							ElseIf map(mox, moy - 1) = m + 1 Then 
								moy = moy - 1
								m = m + 1
							End If
						Next 
					End If
					char_Renamed(0) = 6
					n = 0
				End If
				If map(Val(VB6.Format((pnt.curX + sx) / 60, CStr(0))), Val(VB6.Format((pnt.curY + sy) / 48, CStr(0)))) = 0 Then
					With vTem
						.Left = Val(VB6.Format((pnt.curX + sx) / 60, CStr(0))) * 60
						.Right = .Left + 60
						.Top = Val(VB6.Format((pnt.curY + sy) / 48, CStr(0))) * 48
						.Bottom = .Top + 48
					End With
					DDBltFast(BMapS, Val(VB6.Format((pnt.curX + sx) / 60, CStr(0))) * 60 - sx, Val(VB6.Format((pnt.curY + sy) / 48, CStr(0))) * 48 - sy, vTem, False, True)
					EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, char_Renamed(5) * 4)
					'TP������ǥ��
					'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					vTem = DDGetRect(TPGage)
					DDBltFast(TPGage, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - 65 - sy, vTem, True, True)
					avg = Val(VB6.Format(char_Renamed(3) / 20, CStr(0)))
					With vTem
						.Left = 0
						.Right = avg
						.Top = 0
						.Bottom = 3
					End With
					DDBltFast(TPG, char_Renamed(1) * 60 + 6 - sx, char_Renamed(2) * 48 - 64 - sy, vTem, False, True)
					If GetTime( , 9) > 100 Then
						Call GetTime(True, 9)
						CurIndex = CurIndex + 1
						If CurIndex > 3 Then CurIndex = 0
					End If
					With vTem
						.Left = CurIndex * 50
						.Right = (CurIndex * 50) + 50
						.Bottom = 50
						.Top = 0
					End With
					DDBltFast(Cursor, pnt.curX + 23, pnt.curY + 17, vTem, True, True)
				ElseIf map(Val(VB6.Format((pnt.curX + sx) / 60, CStr(0))), Val(VB6.Format((pnt.curY + sy) / 48, CStr(0)))) > 0 Then 
					With vTem
						.Left = Val(VB6.Format((pnt.curX + sx) / 60, CStr(0))) * 60
						.Right = .Left + 60
						.Top = Val(VB6.Format((pnt.curY + sy) / 48, CStr(0))) * 48
						.Bottom = .Top + 48
					End With
					DDBltFast(BMapS, Val(VB6.Format((pnt.curX + sx) / 60, CStr(0))) * 60 - sx, Val(VB6.Format((pnt.curY + sy) / 48, CStr(0))) * 48 - sy, vTem, False, True)
					EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, char_Renamed(5) * 4)
					'TP������ǥ��
					'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					vTem = DDGetRect(TPGage)
					DDBltFast(TPGage, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - 65 - sy, vTem, True, True)
					avg = Val(VB6.Format(char_Renamed(3) / 20, CStr(0)))
					With vTem
						.Left = 0
						.Right = avg
						.Top = 0
						.Bottom = 3
					End With
					DDBltFast(TPG, char_Renamed(1) * 60 + 6 - sx, char_Renamed(2) * 48 - 64 - sy, vTem, False, True)
					If GetTime( , 9) > 100 Then
						Call GetTime(True, 9)
						CurIndex = CurIndex + 1
						If CurIndex > 2 Then CurIndex = 0
					End If
					With vTem
						.Left = CurIndex * 50
						.Right = (CurIndex * 50) + 50
						.Bottom = 50
						.Top = 0
					End With
					DDBltFast(Scur, pnt.curX + 23, pnt.curY + 17, vTem, True, True)
				Else
					EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, char_Renamed(5) * 4)
					'TP������ǥ��
					'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
					vTem = DDGetRect(TPGage)
					DDBltFast(TPGage, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - 65 - sy, vTem, True, True)
					avg = Val(VB6.Format(char_Renamed(3) / 20, CStr(0)))
					With vTem
						.Left = 0
						.Right = avg
						.Top = 0
						.Bottom = 3
					End With
					DDBltFast(TPG, char_Renamed(1) * 60 + 6 - sx, char_Renamed(2) * 48 - 64 - sy, vTem, False, True)
					CursorDraw()
				End If
			ElseIf char_Renamed(0) = 6 Then  '�̵��߿��� ������ �ȳ��´�.
				If bw(char_Renamed(1) + 1, char_Renamed(2)) = 7 Then
					BWalk(Syurin, char_Renamed(1) * 60, char_Renamed(2) * 48, 3, char_Renamed(4), sx, sy)
					char_Renamed(5) = 3
					Delay(0.15)
					n = n + 1
					If n > 3 Then
						n = 0
						xx = 0
						yy = 0
						bw(char_Renamed(1), char_Renamed(2)) = 0
						char_Renamed(1) = char_Renamed(1) + 1
					End If
				ElseIf bw(char_Renamed(1) - 1, char_Renamed(2)) = 7 Then 
					BWalk(Syurin, char_Renamed(1) * 60, char_Renamed(2) * 48, 2, char_Renamed(4), sx, sy)
					char_Renamed(5) = 2
					Delay(0.15)
					n = n + 1
					If n > 3 Then
						n = 0
						xx = 0
						yy = 0
						bw(char_Renamed(1), char_Renamed(2)) = 0
						char_Renamed(1) = char_Renamed(1) - 1
					End If
				ElseIf bw(char_Renamed(1), char_Renamed(2) + 1) = 7 Then 
					BWalk(Syurin, char_Renamed(1) * 60, char_Renamed(2) * 48, 0, char_Renamed(4), sx, sy)
					char_Renamed(5) = 0
					Delay(0.15)
					n = n + 1
					If n > 3 Then
						n = 0
						xx = 0
						yy = 0
						bw(char_Renamed(1), char_Renamed(2)) = 0
						char_Renamed(2) = char_Renamed(2) + 1
					End If
				ElseIf bw(char_Renamed(1), char_Renamed(2) - 1) = 7 Then 
					BWalk(Syurin, char_Renamed(1) * 60, char_Renamed(2) * 48, 1, char_Renamed(4), sx, sy)
					char_Renamed(5) = 1
					Delay(0.15)
					n = n + 1
					If n > 3 Then
						n = 0
						xx = 0
						yy = 0
						bw(char_Renamed(1), char_Renamed(2)) = 0
						char_Renamed(2) = char_Renamed(2) - 1
					End If
				End If
				If obx = char_Renamed(1) And oby = char_Renamed(2) Then
					If char_Renamed(5) = 0 Then
						EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, char_Renamed(5) * 4 + 1)
					ElseIf char_Renamed(5) = 2 Or char_Renamed(5) = 3 Or char_Renamed(0) = 1 Then 
						EDrawChar(Syurin, char_Renamed(1) * 60 - sx + 5, char_Renamed(2) * 48 - sy - 55, char_Renamed(5) * 4 + 3)
					End If
					char_Renamed(0) = 0
					char_Renamed(3) = 800
					For j = 1 To mx
						For k = 1 To my_Renamed
							If map(j, k) > 0 Then map(j, k) = 0
						Next 
					Next 
					For j = 1 To mx
						For k = 1 To my_Renamed
							If bw(j, k) = 7 Then bw(j, k) = 0
						Next 
					Next 
				End If
			End If
			
			DDFlip()
			System.Windows.Forms.Application.DoEvents()
		Loop 
	End Function
	'UPGRADE_NOTE: char��(��) char_Renamed(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub BWalk(ByVal char_Renamed As DxVBLib.DirectDrawSurface7, ByRef x As Integer, ByRef y As Integer, ByVal speed As Integer, ByRef i As Integer, ByVal sx As Integer, ByVal sy As Integer)
		Dim Temrect As DxVBLib.RECT
		With Temrect
			.Left = (speed * 220) + (i * 55)
			.Right = .Left + 55
			.Top = 0
			.Bottom = 90
		End With
		Select Case speed
			Case 0
				If i = 1 Or i = 3 Then
					yy = yy + 24
				End If
			Case 1
				If i = 1 Or i = 3 Then
					yy = yy - 24
				End If
			Case 2
				If i = 1 Or i = 3 Then
					xx = xx - 30
				End If
			Case 3
				If i = 1 Or i = 3 Then
					xx = xx + 30
				End If
		End Select
		DDBltFast(char_Renamed, x - sx + 5 + xx, y - sy - 55 + yy, Temrect, True, True)
		i = i + 1
		If i > 3 Then i = 0
	End Sub
	Public Sub CursorDraw()
		Dim Temrect As DxVBLib.RECT
		Dim Cur As POINTAPI
		Static CurIndex As Short
		GetCursorPos(Cur)
		If GetTime( , 9) > 100 Then
			Call GetTime(True, 9)
			CurIndex = CurIndex + 1
			If CurIndex > 3 Then CurIndex = 0
		End If
		With Temrect
			.Left = CurIndex * 50
			.Right = (CurIndex * 50) + 50
			.Bottom = 50
			.Top = 0
		End With
		If pnt.curX >= 974 And pnt.curX <= 1024 And pnt.curY >= 718 And pnt.curY <= 768 Then
			DDBltFast(Cursor, 974, 718, Temrect, True, True)
		ElseIf pnt.curX >= 974 And pnt.curX <= 1024 Then 
			DDBltFast(Cursor, 974, Cur.curY, Temrect, True, True)
		ElseIf pnt.curY >= 718 And pnt.curY <= 768 Then 
			DDBltFast(Cursor, Cur.curX, 718, Temrect, True, True)
		Else : DDBltFast(Cursor, Cur.curX, Cur.curY, Temrect, True, True)
		End If
	End Sub
	Public Sub scrollE(ByRef sx As Integer, ByRef sy As Integer, ByVal mapx As Integer, ByVal mapy As Integer, ByVal cx As Integer, ByVal cy As Integer, ByVal lx As Integer, ByVal ly As Integer)
		Dim px As Integer
		Dim py As Integer
		If cx > cy Then
			px = 80
			py = System.Math.Abs((cy / cx) * 80)
		Else
			py = 80
			px = System.Math.Abs((cx / cy) * 80)
		End If
		GetCursorPos(pnt)
		If sx + 512 > char_Renamed(1) * 60 Then
			sx = sx - px
		Else
			sx = sx + px
		End If
		If sy + 384 > char_Renamed(2) * 48 Then
			sy = sy - py
		Else
			sy = sy + py
		End If
		If sx + 512 - (char_Renamed(1) * 60 - sx) > -160 And 160 > sx + 512 - (char_Renamed(1) * 60 - sx) And sy + 384 - (char_Renamed(2) * 48 - sy) > -160 And 160 > sy + 384 - (char_Renamed(2) * 48 - sy) Then char_Renamed(0) = 2
		If sx < 0 Then sx = 0
		If sx > mapx - 1024 Then sx = mapx - 1024
		If sy < 0 Then sy = 0
		If sy > mapy - 768 Then sy = mapy - 768
		Dim vTem As DxVBLib.RECT
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		Dim mapR As DxVBLib.RECT
		With mapR
			.Left = sx
			.Right = .Left + 1024
			.Top = sy
			.Bottom = .Top + 768
		End With
		DDBltFast(BMap, 0, 0, mapR, True)
	End Sub
	Public Sub scrollB(ByRef sx As Integer, ByRef sy As Integer, ByVal mapx As Integer, ByVal mapy As Integer)
		GetCursorPos(pnt)
		If pnt.curX >= 0 And pnt.curX <= 5 Then sx = sx - 30
		If pnt.curX >= 1019 And pnt.curX <= 1024 Then sx = sx + 30
		If pnt.curY >= 0 And pnt.curY <= 5 Then sy = sy - 30
		If pnt.curY >= 763 And pnt.curY <= 768 Then sy = sy + 30
		If sx < 0 Then sx = 0
		If sx > mapx - 1024 Then sx = mapx - 1024
		If sy < 0 Then sy = 0
		If sy > mapy - 768 Then sy = mapy - 768
		Dim vTem As DxVBLib.RECT
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Black)
		DDBltFast(Black, 0, 0, vTem, False)
		Dim mapR As DxVBLib.RECT
		With mapR
			.Left = sx
			.Right = .Left + 1024
			.Top = sy
			.Bottom = .Top + 768
		End With
		DDBltFast(BMap, 0, 0, mapR, True)
		If GetAsyncKeyState(System.Windows.Forms.Keys.P) <> 0 Then
			FadeInOut()
			Delay(2)
			UnloadDx()
			End
		End If
		If GetAsyncKeyState(System.Windows.Forms.Keys.V) <> 0 Then
			StopWave(BGM)
			FadeInOut()
			Delay(2)
			'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
			vTem = DDGetRect(Black)
			DDBltFast(Black, 0, 0, vTem, False)
			DDFlip()
			FadeInOut(False)
			Delay(2)
			Credit()
		End If
	End Sub
	'UPGRADE_NOTE: char��(��) char_Renamed(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Private Sub EWalk(ByVal char_Renamed As DxVBLib.DirectDrawSurface7, ByRef x As Integer, ByRef y As Integer, ByVal speed As Integer, ByRef i As Integer)
		Dim Temrect As DxVBLib.RECT
		With Temrect
			.Left = (speed * 220) + (i * 55)
			.Right = .Left + 55
			.Top = 0
			.Bottom = 90
		End With
		Select Case speed
			Case 0
				If i = 1 Or i = 3 Then
					y = y + 20
				End If
			Case 1
				If i = 1 Or i = 3 Then
					y = y - 20
				End If
			Case 2
				If i = 1 Or i = 3 Then
					x = x - 20
				End If
			Case 3
				If i = 1 Or i = 3 Then
					x = x + 20
				End If
		End Select
		DDBltFast(char_Renamed, x, y, Temrect, True, True)
		i = i + 1
		If i > 3 Then i = 0
	End Sub
	Public Function VisualF(ByVal nat As String) As Object
		Dim vTem As DxVBLib.RECT
		Dim i As Integer
		DDCopy(BackScreen2, ddsBack)
		'UPGRADE_WARNING: vTem ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		vTem = DDGetRect(Visual)
		DDBltFast(Visual, 360, 365, vTem, False)
		ddsBack.DrawText(367, 372, nat, False)
		DDFlip()
		DDCopy(ddsPrim, BackScreen4)
		Delay(0.5)
		Dim lStart As Integer
		Dim s As Integer
		s = 0
		Dim Temrect As DxVBLib.RECT
		Do Until LButton Or s > 20
			lStart = GetTickCount
			Do Until (lStart + 200 < GetTickCount)
			Loop 
			DDCopy(BackScreen4, ddsBack)
			With Temrect
				.Left = i * 16
				.Right = (i * 16) + 16
				.Top = 0
				.Bottom = 16
			End With
			DDBltFast(Tclick, 614, 372, Temrect, True, True)
			DDFlip()
			i = i + 1
			If i > 1 Then i = 0
			System.Windows.Forms.Application.DoEvents()
			s = s + 1
		Loop 
		Effect(4).SetVolume(0)
		PlayWave(Effect(4))
		DDCopy(BackScreen2, ddsBack)
		DDFlip()
		Delay(1)
	End Function
	'UPGRADE_NOTE: char��(��) char_Renamed(��)�� ���׷��̵�Ǿ����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="A9E4979A-37FA-4718-9994-97DD76ED70A7"'
	Public Function Speak(ByVal char_Renamed As String, ByVal ir As Boolean, ByVal Speak1 As String, Optional ByVal Speak2 As String = "", Optional ByVal Speak3 As String = "", Optional ByVal Speak4 As String = "", Optional ByVal Speak5 As String = "", Optional ByVal Speak6 As String = "", Optional ByVal Speak7 As String = "", Optional ByVal illust As DxVBLib.DirectDrawSurface7 = Nothing) As Object
		Dim TempFont As System.Drawing.Font = System.Windows.Forms.Control.DefaultFont.Clone()
		'UPGRADE_WARNING: Windows Forms������ Ʈ��Ÿ�� �� ����Ÿ�� �۲ø� �����˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="971F4DF4-254E-44F4-861D-3AA0031FE361"'
		TempFont = VB6.FontChangeName(TempFont, "�����������9")
		TempFont = VB6.FontChangeSize(TempFont, 9)
		TempFont = VB6.FontChangeBold(TempFont, True)
		ddsBack.SetFont(TempFont.Name)
		Dim i As Integer : Dim nEnd As Integer : Dim nLong As Integer : Dim cOut As String
		i = 0 : nEnd = 1
		Dim k As Integer
		Dim vTem As DxVBLib.RECT
		For k = 0 To 9
			DDCopy(BackScreen2, ddsBack)
			With vTem
				.Left = 0
				.Right = 900
				.Top = k * 150
				.Bottom = .Top + 150
			End With
			DDBltFast(BigTalk, 100, 600, vTem, True)
			DDFlip()
			Delay(0.02)
		Next 
		If ir = True Then
			With vTem
				.Left = 0
				.Right = 400
				.Top = 0
				.Bottom = 550
			End With
			DDBltFast(illust, 0, 218, vTem, True, True)
		End If
		With vTem
			.Left = 0
			.Right = 900
			.Top = 1350
			.Bottom = 1500
		End With
		DDBltFast(BigTalk, 100, 600, vTem, True)
		'�̸�ǥ��
		ddsBack.SetForeColor(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black))
		ddsBack.DrawText(910, 610, "[ " & char_Renamed & " ]", False)
		ddsBack.SetForeColor(System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White))
		DDCopy(ddsBack, BackScreen3)
		nLong = Len(Speak1)
		For i = 1 To nLong
			DDCopy(BackScreen3, ddsBack)
			cOut = Mid(Speak1, 1, nEnd)
			nEnd = nEnd + 1
			ddsBack.DrawText(115, 632, cOut, False)
			DDCopy(ddsBack, BackScreen3)
			DDFlip()
			If LButton = True Then
				Delay(0.005)
			Else
				Delay(0.05)
			End If
		Next 
		If Speak2 = "" Then
		Else
			nEnd = 1
			DDCopy(BackScreen3, ddsBack)
			nLong = Len(Speak2)
			For i = 1 To nLong
				cOut = Mid(Speak2, 1, nEnd)
				nEnd = nEnd + 1
				ddsBack.DrawText(115, 646, cOut, False)
				DDCopy(ddsBack, BackScreen3)
				DDFlip()
				If LButton = True Then
					Delay(0.005)
				Else
					Delay(0.05)
				End If
			Next 
		End If
		If Speak3 = "" Then
		Else
			nEnd = 1
			DDCopy(BackScreen3, ddsBack)
			nLong = Len(Speak3)
			For i = 1 To nLong
				cOut = Mid(Speak3, 1, nEnd)
				nEnd = nEnd + 1
				ddsBack.DrawText(115, 660, cOut, False)
				DDCopy(ddsBack, BackScreen3)
				DDFlip()
				If LButton = True Then
					Delay(0.005)
				Else
					Delay(0.05)
				End If
			Next 
		End If
		If Speak4 = "" Then
		Else
			nEnd = 1
			DDCopy(BackScreen3, ddsBack)
			nLong = Len(Speak4)
			For i = 1 To nLong
				cOut = Mid(Speak4, 1, nEnd)
				nEnd = nEnd + 1
				ddsBack.DrawText(115, 674, cOut, False)
				DDCopy(ddsBack, BackScreen3)
				DDFlip()
				If LButton = True Then
					Delay(0.005)
				Else
					Delay(0.05)
				End If
			Next 
		End If
		If Speak5 = "" Then
		Else
			nEnd = 1
			DDCopy(BackScreen3, ddsBack)
			nLong = Len(Speak5)
			For i = 1 To nLong
				cOut = Mid(Speak5, 1, nEnd)
				nEnd = nEnd + 1
				ddsBack.DrawText(115, 688, cOut, False)
				DDFlip()
				If LButton = True Then
					Delay(0.005)
				Else
					Delay(0.05)
				End If
			Next 
		End If
		If Speak6 = "" Then
		Else
			nEnd = 1
			DDCopy(BackScreen3, ddsBack)
			nLong = Len(Speak6)
			For i = 1 To nLong
				cOut = Mid(Speak6, 1, nEnd)
				nEnd = nEnd + 1
				ddsBack.DrawText(115, 674, cOut, False)
				DDCopy(ddsBack, BackScreen3)
				DDFlip()
				If LButton = True Then
					Delay(0.005)
				Else
					Delay(0.05)
				End If
			Next 
		End If
		If Speak7 = "" Then
		Else
			nEnd = 1
			DDCopy(BackScreen3, ddsBack)
			nLong = Len(Speak7)
			For i = 1 To nLong
				cOut = Mid(Speak7, 1, nEnd)
				nEnd = nEnd + 1
				ddsBack.DrawText(115, 688, cOut, False)
				DDFlip()
				If LButton = True Then
					Delay(0.005)
				Else
					Delay(0.05)
				End If
			Next 
		End If
		DDCopy(ddsPrim, BackScreen4)
		Delay(0.5)
		Dim lStart As Integer
		Dim s As Integer
		s = 0
		Dim Temrect As DxVBLib.RECT
		Do Until LButton Or s > 30
			lStart = GetTickCount
			Do Until (lStart + 200 < GetTickCount)
			Loop 
			DDCopy(BackScreen4, ddsBack)
			With Temrect
				.Left = i * 16
				.Right = (i * 16) + 16
				.Top = 0
				.Bottom = 16
			End With
			DDBltFast(Tclick, 962, 716, Temrect, True, True)
			DDFlip()
			i = i + 1
			If i > 1 Then i = 0
			System.Windows.Forms.Application.DoEvents()
			s = s + 1
		Loop 
		Effect(4).SetVolume(0)
		PlayWave(Effect(4))
		For k = 9 To 0 Step -1
			DDCopy(BackScreen2, ddsBack)
			With vTem
				.Left = 0
				.Right = 900
				.Top = k * 150
				.Bottom = .Top + 150
			End With
			DDBltFast(BigTalk, 100, 600, vTem, True)
			DDFlip()
			Delay(0.02)
		Next 
		DDCopy(BackScreen2, ddsBack)
		DDFlip()
	End Function
	Private Sub Delay(ByRef fTime As Single)
		Dim fStartTime As Single
		fStartTime = VB.Timer()
		Do While VB.Timer() < fStartTime + fTime
			System.Windows.Forms.Application.DoEvents()
		Loop 
	End Sub
	Public Sub InitDDraw()
		DD = dx.DirectDrawCreate("")
		Call DD.SetCooperativeLevel(frmMain.Handle.ToInt32, DxVBLib.CONST_DDSCLFLAGS.DDSCL_EXCLUSIVE Or DxVBLib.CONST_DDSCLFLAGS.DDSCL_FULLSCREEN Or DxVBLib.CONST_DDSCLFLAGS.DDSCL_ALLOWMODEX)
		Call DD.SetDisplayMode(1024, 768, 8, 0, DxVBLib.CONST_DDSDMFLAGS.DDSDM_DEFAULT)
		ddsd1.lFlags = DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_BACKBUFFERCOUNT Or DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_CAPS
		ddsd1.lBackBufferCount = 1
		ddsd1.ddsCaps.lCaps = DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_FLIP Or DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_COMPLEX Or DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_PRIMARYSURFACE
		ddsPrim = DD.CreateSurface(ddsd1)
		ddCaps.lCaps = DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_BACKBUFFER
		ddsBack = ddsPrim.GetAttachedSurface(ddCaps)
		ddsBack.GetSurfaceDesc(ddsd2)
	End Sub
	Public Function InitPalatteFromBmpFile(ByRef aFileName As String, ByRef aSurface As DxVBLib.DirectDrawSurface7) As Boolean
		Dim bmFileHeader As BITMAPFILEHEADER
		Dim bmInfoHeader As BITMAPINFOHEADER
		Dim Pal(255) As DxVBLib.PALETTEENTRY
		Dim hFile As Integer
		Dim i As Short
		On Error GoTo ErrRaise
		'UPGRADE_WARNING: Dir�� �� ������ �ֽ��ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		If Dir(aFileName) = "" Then
			InitPalatteFromBmpFile = False
			Exit Function
		End If
		hFile = FreeFile
		FileOpen(hFile, aFileName, OpenMode.Binary)
		'UPGRADE_WARNING: Get��(��) FileGet(��)�� ���׷��̵�Ǿ� �� ������ �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(hFile, bmFileHeader)
		'UPGRADE_WARNING: Get��(��) FileGet(��)�� ���׷��̵�Ǿ� �� ������ �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(hFile, bmInfoHeader)
		'UPGRADE_WARNING: Get��(��) FileGet(��)�� ���׷��̵�Ǿ� �� ������ �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
		FileGet(hFile, Pal)
		FileClose(hFile)
		For i = 0 To 255
			ddPal(i).red = Pal(i).blue
			ddPal(i).green = Pal(i).green
			ddPal(i).blue = Pal(i).red
			ddPal(i).Flags = 0
		Next i
		ddPalette = DD.CreatePalette(DxVBLib.CONST_DDPCAPSFLAGS.DDPCAPS_8BIT Or DxVBLib.CONST_DDPCAPSFLAGS.DDPCAPS_ALLOW256, ddPal)
		Call aSurface.SetPalette(ddPalette)
		InitPalatteFromBmpFile = True
		Exit Function
ErrRaise: 
		InitPalatteFromBmpFile = False
	End Function
	Public Sub FadeInOut(Optional ByRef iMode As Boolean = True)
		Dim iFor As Short
		Dim iPer As Short
		Dim Pal(255) As DxVBLib.PALETTEENTRY
		Dim fTime As Single
		Dim R As Short
		Dim g As Short
		Dim b As Short
		Dim i As Short
		Dim lStart As Integer
		If iMode Then
			For iFor = 100 To 0 Step -1
				iPer = iFor
				fTime = iPer / 100
				lStart = GetTickCount
				Do Until (lStart + 10 < GetTickCount)
				Loop 
				For i = 0 To 255
					R = (ddPal(i).red) * fTime : If R > 255 Then R = 255
					g = (ddPal(i).green) * fTime : If g > 255 Then g = 255
					b = (ddPal(i).blue) * fTime : If b > 255 Then b = 255
					Pal(i).red = Int(R)
					Pal(i).green = Int(g)
					Pal(i).blue = Int(b)
					Pal(i).Flags = ddPal(i).Flags
				Next 
				Call ddPalette.SetEntries(0, 255, Pal)
				System.Windows.Forms.Application.DoEvents()
			Next 
			Exit Sub
		Else
			For iFor = 0 To 100
				iPer = iFor
				fTime = iPer / 100
				lStart = GetTickCount
				Do Until (lStart + 10 < GetTickCount)
				Loop 
				For i = 0 To 255
					R = (ddPal(i).red) * fTime : If R > 255 Then R = 255
					g = (ddPal(i).green) * fTime : If g > 255 Then g = 255
					b = (ddPal(i).blue) * fTime : If b > 255 Then b = 255
					Pal(i).red = Int(R)
					Pal(i).green = Int(g)
					Pal(i).blue = Int(b)
					Pal(i).Flags = ddPal(i).Flags
				Next 
				Call ddPalette.SetEntries(0, 255, Pal)
				System.Windows.Forms.Application.DoEvents()
			Next 
			Exit Sub
		End If
	End Sub
	Public Sub WhiteInOut(Optional ByRef iMode As Boolean = True)
		Dim Pal(255) As DxVBLib.PALETTEENTRY
		Dim fPer As Single
		Dim R As Short
		Dim g As Short
		Dim b As Short
		Dim i As Short
		Dim j As Short
		Dim iPer As Short
		Dim lStart As Integer
		If Not iMode Then
			For j = 0 To 100
				iPer = 100 - j
				fPer = iPer / 100
				lStart = GetTickCount
				Do Until (lStart + 10 < GetTickCount)
				Loop 
				For i = 0 To 255
					R = ddPal(i).red + ((255 - ddPal(i).red) * fPer) : If R > 255 Then R = 255
					g = ddPal(i).green + ((255 - ddPal(i).green) * fPer) : If g > 255 Then g = 255
					b = ddPal(i).blue + ((255 - ddPal(i).blue) * fPer) : If b > 255 Then b = 255
					Pal(i).red = Int(R)
					Pal(i).green = Int(g)
					Pal(i).blue = Int(b)
					Pal(i).Flags = ddPal(i).Flags
				Next i
				Call ddPalette.SetEntries(0, 255, Pal)
				System.Windows.Forms.Application.DoEvents()
			Next 
		Else
			For j = 100 To 0 Step -1
				iPer = 100 - j
				fPer = iPer / 100
				lStart = GetTickCount
				Do Until (lStart + 10 < GetTickCount)
				Loop 
				For i = 0 To 255
					R = ddPal(i).red + ((255 - ddPal(i).red) * fPer) : If R > 255 Then R = 255
					g = ddPal(i).green + ((255 - ddPal(i).green) * fPer) : If g > 255 Then g = 255
					b = ddPal(i).blue + ((255 - ddPal(i).blue) * fPer) : If b > 255 Then b = 255
					Pal(i).red = Int(R)
					Pal(i).green = Int(g)
					Pal(i).blue = Int(b)
					Pal(i).Flags = ddPal(i).Flags
				Next i
				Call ddPalette.SetEntries(0, 255, Pal)
				System.Windows.Forms.Application.DoEvents()
			Next 
		End If
	End Sub
	Public Sub BlackWhite()
		Dim Pal(255) As DxVBLib.PALETTEENTRY
		Dim R As Short
		Dim g As Short
		Dim b As Short
		Dim i As Short
		Dim Aver As Short
		For i = 0 To 255
			R = ddPal(i).red
			g = ddPal(i).green
			b = ddPal(i).blue
			Aver = Int((R + g + b) / 3)
			Pal(i).red = Aver
			Pal(i).green = Aver
			Pal(i).blue = Aver
			Pal(i).Flags = ddPal(i).Flags
		Next i
		Call ddPalette.SetEntries(0, 255, Pal)
	End Sub
	Public Sub DDRestorePal()
		Call ddPalette.SetEntries(0, 255, ddPal)
	End Sub
	Public Function AppPath() As String
		AppPath = IIf(Right(My.Application.Info.DirectoryPath, 1) = "\", Left(My.Application.Info.DirectoryPath, Len(My.Application.Info.DirectoryPath) - 1), My.Application.Info.DirectoryPath)
	End Function
	Public Sub ExportFromPAK(ByRef PAKArchive As String, ByRef FolderPath As String)
		On Error GoTo ErrEnd
		'UPGRADE_WARNING: vPAKData ����ü�� �迭�� ����ϱ� ���� �ʱ�ȭ�ؾ� �մϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="814DF224-76BD-4BB4-BFFB-EA359CB9FC48"'
		Dim vPAKData As PAK_Item
		Dim FF2, FF, FileSize As Integer
		FF = FreeFile
		FileOpen(FF, PAKArchive, OpenMode.Binary)
		Do Until EOF(FF)
			FileSize = Seek(FF) + 3 - 1
			'UPGRADE_WARNING: Get��(��) FileGet(��)�� ���׷��̵�Ǿ� �� ������ �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FileGet(FF, vPAKData, FileSize)
			FF2 = FreeFile
			FileOpen(FF2, FolderPath & "\" & vPAKData.FileName, OpenMode.Binary)
			'UPGRADE_WARNING: Put��(��) FilePut(��)�� ���׷��̵�Ǿ� �� ������ �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="9B7D5ADD-D8FE-4819-A36C-6DEDAF088CC7"'
			FilePut(FF2, vPAKData.Data)
			FileClose(FF2)
		Loop 
		FileClose(FF)
ErrEnd: 
	End Sub
	Public Sub DDCreate(ByRef cSurface As DxVBLib.DirectDrawSurface7, ByVal cFileName As String, Optional ByVal cWidth As Short = -1, Optional ByVal cHeight As Short = -1)
		Dim DDSDTem As DxVBLib.DDSURFACEDESC2
		'UPGRADE_NOTE: cSurface ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		cSurface = Nothing
		If cWidth <> -1 Then
			DDSDTem.lFlags = DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_WIDTH Or DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_HEIGHT Or DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_CAPS
			DDSDTem.lWidth = cWidth
			DDSDTem.lHeight = cHeight
		Else
			DDSDTem.lFlags = DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_CAPS
		End If
		DDSDTem.ddsCaps.lCaps = DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_OFFSCREENPLAIN Or DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_SYSTEMMEMORY
		cSurface = DD.CreateSurfaceFromFile(cFileName, DDSDTem)
		Dim ddColor As DxVBLib.DDCOLORKEY
		ddColor.low = 0
		ddColor.high = 0
		cSurface.SetColorKey(DxVBLib.CONST_DDCKEYFLAGS.DDCKEY_SRCBLT, ddColor)
	End Sub
	Public Function DDBltFast(ByRef cSurface As DxVBLib.DirectDrawSurface7, ByVal x As Single, ByVal y As Single, ByRef SourceRect As DxVBLib.RECT, Optional ByVal Transparent As Boolean = True, Optional ByVal Cliping As Boolean = False) As Boolean
		Dim Flags As DxVBLib.CONST_DDBLTFASTFLAGS
		Dim RectTEMP As DxVBLib.RECT
		Dim ScreenRECT As DxVBLib.RECT
		'UPGRADE_WARNING: RectTEMP ��ü�� �⺻ �Ӽ��� Ȯ���� �� �����ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6A50421D-15FE-4896-8A1B-2EC21E9037B2"'
		RectTEMP = SourceRect
		If Cliping Then
			With ScreenRECT
				.Top = y
				.Left = x
				.Bottom = y + SourceRect.Bottom - SourceRect.Top
				.Right = x + SourceRect.Right - SourceRect.Left
			End With
			With ScreenRECT
				If .Bottom > 768 Then
					RectTEMP.Bottom = RectTEMP.Bottom - (.Bottom - 768)
					.Bottom = 768 - 1
				End If
				If .Left < 0 Then
					RectTEMP.Left = RectTEMP.Left - .Left
					.Left = 0
					x = 0
				End If
				If .Right > 1024 Then
					RectTEMP.Right = RectTEMP.Right - (.Right - 1024)
					.Right = 1024
				End If
				If .Top < 0 Then
					RectTEMP.Top = RectTEMP.Top - .Top
					.Top = 0
					y = 0
				End If
			End With
		End If
		If Transparent = True Then
			ddsBack.BltFast(x, y, cSurface, RectTEMP, DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_SRCCOLORKEY)
		Else
			ddsBack.BltFast(x, y, cSurface, RectTEMP, DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_WAIT)
		End If
		DDBltFast = True
		If Err.Number <> 0 Then GoTo errh
		Exit Function
errh: 
		DDBltFast = False
	End Function
	Public Function DDGetRect(ByRef aSource As DxVBLib.DirectDrawSurface7) As DxVBLib.RECT
		Dim TemDesc As DxVBLib.DDSURFACEDESC2
		Call aSource.GetSurfaceDesc(TemDesc)
		DDGetRect.Bottom = TemDesc.lHeight
		DDGetRect.Right = TemDesc.lWidth
	End Function
	Public Sub DDFlip()
		On Error GoTo errh
		ddsPrim.Flip(ddsBack, DxVBLib.CONST_DDFLIPFLAGS.DDFLIP_WAIT)
		Exit Sub
errh: 
	End Sub
	Public Function LButton() As Boolean
		LButton = IIf(GetKeyState(VB6.MouseButtonConstants.LeftButton) < 0, True, False)
	End Function
	Public Function RButton() As Boolean
		RButton = IIf(GetKeyState(VB6.MouseButtonConstants.RightButton) < 0, True, False)
	End Function
	Public Function GetTime(Optional ByRef vSet As Boolean = False, Optional ByRef vRoom As Short = 0) As Single
		Dim NowTime As Single
		NowTime = dx.TickCount
		GetTime = (NowTime - aTickCount(vRoom))
		aTickCount(vRoom) = IIf(vSet, NowTime, aTickCount(vRoom))
	End Function
	Public Sub DDCreateNo(ByRef cSurface As DxVBLib.DirectDrawSurface7, Optional ByVal Trans As Boolean = False)
		Dim DDSDTem As DxVBLib.DDSURFACEDESC2
		'UPGRADE_NOTE: cSurface ��ü�� �������� �����Ǿ�� �Ҹ�˴ϴ�. �ڼ��� ������ ������ �����Ͻʽÿ�. 'ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?keyword="6E35BFF6-CD74-4B09-9689-3E1A43DF8969"'
		cSurface = Nothing
		DDSDTem.lFlags = DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_CAPS Or DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_WIDTH Or DxVBLib.CONST_DDSURFACEDESCFLAGS.DDSD_HEIGHT
		DDSDTem.lWidth = 1024
		DDSDTem.lHeight = 768
		DDSDTem.ddsCaps.lCaps = DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_OFFSCREENPLAIN Or DxVBLib.CONST_DDSURFACECAPSFLAGS.DDSCAPS_SYSTEMMEMORY
		cSurface = DD.CreateSurface(DDSDTem)
		Dim ddColor As DxVBLib.DDCOLORKEY
		If Trans Then
			ddColor.low = 0
			ddColor.high = 0
			cSurface.SetColorKey(DxVBLib.CONST_DDCKEYFLAGS.DDCKEY_SRCBLT, ddColor)
		End If
	End Sub
	Public Sub DDCopy(ByRef aSurface As DxVBLib.DirectDrawSurface7, ByRef bSurface As DxVBLib.DirectDrawSurface7)
		Dim TemSou As DxVBLib.RECT
		TemSou.Bottom = 768
		TemSou.Right = 1024
		TemSou.Left = 0
		TemSou.Top = 0
		bSurface.BltFast(0, 0, aSurface, TemSou, DxVBLib.CONST_DDBLTFASTFLAGS.DDBLTFAST_WAIT)
	End Sub
	Public Sub LoadWave(ByRef DsBuffer As DxVBLib.DirectSoundBuffer, ByVal File As String)
		On Error GoTo s10
		Dim bufferDesc As DxVBLib.DSBUFFERDESC
		Dim waveFormat As DxVBLib.WAVEFORMATEX
		bufferDesc.lFlags = DxVBLib.CONST_DSBCAPSFLAGS.DSBCAPS_CTRLFREQUENCY Or DxVBLib.CONST_DSBCAPSFLAGS.DSBCAPS_CTRLPAN Or DxVBLib.CONST_DSBCAPSFLAGS.DSBCAPS_CTRLVOLUME Or DxVBLib.CONST_DSBCAPSFLAGS.DSBCAPS_STATIC
		waveFormat.nFormatTag = DxVBLib.CONST_DSOUND.WAVE_FORMAT_PCM
		waveFormat.nChannels = 2
		waveFormat.lSamplesPerSec = 44100
		waveFormat.nBitsPerSample = 16
		waveFormat.nBlockAlign = waveFormat.nBitsPerSample / 8 * waveFormat.nChannels
		waveFormat.lAvgBytesPerSec = waveFormat.lSamplesPerSec * waveFormat.nBlockAlign
		DsBuffer = ds.CreateSoundBufferFromFile(File, bufferDesc, waveFormat)
s10: 
	End Sub
	Public Sub PlayWave(ByRef DsBuffer As DxVBLib.DirectSoundBuffer, Optional ByVal Looping As Boolean = False)
		On Error Resume Next
		If Looping Then
			DsBuffer.Play(DxVBLib.CONST_DSBPLAYFLAGS.DSBPLAY_LOOPING)
			DsBuffer.GetStatus()
		Else
			DsBuffer.Play(DxVBLib.CONST_DSBPLAYFLAGS.DSBPLAY_DEFAULT)
		End If
	End Sub
	Public Sub StopWave(ByRef DsBuffer As DxVBLib.DirectSoundBuffer)
		DsBuffer.Stop()
		DsBuffer.SetCurrentPosition(0)
	End Sub
End Module