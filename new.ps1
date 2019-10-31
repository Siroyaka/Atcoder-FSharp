<#

#>
param([string]$command, [string]$contestName, [array]$questionNames)
$LOGFILE = Join-Path $PSScriptRoot "new.ps1.log";

function WriteLogFile([string]$logMessage) {
    $nowDate = Get-Date -Format "yyyy/MM/dd HH:mm:ss.f";
    Write-Output "$($nowDate) $($logMessage)" | Add-Content $LOGFILE;
    Write-Host $logMessage;
}

function MakeProject($questionName) {
    # プロジェクトを作成する
    $sourcePath = Join-Path "src" $questionName;
    dotnet.exe new console -lang F`# -o $sourcePath | Out-Null;
    WriteLogFile "Create Project [$($questionName)]";
    
    # プロジェクトをソリューションに関連付ける
    $projFilePath = Join-Path $sourcePath "$($questionName).fsproj";
    dotnet.exe sln add $projFilePath | Out-Null;
    WriteLogFile "Join Solution [$($questionName)]";
}

function CreateContest([string]$contestPath) {
    WriteLogFile "CreateContest";

    # すでにコンテストフォルダが作られている
    if (Test-Path $contestPath) {
        WriteLogFile "コンテスト名[$($contestName)]が既に存在します。";
        return;
    }

    # questionNameが空
    if ($questionNames.Length -eq 0) {
        WriteLogFile "問題名が空です。";
        return;
    }

    # コンテストのソリューションを作成する。
    dotnet.exe new sln -o $contestPath | Out-Null;
    WriteLogFile "Create Solution [$($contestName)]";

    # ディレクトリを変更する。
    Set-Location $contestPath | Out-Null;
    
    # 問題のプロジェクトを作成し、ソリューションと結びつける。
    foreach ($questionName in $questionNames) {
        MakeProject $questionName;
    }

    code .
	Write-Host "Open solution for VSCode";
    Set-Location $PSScriptRoot;
}

function AddQuestion([string]$contestPath) {
    WriteLogFile "AddQuestion";

    # コンテストフォルダが存在しない
    if (!(Test-Path $contestPath)) {
        WriteLogFile "コンテスト名[$($contestName)]が存在しません。";
        return;
    }

    # questionNameが空
    if ($questionNames.Length -eq 0) {
        WriteLogFile "問題名が空です。";
        return;
    }

    # ディレクトリを変更する。
    Set-Location $contestPath | Out-Null;
    
    # 問題のプロジェクトを作成し、ソリューションと結びつける。
    foreach ($questionName in $questionNames) {
        if (Test-Path(Join-Path $contestPath $questionName)) {
            WriteLogFile "コンテストフォルダ内に[$($questionName)]が既に存在します。";
            continue;
        }
        MakeProject $questionName;
    }

    code .
	Write-Host "Open solution for VSCode";
    Set-Location $PSScriptRoot;
}

function Main {
    Write-Host $questionNames;
    # ログフォルダの作成(存在していない場合)
    if (!(Test-Path $LOGFILE)) {
        New-Item -Path $LOGFILE -ItemType File;
    }

    # コンテストフォルダのフルパスを作成
    $contestFullPath = Join-Path $PSScriptRoot $contestName;
    switch ($command.ToLower()) {
        "create" {
            CreateContest $contestFullPath;
            break;
        }
        "Add" {
            AddQuestion $contestFullPath;
            break;
        }
        default {
            Write-Host "コマンド[$($command)]は有効ではありません。";
            break;
        }
    }

    WriteLogFile "Finish";
    WriteLogFile "--------------------------------";
}

Main;
