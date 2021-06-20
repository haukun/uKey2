# uKey2
Useful key Version 2
http://ukey.projectroom.jp/

# 概要(Overview)
Windowsにて、ホットキーとWIN32の[SendMessage](https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessage)を紐づけることができるツールです。

It is a tool that can link hotkeys and WIN32 [SendMessage](https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-sendmessage) on Windows.

# インストール(How to install)
Release.zipを解凍し、Setting/setting.jsonを編集してからuKey2.exeを実行してください。

Unzip Release.zip, edit Setting / setting.json, and then run uKey2.exe.

## setting.json
ホットキーとSendMessageとの連携を記述するファイルです。
JSONフォーマットで記載してください。

It is a file that describes the cooperation between hotkeys and SendMessage.
Please edit in JSON format.

### キー(Keys)
- Settings
  - shift
    - true / false(デフォルト)
    - シフトキーと組み合わせるかどうか。(Whether to combine with the shift key.)
  - control
    - true / false(デフォルト)
    - コントロールキーと組み合わせるかどうか。(Whether to combine with the control key.)
  - alt
    - true / false(デフォルト)
    - オルトキーと組み合わせるかどうか。(Whether to combine with alt key.)
  - win
    - true / false(デフォルト)
    - ウィンドウズキーと組み合わせるかどうか。(Whether to combine with Windows key.)
  - key
    - [keys列挙体](https://docs.microsoft.com/ja-jp/dotnet/api/system.windows.forms.keys?view=net-5.0)の名称
    - 組み合わせるキー(Whether to combine with the Windows key.)
  - message
    - SendMessageのメッセージID(SendMessage message ID)
  - wParam
    - SendMessageに送るwParam(Send to SendMessage wParam)
  - lParam
    - SendMessageに送るlParamSend to SendMessage lParam)

### 例(Example)
```
{
  "Settings": [
    {
      "control": true,
      "key": "D0",
      "message": 793,
      "wParam": 0,
      "lParam": 1572864
    }
  ]
}
```

## その他仕様(Other specifications)

### マイク(Microphone)
- マイクON/OFF（message:793 / lParam:1572864）の場合、マイクの状態がタスクバーに表示されます。(When the microphone is ON / OFF (message: 793 / lParam: 1572864), the status of the microphone is displayed on the taskbar.)
