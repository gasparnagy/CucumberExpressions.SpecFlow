@pushd %~dp0

set PRODVER=%1
set CONFIG_VAL=Release

@echo publishing version %PRODVER%, %CONFIG%, OK?
@pause

dotnet build -c "%CONFIG_VAL%" -p:PackageVersion=%PRODVER% -p:Version=%PRODVER%
IF ERRORLEVEL 1 GOTO error

copy /Y CucumberExpressions.SpecFlow.SpecFlowPlugin\bin\%CONFIG_VAL%\CucumberExpressions.SpecFlow.3-5.%PRODVER%.nupkg %NUGET_LOCAL_FEED%\CucumberExpressions.SpecFlow.3-5.%PRODVER%.nupkg
IF ERRORLEVEL 1 GOTO error

@popd
goto end

:error
echo ERROR!!!
@popd
exit /b

:end
